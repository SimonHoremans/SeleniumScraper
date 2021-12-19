using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;

namespace scrape
{
    class SteamScraper
    {
        public static Dictionary<string, string> XPathsGeneral = new Dictionary<string, string>{
                                    {"searchInput", "//input[@id='store_nav_search_term']"},
                                    {"specialOffers", "//span[@data-param = 'specials']//span[@class = 'tab_filter_control_checkbox']"},
                                    {"sort by", "//a[@id='sort_by_trigger']"},
                                    {"players", "//div[text()='Narrow by number of players']"},
                                    {"results", "//a[contains(@class, 'search_result_row')]"}
                                    };
        public static Dictionary<string, string> XPathsPlayers = new Dictionary<string, string>{
                                    {"singleplayer", "//span[@data-loc = 'Single-player']//span[@class = 'tab_filter_control_checkbox']"},
                                    {"multiplayer", "//span[@data-loc = 'Multi-player']//span[@class = 'tab_filter_control_checkbox']"},
                                    {"co-op", "//span[@data-param='category3' and @data-loc='Co-op']//span[@class = 'tab_filter_control_checkbox']"}
                                    };
        public static Dictionary<string, string> XPathsTags = new Dictionary<string, string>{
                                    {"indie", "//span[@data-param='tags' and @data-loc='Indie']//span[@class = 'tab_filter_control_checkbox']"},
                                    {"action", "//span[@data-param='tags' and @data-loc='Action']//span[@class = 'tab_filter_control_checkbox']"},
                                    {"adventure", "//span[@data-param='tags' and @data-loc='Adventure']//span[@class = 'tab_filter_control_checkbox']"}
                                    };
        public static Dictionary<string, string> XPathsSort = new Dictionary<string, string>{
                                    {"lowest price", "//ul[@id='sort_by_droplist']/li/a[text()='Lowest Price']"},
                                    {"highest price", "//ul[@id='sort_by_droplist']/li/a[text()='Highest Price']"},
                                    {"reviews", "//ul[@id='sort_by_droplist']/li/a[text()='User Reviews']"},
                                    {"relevance", "//ul[@id='sort_by_droplist']/li/a[text()='Relevance']"}
                                    };
        public static Dictionary<string, string> XPathsResult = new Dictionary<string, string>{
                                    {"name", ".//span[@class='title']"},
                                    {"prices", ".//div[contains(@class,'search_price ')]"},
                                    {"discount", ".//div[contains(@class,'search_price ')]/span"}
                                    };

        private SimpleScraper Scraper;
        private IJavaScriptExecutor js;
        public SteamScraper(SimpleScraper scraper)
        {
            Scraper = scraper;
            js = (IJavaScriptExecutor)Scraper.Driver;

        }

        public Tuple<SteamSearch, List<SteamGame>> GetGames(string searchTerm, bool specialOffers, string sortBy, List<string> tags, List<string> players)
        {

            Scraper.GoTo("https://store.steampowered.com/");

            

            var steamSearch = new SteamSearch
            {
                SearchTerm = searchTerm,
                SpecialOffers = specialOffers ? 1 : 0,
                SortBy = sortBy,
                Tags = JsonConvert.SerializeObject(tags),
                Players = JsonConvert.SerializeObject(players)

            };

            var steamGames = new List<SteamGame>();

            Scraper.GetElement(XPathsGeneral["searchInput"]).SendKeys(searchTerm + Keys.Enter);
            if (specialOffers)
            {
                Scraper.CheckAndClick(XPathsGeneral["specialOffers"]);
            }

            foreach(string tag in tags)
            {
                Scraper.CheckAndClick(XPathsTags[tag]);
            }

            Scraper.CheckAndClick(XPathsGeneral["players"]);

            foreach (string player in players)
            {
                Scraper.CheckAndClick(XPathsPlayers[player]);
            }

            Scraper.CheckAndClick(XPathsGeneral["sort by"]);
            Scraper.CheckAndClick(XPathsSort[sortBy]);
            //Console.WriteLine("scraper");
            //Console.WriteLine(XPathsSort[sortBy]);
            Scraper.Refresh();

            var gameResults = Scraper.GetElements(XPathsGeneral["results"]);


            foreach (IWebElement game in gameResults)
            {
                var steamGame = new SteamGame();

                steamGame.Link = game.GetAttribute("href");
                steamGame.Name = game.FindElement(By.XPath(XPathsResult["name"])).Text;
                var prices = game.FindElement(By.XPath(XPathsResult["prices"])).Text;
                string price;
                if(prices.Contains("\n")) {
                    price = prices.Split("\n")[1];
                } else
                {
                    price = prices;
                }
                steamGame.Price = price;



                //js.ExecuteScript("console.log(arguments[0])", game);
                //foreach (PropertyInfo info in steamGame.GetType().GetProperties())
                //{
                //    string name = info.Name;
                //    object value = info.GetValue(steamGame);
                //    Console.WriteLine("{0}={1}", name, value);
                //}
                //Console.WriteLine("");


                steamGames.Add(steamGame);
            }

            return Tuple.Create(steamSearch, steamGames);
        }
    }
}
