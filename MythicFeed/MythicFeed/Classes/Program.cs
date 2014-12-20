using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace MythicFeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Bosses.InitDictionary();
            Connection.InitConnection();

            Connection.SendMessage("Now reporting highmaul encounter completion.");

            Log combatLog = new Log();

            Console.ReadKey();
        }
    }
}
