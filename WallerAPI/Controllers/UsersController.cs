using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public UsersController(ILogger<UsersController> logger, IMapper mapper,
             IUserServices userServices)
        {
            _logger = logger;
            _mapper = mapper;
            _userServices = userServices;
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
                ModelState.AddModelError("Not found", "There was no record for users found!");
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

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userServices.GetUserById(userId);
            if(user != null)
            {
                _userServices.RemoveUser(user);
                return Ok();
            }
            ModelState.AddModelError("Notfound", $"There was no record found for user with id {userId}");
            return NotFound(Utility.BuildResponse<string>(false, "No result found!", ModelState, null));
        }

    }
}
