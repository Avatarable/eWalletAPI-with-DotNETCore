using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WallerAPI.Commons;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly INotificationServices _notify;
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userMgr;

        public AuthController(UserManager<User> userMgr, IAuthServices authServices, IMapper mapper,
            IConfiguration config, INotificationServices notify, IUserServices userServices)
        {
            _userMgr = userMgr;
            _mapper = mapper;
            _authServices = authServices;
            _config = config;
            _notify = notify;
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userMgr.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                return BadRequest(Utility.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
            }

            // check if user's email is confirmed
            if (await _userMgr.IsEmailConfirmedAsync(user))
            {
                // added for test
                bool rememberMe = false;

                var res = await _authServices.Login(model.Email, model.Password, rememberMe);
                if (!res.Status)
                {
                    ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                    return BadRequest(Utility.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
                }
                return Ok(Utility.BuildResponse(true, "Login is sucessful!", null, res));
            }

            ModelState.AddModelError("Invalid", "User must first confirm email before attempting to login");
            return BadRequest(Utility.BuildResponse<object>(false, "Email not confirmed", ModelState, null));

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            // check if user already exist
            var existingEmailUser = await _userServices.GetUserByEmail(model.Email);
            if (existingEmailUser != null)
            {
                ModelState.AddModelError("Invalid", $"User with email {model.Email} already exists");
                return BadRequest(Utility.BuildResponse<object>(false, "User already exists!", ModelState, null));
            }

            var user = _mapper.Map<User>(model);
            var addUserResponse = await _userServices.AddUser(user, model.Password);
            if (!addUserResponse.Succeeded)
            {
                foreach (var err in addUserResponse.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utility.BuildResponse<object>(false, "Failed to add user!", ModelState, null));
            }

            var addToRoleResponse = await _userServices.AddUserToRole(user, model.Role);
            if (!addToRoleResponse.Succeeded)
            {
                foreach (var err in addToRoleResponse.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utility.BuildResponse<string>(false, "Failed to add user role!", ModelState, null));
            }
            var claim = new Claim("AccountType", model.AccountType);
            await _userServices.AddUserClaim(user, claim);

            //  generate email confirmation token and url
            var token = await _authServices.GenerateEmailConfirmationToken(user);
            //var url = Url.Action("ConfirmEmail", "User", new { Email = user.Email, Token = token }, Request.Scheme);

            var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["email"] = model.Email;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            // Send an email to this new user to the email provided using a notification service
            var senderEmail = _config["ReturnPaths:SenderEmail"];
            await _notify.SendEmailAsync(senderEmail, user.Email, "Confirm your email address", urlString);

            var details = _mapper.Map<RegisterSuccessDTO>(user);

            // the confirmation link is added to this response object for testing purpose since at this point it is not being sent via mail
            return Ok(Utility.BuildResponse(true, "New user added! Check email for confirmation link.", null, new { details, ConfimationLink = urlString }));
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO model)
        {
            var user = await _userServices.GetUserByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("NotFound", $"User with email: {model.Email} was not found");
                return NotFound(Utility.BuildResponse<object>(false, "User not found!", ModelState, null));
            }

            var res = await _authServices.ConfirmEmail(user, model.Token);
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utility.BuildResponse<object>(false, "Failed to confirm email", ModelState, null));
            }

            return Ok(Utility.BuildResponse<object>(true, "Email confirmation suceeded!", null, null));
        }


    }
}
