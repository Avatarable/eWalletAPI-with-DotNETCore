using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WallerAPI.Services.Interfaces
{
    public interface INotificationServices
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
