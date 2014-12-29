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
            StreamText streamText;
            
            Config.InitConfig();
            Connection.InitConnection();

            Connection.SendMessage("Now reporting World of Warcraft encounters.", true);

            Log combatLog = new Log();

            if (Config.WriteStream)
                streamText = new StreamText();

            Console.ReadKey();
        }
    }
}
