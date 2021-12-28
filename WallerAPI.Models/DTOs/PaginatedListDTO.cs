using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class PaginatedListDTO<T>
    {
        public PageMeta MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedListDTO()
        {
            Data = new List<T>();
        }
    }

    public class PageMeta
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
