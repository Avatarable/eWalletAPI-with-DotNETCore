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
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionServices _transactionServices;
        private readonly IMapper _mapper;

        public TransactionsController(IMapper mapper, ITransactionServices transactionServices, 
            ILogger<TransactionsController> logger)
        {
            _mapper = mapper;
            _transactionServices = transactionServices;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionServices.GetAllTransactions();
            if (transactions != null)
            {
                return Ok(Utility.BuildResponse(true, "List of transactions", null, transactions));
            }
            else
            {
                ModelState.AddModelError("Not found", "There was no record for currencies found!");
                return NotFound(Utility.BuildResponse<List<Transaction>>(false, "No results found!", ModelState, null));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(string id)
        {
            //TODO check for empty input

            var transaction = _transactionServices.GetTransactionById(id);
            if (transaction != null)
            {
                return Ok(Utility.BuildResponse(true, "Currency", null, transaction));
            }
            else
            {
                ModelState.AddModelError("Not found", $"There was no record for transaction with id {id} found!");
                return NotFound(Utility.BuildResponse<Transaction>(false, "No results found!", ModelState, null));
            }
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(AddTransactionDTO model)
        {
            //return Ok(_transactionServices.Deposit(model.Amount, model.Description, model.WalletAddress));
            
            var transaction = _transactionServices.Deposit(model.Amount, model.Description, model.WalletAddress);

            if (transaction != null)
            {
                var res = _mapper.Map<TransactionResponseDTO>(transaction);
                //return CreatedAtAction("Transaction", Utility.BuildResponse(true, "Transaction", null, res));
                return Ok(Utility.BuildResponse(true, "Transaction", null, res));
            }
            else
            {
                ModelState.AddModelError("Failed", "Deposit could not be made!");
                return NotFound(Utility.BuildResponse<TransactionResponseDTO>(false, "Failed to add transaction!", ModelState, null));
            }
        }
        
        [HttpPost("withdraw")]
        public IActionResult Withdraw(AddTransactionDTO model)
        {
            var transaction = _transactionServices.Withdraw(model.Amount, model.Description, model.WalletAddress);
            if (transaction != null)
            {
                var res = _mapper.Map<TransactionResponseDTO>(transaction);
                return Ok(Utility.BuildResponse(true, "Transaction", null, res));
            }
            else
            {
                ModelState.AddModelError("Failed", "Withdrawal could not be made!");
                return NotFound(Utility.BuildResponse<TransactionResponseDTO>(false, "Failed to add transaction!", ModelState, null));
            }
        }
        
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(MakeTransferDTO model)
        {
            var transaction = await _transactionServices.Transfer(model.Amount, model.Description, model.ReceiverWalletAddress, model.SenderWalletAddress);
            if (transaction != null)
            {
                var res = _mapper.Map<TransferResponseDTO>(transaction);
                return Ok(Utility.BuildResponse(true, "Transfer Transaction", null, res));
            }
            else
            {
                ModelState.AddModelError("Failed", "Transaction could not be made!");
                return Ok(Utility.BuildResponse<TransferResponseDTO>(false, "Failed to make transfer!", ModelState, null));
            }
        }

    }
}
