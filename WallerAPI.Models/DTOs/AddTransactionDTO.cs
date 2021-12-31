using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Models.DTOs
{
    public class AddTransactionDTO
    {
        [Required]
        public decimal Amount { get; set; }
        public string Description { get; set; }

        [Required]
        public string WalletAddress { get; set; }
    }
}
