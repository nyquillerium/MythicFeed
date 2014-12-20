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
        private string systemPath = Config.LogPath;

        private string parseDifficulty(int diff)
        {
            switch (diff)
            {
                case 14:
                    return "(Normal)";
                case 15:
                    return "(Heroic)";
                case 16:
                    return "(Mythic)";
            }
            return "";
        }

        public Log()
        {
            using (var fs = new FileStream(systemPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            using (var reader = new StreamReader(fs))
            {
                string currentEncounter;
                while (true)
                {
                    var line = reader.ReadLine();

                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string name;
                        string timeStamp;
                        switch (line.Split(',')[0].Split(' ')[3])
                        {
                            case "ENCOUNTER_START":
                                name = line.Split(',')[2];
                                timeStamp = line.Split(',')[0].Split(' ')[2];
                                currentEncounter = name;
                                Connection.SendMessage("Encounter of " + name + parseDifficulty(int.Parse(line.Split(',')[3])) + " started at " + timeStamp + ".", true);
                                break;

                            case "ENCOUNTER_END":
                                name = line.Split(',')[2];
                                break;

                            case "UNIT_DIED":
                                string unitName = line.Split(',')[6];
#if DEBUG
                                Console.WriteLine(unitName + " died");
#endif
                                break;
                        }
                    }
                }
            }
        }
    }
}
