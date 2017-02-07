using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWNI.Website.Constants
{
    public static class ApplicationConstants
    {
        public static string Name { get { return "Online Donation Portal"; } }

        public static string Description { get { return "Show some love! Make a donation!! Save lives!!!"; } }

        public static string[] Roles
        {
            get
            {
                string[] roles = { "Admin", "Executive", "Cost Admin", "User" };
                return roles;
            }
        }

        public static class Email
        {

            public static string Account
            {
                get
                {
                    return "website-email-address@your-domain.name";
                }
            }

            public static string Host
            {
                get
                {
                    return "mail.your-domain.name";
                }
            }

            public static string OnlineHost
            {
                get
                {
                    return "your-online-mail-server.com";
                }
            }

            public static string Password
            {
                get
                {
                    return "your-online-mail-server-password";
                }
            }
        }
    }
}
