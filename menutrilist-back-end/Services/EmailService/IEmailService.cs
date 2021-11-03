using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menutrilist.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string to, string subject, string html, string from = null);
    }
}
