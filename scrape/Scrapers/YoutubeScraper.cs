using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Threading;

namespace scrape
{
    class YoutubeScraper
    {
        private Dictionary<string, string> XPaths;
        private SimpleScraper Scraper;
        private IJavaScriptExecutor js;
        private bool First = true;
        public YoutubeScraper(SimpleScraper scraper)
        {
            XPaths = new Dictionary<string, string>();
            XPaths.Add("agreeButton", "//tp-yt-paper-dialog//*[contains(@class, 'footer')]//*[contains(@class, 'buttons')]//child::ytd-button-renderer[2]//a");
            XPaths.Add("searchBar", "//input[@id = 'search']");
            XPaths.Add("searchBox", "//ytd-searchbox");
            XPaths.Add("filterButton", "//div[@id = 'filter-menu']//*[@id = 'button']");
            XPaths.Add("filterDate", "//ytd-search-filter-group-renderer[last()]//ytd-search-filter-renderer[2]//a");
            XPaths.Add("videos", "//ytd-video-renderer");
            XPaths.Add("title", ".//a[@id = 'video-title']");
            XPaths.Add("views", ".//div[@id = 'metadata-line']//child::span[1]");
            XPaths.Add("channel", ".//div[@id = 'channel-info']//div[@id = 'text-container']//a");
            Scraper = scraper;
            js = (IJavaScriptExecutor)Scraper.Driver;
            //Scraper.GoTo("https://www.youtube.com/");
            //Scraper.CheckAndClick(XPaths["agreeButton"]);

        }

        public Tuple<YoutubeSearch, List<YoutubeVideo>> GetVideos(string searchTerm)
        {
            Scraper.GoTo("https://www.youtube.com/");
            if(First)
            {
                Scraper.CheckAndClick(XPaths["agreeButton"]);
            }
            //Scraper.CheckAndClick(XPaths["agreeButton"]);
            try
            {
                Scraper.GetElement(XPaths["searchBar"]).SendKeys(searchTerm + Keys.Enter);
            }
            catch
            {
                Scraper.CheckAndClick(XPaths["searchBox"]);
                Scraper.GetElement(XPaths["searchBar"]).SendKeys(searchTerm + Keys.Enter);
            }
            
            
            Scraper.CheckAndClick(XPaths["filterButton"]);
            Scraper.CheckAndClick(XPaths["filterDate"]);
            Scraper.Refresh();
            var videos = Scraper.GetElements(XPaths["videos"]);


            var youtubeSearch = new YoutubeSearch
            {
                SearchTerm = searchTerm
            };

            var videoList = new List<YoutubeVideo>();

            //foreach (IWebElement video in videos)
            for(int i=0; i < 5; i++)
            {
                var video = videos.ElementAt(i);

                var videoInfo = new YoutubeVideo();

                var titleInfo = video.FindElement(By.XPath(XPaths["title"]));
                videoInfo.Title = titleInfo.GetAttribute("title");
                videoInfo.Link = titleInfo.GetAttribute("href");


                var viewsInfo = video.FindElement(By.XPath(XPaths["views"]));
                var views = viewsInfo.Text.Split(" ")[0];
                videoInfo.Views = views;

                var channelInfo = video.FindElement(By.XPath(XPaths["channel"]));
                videoInfo.Channel = channelInfo.Text;
                videoInfo.ChannelLink = channelInfo.GetAttribute("href");

                videoList.Add(videoInfo);

                //js.ExecuteScript("console.log(arguments[0])", video);

            }
            

            First = false;
            return Tuple.Create(youtubeSearch, videoList);

        } 
    }
}
