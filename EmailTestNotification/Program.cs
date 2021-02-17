using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailNotification.EmailNotification;
using System.Configuration;

namespace EmailTestNotification
{
    class Program
    {
        static void Main(string[] args)
        {

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            string _SMTPFromUsername = ConfigurationManager.AppSettings["SMTPFromUsername"];
            string _SMTPFromPassword = ConfigurationManager.AppSettings["SMTPFromPassword"];
            string _SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            string _SMTPPort = ConfigurationManager.AppSettings["SMTPPort"];
            EmailNotification.EmailNotification.EmailNotification _emailNotification = new EmailNotification.EmailNotification.EmailNotification("Suggestion Notification",_SMTPFromUsername, _SMTPFromPassword, _SMTPHost, _SMTPPort, false);
            List<string> _emailto = new List<string>() { "" };
            List<string> _emailCC = new List<string>() { "", "" };
            bool result = await _emailNotification.SendSMTPEmail(_emailto,_emailCC, "Test", "Please Ignore My Mail Testing New WorkFlow Notification");

            Console.ReadKey();
        }
    }
}
