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
                string startTime;
                while (true)
                {
                    var line = reader.ReadLine();

                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string name;
                        string timeStamp;
                        int difficulty;
                        switch (line.Split(',')[0].Split(' ')[2])
                        {
                            case "ENCOUNTER_START":
                                name = line.Split(',')[2];
                                timeStamp = line.Split(',')[0].Split(' ')[1];
                                difficulty = int.Parse(line.Split(',')[3]);

                                if (difficulty == 7 && Config.IgnoreLFR)
                                    break;

                                Connection.SendMessage("Encounter of " + name + parseDifficulty(difficulty) + " started at " + timeStamp + ".", true);
                                startTime = timeStamp;
                                break;

                            case "ENCOUNTER_END":
                                name = line.Split(',')[2];
                                timeStamp = line.Split(',')[0].Split(' ')[2];
                                difficulty = int.Parse(line.Split(',')[3]);

                                if (difficulty == 7 && Config.IgnoreLFR || line.Split(',')[5] == "0") //do not report if wipe
                                    break;

                                //TODO: ADD DURATION LOGIC
                                //float duration = float.Parse(timeStamp.Split(':')[2] )

                                Connection.SendMessage(name + parseDifficulty(difficulty) + " downed at " + timeStamp + "!", true);
                                break;
#if DEBUG
                            case "UNIT_DIED":
                                string unitName = line.Split(',')[6];
                                Console.WriteLine(unitName + " died");
                                break;
#endif
                        }
                    }
                }
            }
        }
    }
}
