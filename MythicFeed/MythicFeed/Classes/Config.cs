using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TweetSharp;

namespace MythicFeed
{
    internal static class Config //I have so many FUCKING singletons
    {
        internal static string ConsumerKey;
        internal static string ConsumerSecret;
        internal static string LogPath;
        internal static string StreamPath;

        internal static bool WriteStream;
        internal static bool IgnoreLFR;

        internal static void InitConfig()
        {
            if (!File.Exists("Config.ini"))
            {
                using (StreamWriter sw = new StreamWriter("Config.ini"))
                {
#if DEBUG
                    sw.WriteLine("[Auth]");
                    sw.WriteLine("ConsumerKey = 123456789");
                    sw.WriteLine("ConsumerSecret = 123456789");
                    sw.WriteLine();
#endif

                    sw.WriteLine("[FileSystem]");
                    sw.WriteLine("LogPath = C:/Program Files (x86)/World of Warcraft/Logs/WoWCombatLog.txt");
                    sw.WriteLine("StreamPath = C:/StreamOut.txt");

                    sw.WriteLine();
                    sw.WriteLine("[Flags]");
                    sw.WriteLine("IgnoreLFR = 1");
                    sw.WriteLine("WriteStream = 1");
                }
                Console.WriteLine("You do not have your configuration set up yet! Please fill out Config.ini before continuing.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            ReadConfig();
        }

        internal static void ReadConfig()
        {
            using (StreamReader sr = new StreamReader("Config.ini"))
            {
                string line;
                string[] path;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Split(' ')[0])
                    {
#if DEBUG
                        case "consumerKey":
                            ConsumerKey = line.Split(' ')[2];
                            break;
                        case "consumerSecret":
                            ConsumerSecret = line.Split(' ')[2];
                            break;
#endif
                        case "LogPath":
                            path = line.Split(' ');
                            LogPath = string.Join(" ", path.Skip(2));
                            break;

                        case "StreamPath":
                            path = line.Split(' ');
                            StreamPath = string.Join(" ", path.Skip(2));
                            break;

                        case "IgnoreLFR":
                            if (int.Parse(line.Split(' ')[2]) == 1)
                                IgnoreLFR = true;
                            else
                                IgnoreLFR = false;
                            break;

                        case "WriteStream":
                            if (int.Parse(line.Split(' ')[2]) == 1)
                                WriteStream = true;
                            else
                                WriteStream = false;
                            break;
                    }
                }
            }
#if RELEASE
            ConsumerKey = "";
            ConsumerSecret = "";
#endif
        }
    }
}
