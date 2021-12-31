using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class WalletToReturnDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
