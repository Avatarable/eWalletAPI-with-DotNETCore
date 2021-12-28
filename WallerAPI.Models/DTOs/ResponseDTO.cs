using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class ResponseDTO<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public IList<ErrorItem> Errors { get; set; }
        public T Data { get; set; }

        public ResponseDTO()
        {
            Errors = new List<ErrorItem>();
        }
    }

    public class ErrorItem
    {
        public string Key { get; set; }
        public IList<string> ErrorMessages { get; set; }
    }
}
