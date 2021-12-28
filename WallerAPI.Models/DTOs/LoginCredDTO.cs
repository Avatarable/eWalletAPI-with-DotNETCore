using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class LoginCredDTO
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public bool Status { get; set; }
    }
}
