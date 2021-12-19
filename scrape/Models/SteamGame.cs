using System;
using System.Collections.Generic;
using System.Text;

namespace scrape
{
    class SteamGame
    {
        public int SteamGameID { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int SteamSearchID { get; set; }
    }
}
