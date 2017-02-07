using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SWNI.Website.Communication
{
    public class TextService
    {
        public static async Task<bool> Send(IdentityMessage msg)
        {
            TextMessage txt = new TextMessage() { Message = msg.Body, To = msg.Destination };
            return await SendMessage(txt, await CreateSession());
        }

        static string sessionId { get; set; }

        internal static async Task<string> CreateSession(string url = "https://api.clickatell.com/http/auth?", string key = "your-clickatell-key", string username = "your-clickatell-username", string password = "your-clickatell-password")
        {
            string sessionId = "";
            WebClient client = new WebClient();
            try
            {
                var result = client.OpenRead(string.Format("{0}api_id={1}&user={2}&password={3}", url, key, username, password));
                var reading = Read(result);
                if (reading.ToString().StartsWith("OK:"))
                {
                    sessionId = reading.ToString().Substring(4);
                }
                else
                {
                    //log error
                }
            }
            catch (Exception ex)
            {
                //log error
            }
            return sessionId;
        }

        internal static bool KeepAlive(string sessionId)
        {
            bool isAlive = false;
            WebClient client = new WebClient();
            var result = client.OpenRead(string.Format("https://api.clickatell.com/http/ping={0}", sessionId));
            var reading = Read(result);
            if (reading.ToString().StartsWith("OK:"))
            {
                isAlive = true;
            }
            else
            {
                //log error
            }
            return isAlive;
        }

        internal static async Task<bool> SendMessage(TextMessage msg, string sessionId = "")
        {
            if (sessionId == "")
            {
                sessionId = await CreateSession();
            }
            bool sent = false;
            WebClient client = new WebClient();
            var result = client.OpenRead(
                string.Format("https://api.clickatell.com/http/sendmsg?session_id={0}&to={1}&text={2}&cliMsgId={3}", sessionId, ApplyPrefix(msg.To), msg.Message, msg.Id));
            var reading = Read(result);
            if (reading.ToString().StartsWith("ID:"))
            {
                sent = true;
            }
            else
            {
                //log error
            }
            return sent;

        }

        private static async Task<string> Read(Stream result)
        {
            StreamReader reader = new StreamReader(result);
            var reading = await Task.Run(() => reader.ReadToEnd());
            return reading;
        }

        public class TextMessage
        {
            public int Id { get; set; }
            public string To { get; set; }
            public string Message { get; set; }
        }

        public static bool IsValidMobile(string mobile)
        {
            bool valid = false;
            if (mobile.Length > 10 && mobile.Length <= 13)
            {
                valid = true;
            }
            return valid;
        }

        public static string ApplyPrefix(string mobileNumber)
        {
            mobileNumber = mobileNumber.Trim();

            if (mobileNumber.StartsWith("0"))
            {
                return string.Format("+234{0}", mobileNumber.Substring(1));
            }
            else if (!mobileNumber.StartsWith("+"))
            {
                return string.Format("+{0}", mobileNumber);
            }

            else return mobileNumber;
        }
    }
}
