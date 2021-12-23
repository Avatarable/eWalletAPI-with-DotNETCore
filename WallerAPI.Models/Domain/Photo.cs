using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.Domain
{
    public class Photo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; }
        public string PublicId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
