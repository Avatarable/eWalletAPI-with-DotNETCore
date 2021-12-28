using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Commons;
using WallerAPI.Helpers;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger,
             IMapper mapper, IUserServices userServices)
        {
            _logger = logger;
            _mapper = mapper;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterUserDTO model)
        {
            // check if user already exist
            var existingEmailUser = await _userServices.GetUserByEmail(model.Email);
            if(existingEmailUser != null)
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

            //  generate email confirmation token and url
            var token = await _userServices.GenerateEmailConfirmationToken(user);
            var url = Url.Action("ConfrimEmail", "User", new { Email = user.Email, Token = token }, Request.Scheme);

            // TODO:  send an email to this new user to the email provided using a notification service

            var details = _mapper.Map<RegisterSuccessDTO>(user);

            // the confirmation link is added to this response object for testing purpose since at this point it is not being sent via mail
            return Ok(Utility.BuildResponse(true, "New user added!", null, new { details, ConfimationLink = url }));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("Invalid", "UserId and token is required");
                return BadRequest(Utility.BuildResponse<object>(false, "UserId or token is empty!", ModelState, null));
            }

            var user = await _userServices.GetUserByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("NotFound", $"User with email: {email} was not found");
                return NotFound(Utility.BuildResponse<object>(false, "User not found!", ModelState, null));
            }

            var res = await _userServices.ConfirmEmail(user, token);
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


        [HttpGet]
        public IActionResult GetAllUsers(int page, int perPage)
        {
            var listOfUsersToReturn = new List<UserToReturnDTO>();

            var users = _userServices.GetAllUsers();
            if (users != null)
            {
                var pagedList = PagedList<User>.Paginate(users, page, perPage);
                foreach (var user in pagedList.Data)
                {
                    listOfUsersToReturn.Add(_mapper.Map<UserToReturnDTO>(user));
                }

                var res = new PaginatedListDTO<UserToReturnDTO>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsersToReturn
                };

                return Ok(Utility.BuildResponse(true, "List of users", null, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for users found!");
                var res = Utility.BuildResponse<List<UserToReturnDTO>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            var userToReturn = new UserToReturnDTO();
            var user = await _userServices.GetUserByEmail(email);
            if (user != null)
            {
                userToReturn = _mapper.Map<UserToReturnDTO>(user);
                var res = Utility.BuildResponse(true, "User details", null, userToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with email {user.Email}");
                return NotFound(Utility.BuildResponse<List<UserToReturnDTO>>(false, "No result found!", ModelState, null));
            }
        }

    }
}
