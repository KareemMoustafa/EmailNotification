using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.IEmail
{
    interface IEmailNotification
    {
        Task<bool> SendSMTPEmail(List<string> ToAddresses, List<string> CcAddresses, string Subject, string Body);

        bool IsValidEmail(string email);

    }
}
