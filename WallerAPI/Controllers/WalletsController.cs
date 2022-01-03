using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WallerAPI.Commons;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly ILogger<WalletsController> _logger;
        private readonly IWalletServices _walletServices;
        private readonly IMapper _mapper;

        public WalletsController(IMapper mapper, IWalletServices walletServices, 
            ILogger<WalletsController> logger)
        {
            _mapper = mapper;
            _walletServices = walletServices;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllWallets()
        {
            var listOfWalletsToReturn = new List<WalletToReturnDTO>();

            var wallets = _walletServices.GetAllWallets();
            if (wallets != null)
            {
                foreach(var wallet in wallets)
                {
                    listOfWalletsToReturn.Add(_mapper.Map<WalletToReturnDTO>(wallet));
                }
                return Ok(Utility.BuildResponse(true, "List of wallets", null, listOfWalletsToReturn));
            }
            else
            {
                ModelState.AddModelError("Not found", "There was no record for wallets found!");
                return NotFound(Utility.BuildResponse<List<WalletToReturnDTO>>(false, "No results found!", ModelState, null));
            }
        }

        [HttpGet("{walletAddress}")]
        public IActionResult GetWalletByAddress(string walletAddress)
        {
            //TODO check for empty input

            var wallet = _walletServices.GetWalletByAddress(walletAddress);
            if (wallet != null)
            {
                var walletToReturn = _mapper.Map<WalletToReturnDTO>(wallet);
                return Ok(Utility.BuildResponse(true, "Wallelt", null, walletToReturn));
            }
            else
            {
                ModelState.AddModelError("Not found", $"There was no record for wallet with address {walletAddress} found!");
                return NotFound(Utility.BuildResponse<Wallet>(false, "No results found!", ModelState, null));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddWallet(AddWalletDTO model)
        {
            // Get current user's id
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var wallet = _walletServices.AddWallet(model.WalletCurrency, currentUserId);
            if (wallet != null)
            {
                var walletToReturn = _mapper.Map<WalletToReturnDTO>(wallet);
                return Ok(Utility.BuildResponse(true, "Wallet", null, walletToReturn));
            }
            else
            {
                ModelState.AddModelError("Not found", "Wallet could not be added!");
                return NotFound(Utility.BuildResponse<Wallet>(false, "Failed to add wallet!", ModelState, null));
            }
        }
    }
}
