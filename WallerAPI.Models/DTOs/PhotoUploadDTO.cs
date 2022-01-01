using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class PhotoUploadDTO
    {
        public IFormFile Photo { get; set; }

        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}
