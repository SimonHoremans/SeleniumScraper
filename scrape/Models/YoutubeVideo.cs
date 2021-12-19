using System;
using System.Collections.Generic;
using System.Text;

namespace scrape
{
    class YoutubeVideo
    {
        public string Title { get; set; }
        public string Channel { get; set; }
        public string Link { get; set; }
        public string ChannelLink { get; set; }
        public string Views { get; set; }
        public int YoutubeVideoID { get; set; }
        public int YoutubeSearchID { get; set; }
    }
}
