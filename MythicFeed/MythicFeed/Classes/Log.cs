using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using TweetSharp;

namespace MythicFeed
{
    public class Log
    {
        private string systemPath = "C:/Program Files (x86)/World of Warcraft/Logs/WoWCombatLog.txt";

        public Log()
        {
            using (var fs = new FileStream(systemPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            using (var reader = new StreamReader(fs))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        if (line.Split(',')[0].Split(' ')[3] == "UNIT_DIED")
                            {
                                string unitName = line.Split(',')[6];
#if DEBUG
                                Console.WriteLine(unitName + " GOT FUCKING WRECKED");
#endif
                                if (Bosses.Dick.ContainsKey(unitName))
                                {
                                    Connection.SendMessage(unitName + "(Normal) has been downed!"); //wow this is ghetto as fug
                                }
                            }
                        //Console.WriteLine("EVENT: " + line);
                    }
                }
            }
        }

    }
}
