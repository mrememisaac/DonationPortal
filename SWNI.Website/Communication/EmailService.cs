using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SWNI.Website.Communication
{
    public class EmailService
    {
        public static async Task<bool> Send(IdentityMessage message)
        {
            MailMessage msg = new MailMessage();
            msg.Body = message.Body;
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.From = new MailAddress(Constants.ApplicationConstants.Email.Account);
            using (var smtp = new SmtpClient())
            {
                if (HttpContext.Current.Request.Url.Authority.Contains("your-domain.name"))
                {
                    var credential = new NetworkCredential
                    {
                        UserName = Constants.ApplicationConstants.Email.Account,
                        Password = Constants.ApplicationConstants.Email.Password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Constants.ApplicationConstants.Email.Host;
                }
                else
                {
                    smtp.Host = Constants.ApplicationConstants.Email.OnlineHost;
                }
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(msg);
                return true;
            }
        }

        public static async Task<bool> Send(System.Net.Mail.MailMessage msg)
        {
            msg.From = new MailAddress(Constants.ApplicationConstants.Email.Account);
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Constants.ApplicationConstants.Email.Account,
                    Password = Constants.ApplicationConstants.Email.Password
                };
                smtp.Credentials = credential;
                smtp.Host = Constants.ApplicationConstants.Email.Host;
                smtp.Port = 25;// 587;
                smtp.EnableSsl = false;
                await smtp.SendMailAsync(msg);
                return true;
            }
            return false;
        }

        public static async void Send(MailMessage msg, string userName, string passWord)
        {
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = userName,
                    Password = passWord
                };
                smtp.Credentials = credential;
                smtp.Host = Constants.ApplicationConstants.Email.Host;
                smtp.Port = 25; // 587;
                smtp.EnableSsl = false;
                await smtp.SendMailAsync(msg);
            }
        }
    }
}
