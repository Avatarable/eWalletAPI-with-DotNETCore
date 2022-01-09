using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly UserManager<User> _userMgr;

        public AuthController(UserManager<User> userMgr, IAuthServices authServices)
        {
            _userMgr = userMgr;
            _authServices = authServices;
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
    }
}
