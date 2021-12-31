using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class AddWalletDTO
    {
        [Required(ErrorMessage = "Default currency field is required")]
        public string WalletCurrency { get; set; }
    }
}
