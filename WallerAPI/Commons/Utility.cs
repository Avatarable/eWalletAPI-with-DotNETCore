using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Models.DTOs;

namespace WallerAPI.Commons
{
    public static class Utility
    {
        public  static ResponseDTO<T> BuildResponse<T>(bool status, string message, ModelStateDictionary errs, T data)
        {
            var listOfErrorItems = new List<ErrorItem>();

            if (errs != null)
            {
                foreach (var err in errs)
                {
                    var key = err.Key;
                    var errValues = err.Value;
                    var errList = new List<string>();
                    foreach (var errItem in errValues.Errors)
                    {
                        errList.Add(errItem.ErrorMessage);
                        listOfErrorItems.Add(new ErrorItem { Key = key, ErrorMessages = errList });
                    }
                }
            }

            var res = new ResponseDTO<T>
            {
                Status = status,
                Message = message,
                Data = data,
                Errors = listOfErrorItems
            };
            return res;
        }
    }
}
