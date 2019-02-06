using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailDemo.Models
{
   public interface IEmailSender
    {
        Task SendEmailAsync(List<string> email, string subject, string message);

    }
}
