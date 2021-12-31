using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class MakeTransferDTO
    {
        [Required]
        public decimal Amount { get; set; }
        public string Description { get; set; }

        [Required]
        public string SenderWalletAddress { get; set; }

        [Required]
        public string ReceiverWalletAddress { get; set; }
    }
}
