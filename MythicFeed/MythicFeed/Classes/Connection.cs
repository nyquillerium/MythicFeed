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
            service = new TwitterService(Config.ConsumerKey, Config.ConsumerSecret);

            OAuthRequestToken requestToken = service.GetRequestToken();

            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());

            Console.WriteLine("Enter auth verifier:");
            string verifier = Console.ReadLine(); 
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            service.AuthenticateWith(access.Token, access.TokenSecret);
            //TODO: Auth error checking.
            Console.WriteLine("Authenticated!");
            Console.WriteLine();

            VerifyCredentialsOptions userOptions = new VerifyCredentialsOptions();
            user = service.VerifyCredentials(userOptions);
        }

        public static void SendMessage(string message, bool tweet = false)
        {
            if (tweet)
            {
                SendTweetOptions options = new SendTweetOptions();
                options.Status = string.Format("[MythicFeed] " + message + " @{0}", user.ScreenName);
                service.SendTweet(options);
            }
            
            Console.WriteLine(message);
        }
    }
}
