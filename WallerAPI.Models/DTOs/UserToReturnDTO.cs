using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class UserToReturnDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
