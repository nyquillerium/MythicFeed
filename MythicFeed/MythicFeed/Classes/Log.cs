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
                        switch (line.Split(',')[0].Split(' ')[3])
                        {
                            case "ENCOUNTER_START":
                                int id = int.Parse(line.Split(',')[1]);
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
