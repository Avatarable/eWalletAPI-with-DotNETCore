using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WallerAPI.Models.DTOs
{
    public class AddCurrencyDTO
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abrreviation is a required field.")]
        public string Abbreviation { get; set; }
    }
}
