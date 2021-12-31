using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Models.DTOs
{
    public class TransferResponseDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string SenderWalletAddress { get; set; }
        public string ReceiverWalletAddress { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
