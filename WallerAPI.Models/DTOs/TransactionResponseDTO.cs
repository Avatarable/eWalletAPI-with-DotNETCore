using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Models.DTOs
{
    public class TransactionResponseDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string WalletAddress { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
