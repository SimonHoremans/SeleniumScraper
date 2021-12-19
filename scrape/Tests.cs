using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace scrape
{
    class Tests
    {
        public static void scrapeDBYoutube()
        {
            

            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";

            var dbManager = new DBManager(connectionStringTemplate, relativePath);
            var sharedScraper = new SimpleScraper();
            var youtubeScraper = new YoutubeScraper(sharedScraper);

            var youtubeResult = youtubeScraper.GetVideos("toast");
            var youtubeResultString = JsonConvert.SerializeObject(youtubeResult);

            var youtubeSearchReturn = dbManager.InsertYoutube(youtubeResult.Item1, youtubeResult.Item2);
            var videosQuery = dbManager.GetYoutubeVideos(youtubeSearchReturn);

            Console.WriteLine(videosQuery.Count);

            foreach (YoutubeVideo video in videosQuery)
            {
                foreach (PropertyInfo info in video.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(video);
                    Console.WriteLine("{0}={1}", name, value);
                }
                Console.WriteLine("");
            }


        }

        public static void scrapeGetYoutubeSearches()
        {
            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";

            var dbManager = new DBManager(connectionStringTemplate, relativePath);

            var youtubeSearches = dbManager.GetYoutubeSearches();

            Console.WriteLine(JsonConvert.SerializeObject(youtubeSearches));
        }

        public static void scrapeDBIndeed()
        {
            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";

            var dbManager = new DBManager(connectionStringTemplate, relativePath);
            var sharedScraper = new SimpleScraper();
            var indeedScraper = new IndeedScraper(sharedScraper);

            var indeedResult = indeedScraper.GetJobs("developer", "brussel");
            var indeedResultString = JsonConvert.SerializeObject(indeedResult);

            var indeedSearchReturn = dbManager.InsertIndeed(indeedResult.Item1, indeedResult.Item2);
            var jobsQuery = dbManager.GetIndeedJobs(indeedSearchReturn);

            Console.WriteLine(jobsQuery.Count);

            foreach (IndeedJob video in jobsQuery)
            {
                foreach (PropertyInfo info in video.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(video);
                    Console.WriteLine("{0}={1}", name, value);
                }
                Console.WriteLine("");
            }


        }

        public static void scrapeDBSteam()
        {
            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";

            var dbManager = new DBManager(connectionStringTemplate, relativePath);
            var sharedScraper = new SimpleScraper();
            var steamScraper = new SteamScraper(sharedScraper);

            var steamResult = steamScraper.GetGames("", false, "reviews", new List<string> { "indie" }, new List<string> { "singleplayer" });
            var steamResultString = JsonConvert.SerializeObject(steamResult);

            Console.WriteLine(CommandlineInterface.ConvertResultToString(steamResult));

            //Console.WriteLine(steamResultString);

            var steamSearchReturn = dbManager.InsertSteam(steamResult.Item1, steamResult.Item2);
            var jobsQuery = dbManager.GetSteamGames(steamSearchReturn);

            //Console.WriteLine(jobsQuery.Count);

            foreach (SteamGame video in jobsQuery)
            {
                Console.WriteLine(CommandlineInterface.ConvertInstanceToString<SteamGame>(video));
                //foreach (PropertyInfo info in video.GetType().GetProperties())
                //{
                //    string name = info.Name;
                //    object value = info.GetValue(video);
                //    Console.WriteLine("{0}={1}", name, value);
                //}
                Console.WriteLine("");
            }
        }

        public static void scrapeGetSteamSearches()
        {
            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";

            var dbManager = new DBManager(connectionStringTemplate, relativePath);

            var steamSearches = dbManager.GetSteamSearches();

            Console.WriteLine(JsonConvert.SerializeObject(steamSearches));
        }

        public static string getDBInsertString<T>()
        {
            var propertyNames = new List<string>();
            var insertTemplate = "insert into {0}({1}) values ({2})";

            var insertType = typeof(T);

            var className = insertType.Name;
            string columnString = "";
            string valueString = "";

            foreach (PropertyInfo info in insertType.GetProperties())
            {
                string name = info.Name;
                propertyNames.Add(name);
            }

            for (int i = 0; i < propertyNames.Count; i++)
            {
                var propertyName = propertyNames[i];

                columnString += propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
                valueString += "@" + propertyName;

                if (i != propertyNames.Count - 1)
                {
                    columnString += ", ";
                    valueString += ", ";
                }

            }


            return string.Format(insertTemplate, className, columnString, valueString);

        }

        public static string getDictionaries()
        {
            string definitionTemplate = "public static Dictionary<string, string> {0} = new Dictionary<string, string>{{\n{1}}}\n";
            string insertTemplate = "{{\"{0}\", \"{1}\"}},\n";
            Console.WriteLine(string.Format(insertTemplate, "oi", "mate"));
            string returnString = "";
             Dictionary<string, string> XPathsGeneral = new Dictionary<string, string>();
             Dictionary<string, string> XPathsPlayers = new Dictionary<string, string>();
             Dictionary<string, string> XPathsTags = new Dictionary<string, string>();
             Dictionary<string, string> XPathsSort = new Dictionary<string, string>();
             Dictionary<string, string> XPathsResult = new Dictionary<string, string>();
            XPathsGeneral.Add("searchInput", "//input[@id='store_nav_search_term']");
            XPathsGeneral.Add("specialOffers", "//span[@data-param = 'specials']//span[@class = 'tab_filter_control_checkbox']");
            XPathsGeneral.Add("sort by", "//a[@id='sort_by_trigger']");
            XPathsGeneral.Add("players", "//div[text()='Narrow by number of players']");
            XPathsGeneral.Add("results", "//a[contains(@class, 'search_result_row')]");

            XPathsResult.Add("name", ".//span[@class='title']");
            XPathsResult.Add("prices", ".//div[contains(@class,'search_price ')]");
            XPathsResult.Add("discount", ".//div[contains(@class,'search_price ')]/span");

            XPathsPlayers.Add("singleplayer", "//span[@data-loc = 'Single-player']//span[@class = 'tab_filter_control_checkbox']");
            XPathsPlayers.Add("multiplayer", "//span[@data-loc = 'Multi-player']//span[@class = 'tab_filter_control_checkbox']");
            XPathsPlayers.Add("co-op", "//span[@data-param='category3' and @data-loc='Co-op']//span[@class = 'tab_filter_control_checkbox']");

            XPathsTags.Add("indie", "//span[@data-param='tags' and @data-loc='Indie']//span[@class = 'tab_filter_control_checkbox']");
            XPathsTags.Add("action", "//span[@data-param='tags' and @data-loc='Action']//span[@class = 'tab_filter_control_checkbox']");
            XPathsTags.Add("adventure", "//span[@data-param='tags' and @data-loc='Adventure']//span[@class = 'tab_filter_control_checkbox']");

            XPathsSort.Add("lowest price", "//ul[@id='sort_by_droplist']/li/a[text()='Lowest Price']");
            XPathsSort.Add("highest price", "//ul[@id='sort_by_droplist']/li/a[text()='Highest Price']");
            XPathsSort.Add("reviews", "//ul[@id='sort_by_droplist']/li/a[text()='User Reviews']");
            XPathsSort.Add("relevance", "//ul[@id='sort_by_droplist']/li/a[text()='Relevance']");

            var XpathsList = new List<Dictionary<string, string>>
            {
                XPathsGeneral,
                XPathsPlayers,
                XPathsTags,
                XPathsSort,
                XPathsResult
            };

            var names = new List<string> {
                "XPathsGeneral",
                "XPathsPlayers",
                "XPathsTags",
                "XPathsSort",
                "XPathsResult"
                };

            for(int i = 0; i < XpathsList.Count; i++)
            {
                var xpath = XpathsList[i];
                string name = names[i];
                string insertStrings = "";
                string definitionString;


                foreach (KeyValuePair <string, string> entry in xpath)
                {
                    insertStrings += string.Format(insertTemplate, entry.Key, entry.Value);
                }

                definitionString = string.Format(definitionTemplate, name, insertStrings);

                returnString += definitionString;
            }
            return returnString;

        }

        
    }
}
