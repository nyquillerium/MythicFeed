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
            Config.InitConfig();
            Connection.InitConnection();

            //Connection.SendMessage("Now reporting highmaul encounter completion.", true);

            Log combatLog = new Log();

            Console.ReadKey();
        }
    }
}
