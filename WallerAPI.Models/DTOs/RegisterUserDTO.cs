using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is a required field.")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty")]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
        public string AccountType { get; set; }
    }
}
