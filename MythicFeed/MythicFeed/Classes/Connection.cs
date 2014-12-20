using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using TweetSharp;

namespace MythicFeed
{
    internal static class Connection
    {
        private static TwitterUser user;
        private static TwitterService service;

        public static void InitConnection()
        {
            string consumerKey = "";
            string consumerSecret = "";

            if (!File.Exists("Config.ini"))
            {
                using (StreamWriter sw = new StreamWriter("Config.ini"))
                {
                        sw.WriteLine("[Auth]");
                        sw.WriteLine("consumerKey = 123456789");
                        sw.WriteLine("consumerSecret = 123456789");
                }
                Console.WriteLine("You do not have your configuration set up yet! Please fill out Config.ini before continuing.");
                Console.ReadKey();
            }

            using (StreamReader sr = new StreamReader("Config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Split(' ')[0])
                    {
                        case "consumerKey":
                            consumerKey = line.Split(' ')[2];
                            break;
                        case "consumerSecret":
                            consumerSecret = line.Split(' ')[2];
                            break;
                    }

                }
            }

            service = new TwitterService(consumerKey, consumerSecret);

            OAuthRequestToken requestToken = service.GetRequestToken();

            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());

            Console.WriteLine("Enter auth verifier:");
            string verifier = Console.ReadLine(); 
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);
            Console.WriteLine("Authenticated!");
            Console.WriteLine();

            service.AuthenticateWith(access.Token, access.TokenSecret);

            VerifyCredentialsOptions userOptions = new VerifyCredentialsOptions();
            user = service.VerifyCredentials(userOptions);
        }

        public static void SendMessage(string Message)
        {
            SendTweetOptions options = new SendTweetOptions();
            options.Status = string.Format("[MythicFeed] " + Message + " @{0}", user.ScreenName);
            service.SendTweet(options);
        }
    }
}
