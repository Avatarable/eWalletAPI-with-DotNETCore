using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WallerAPI.Models.Domain
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be between 3 and 15")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be between 3 and 15")]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public Photo Photo { get; set; }
        public IList<Wallet> Wallets { get; set; }

        public User()
        {
            Wallets = new List<Wallet>();
        }
    }
}
