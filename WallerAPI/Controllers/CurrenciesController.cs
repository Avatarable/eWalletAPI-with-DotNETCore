using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class CurrenciesController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ICurrencyServices _currencyServices;
        private readonly IMapper _mapper;

        public CurrenciesController(ILogger<UsersController> logger, 
            ICurrencyServices currencyServices, IMapper mapper)
        {
            _logger = logger;
            _currencyServices = currencyServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            var currencies = _currencyServices.GetCurrencies();
            if(currencies != null)
            {
                return Ok(Utility.BuildResponse(true, "List of currencies", null, currencies));
            }
            else
            {
                ModelState.AddModelError("Not found", "There was no record for currencies found!");
                return NotFound(Utility.BuildResponse<List<Currency>>(false, "No results found!", ModelState, null));
            }
        }

        [HttpGet("{currencyName}")]
        public IActionResult GetCurrencyByName(string currencyName)
        {
            //TODO check for empty input

            var currency = _currencyServices.GetCurrencyByName(currencyName);
            if (currency != null)
            {
                return Ok(Utility.BuildResponse(true, "Currency", null, currency));
            }
            else
            {
                ModelState.AddModelError("Not found", $"There was no record for currency with name {currencyName} found!");
                return NotFound(Utility.BuildResponse<Currency>(false, "No results found!", ModelState, null));
            }
        }

        [HttpPost]
        public IActionResult AddCurrency(AddCurrencyDTO model)
        {
            var currency = _currencyServices.AddCurrency(model.Name, model.Abbreviation);
            if (currency != null)
            {
                return Ok(Utility.BuildResponse(true, "Currency", null, currency));
            }
            else
            {
                ModelState.AddModelError("Not found", "Currency could not be added!");
                return NotFound(Utility.BuildResponse<Currency>(false, "Failed to add currency!", ModelState, null));
            }
        }
    }
}
