using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicFeed
{
    public static class Bosses
    {
        /* This is needed temporarily until blizzard figures out ENCOUNTER_START better */
        public static Dictionary<string, int> Dick;
        
        public static void InitDictionary()
        {
            //initialize boss names
            Dick = new Dictionary<string, int>();
            Dick.Add("Kargath Bladefist", 1);
            Dick.Add("The Butcher", 2);
            Dick.Add("Brackenspore", 3);
            Dick.Add("Tectus", 4);
            Dick.Add("Twin Ogron", 5);
            Dick.Add("Ko'ragh", 6);
            Dick.Add("Imperator Mar'gok", 7);
        }

    }
}
