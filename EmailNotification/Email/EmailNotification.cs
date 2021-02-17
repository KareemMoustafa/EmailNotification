using EmailNotification.IEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.EmailNotification
{
    public class EmailNotification : IEmailNotification
    {

        #region "PROP DECLARTION"
        private string _fromAddress { get; set; }
        private string _sMTPFromPassword { get; set; }
        private string _sMTPHost { get; set; }
        private string _sMTPPort { get; set; }
        private bool _enableSsl { get; set; }
        private string _displayNameS { get; set; }

        #endregion


        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmailNotification()
        {

        }


        /// <summary>
        /// Parameter Authorization Constructor
        /// </summary>
        /// <param name="FromAddress">Email From</param>
        /// <param name="SMTPFromPassword">PASSWORD</param>
        /// <param name="SMTPHost">HOST SMTP</param>
        /// <param name="SMTPPort">PORT</param>
        /// <param name="EnableSsl">SSl</param>
        public EmailNotification(string DisplaynameSender, string FromAddress, string SMTPFromPassword, string SMTPHost, string SMTPPort, bool EnableSsl)
        {
            _fromAddress = FromAddress;
            _displayNameS = DisplaynameSender;
            _sMTPFromPassword = SMTPFromPassword;
            _sMTPHost = SMTPHost;
            _sMTPPort = SMTPPort;
            _enableSsl = EnableSsl;

        }


        /// <summary>
        /// Send Email To Target list Users
        /// </summary>
        /// <param name="ToAddresses">Send To List Users</param>
        /// <param name="Subject">Subject of email</param>
        /// <param name="Body">Body of Email Convert To String </param>
        /// <returns></returns>
        public async Task<bool> SendSMTPEmail(List<string> ToAddresses, List<string> CcAddresses, string Subject, string Body)
        {
            // Initialization.  
            bool isSend = false;  
            try
            {
                using (SmtpClient _SMTPClient = new SmtpClient())
                {
                    NetworkCredential _credential = new NetworkCredential(_fromAddress, _sMTPFromPassword);
                    _SMTPClient.UseDefaultCredentials = false;
                    _SMTPClient.Credentials = _credential;
                    _SMTPClient.Host = _sMTPHost;
                    _SMTPClient.Port = Convert.ToInt32(_sMTPPort);
                    _SMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    _SMTPClient.EnableSsl = _enableSsl;

                    foreach (string toAdd in ToAddresses)
                    {
                        if (IsValidEmail(toAdd))
                        {
                            MailAddress From = new MailAddress(_fromAddress, _displayNameS);
                            MailAddress To = new MailAddress(toAdd);
                            MailMessage mailObj = new MailMessage(From, To);
                            foreach (var ccAdd in CcAddresses)
                            {
                                if (IsValidEmail(ccAdd) == true) mailObj.CC.Add(ccAdd);   
                            }
                            mailObj.Subject = Subject;
                            mailObj.Body = Body;
                            //If want adding attachment files
                            //mailObj.Attachments.Add(new Attachment("Hydrangeas.jpg"));
                            mailObj.IsBodyHtml = true;
                            await _SMTPClient.SendMailAsync(mailObj); 
                        }
                    }
                    // Settings.  
                    isSend = true;  
                }                                               
            }
            catch (Exception ex)
            {
                return false;  
            }

             // info.  
            return isSend;  
        }


        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
