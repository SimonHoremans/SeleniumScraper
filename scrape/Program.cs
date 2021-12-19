using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace scrape
{
    class Program
    {
        static void Main(string[] args)
        {


            //scrapeDB();
            //Console.WriteLine(getDBInsertString<SteamGame>());
            //scrapeDBIndeed();
            //Console.WriteLine(Tests.getDictionaries());

            //var returnTuple = CommandlineInterface.ListMenu(new List<string> { "lsdkf", "lsdkfj", "lskdfj" }, "dit is extra hihi", new Dictionary<string, string> {
            //    {"b", "back" },
            //    {"h", "home" }
            //});

            //var returnTuple = CommandlineInterface.ListMenu(new List<string> { "lsdkf", "lsdkfj", "lskdfj" }, ""
            //);

            //Console.WriteLine(returnTuple);

            var commandlineInterface = new CommandlineInterface();






        }

        
        //static void firstTry()
        //{
        //    IWebDriver driver = new FirefoxDriver();
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        //    driver.Navigate().GoToUrl("https://www.youtube.com/");
        //    //IWebElement searchBar = driver.FindElement(By.Id("search"));
        //    //IWebElement searchButton = driver.FindElement(By.Id("search-icon-legacy"));

        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    string xPathAgreeButton = "//tp-yt-paper-dialog//*[contains(@class, 'footer')]//*[contains(@class, 'buttons')]//child::ytd-button-renderer[2]//a";
        //    IWebElement agreeButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xPathAgreeButton)));

        //    //IWebElement agreeButton = driver.FindElement(By.XPath("//tp-yt-paper-dialog"));
        //    string styleAgreeButton = agreeButton.GetAttribute("style");
        //    //js.ExecuteScript("console.log(arguments[0])", agreeButton);
        //    Console.WriteLine(styleAgreeButton);
        //    Console.WriteLine(agreeButton);
        //    agreeButton.Click();
        //    string xPathSearchBar = "//input[@id = 'search']";
        //    IWebElement searchBar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xPathSearchBar)));


        //    string xPathSearchBox = "//ytd-searchbox";
        //    CheckAndClick(xPathSearchBox, wait);

        //    searchBar.SendKeys("selenium tutorial" + Keys.Enter);



        //    //string xPathVideos = "//ytd-video-renderer";

        //    //var firstVideo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xPathVideos)));




        //    string xPathFilterButton = "//div[@id = 'filter-menu']//*[@id = 'button']";
        //    CheckAndClick(xPathFilterButton, wait);

        //    string xPathFilterDate = "//ytd-search-filter-group-renderer[last()]//ytd-search-filter-renderer[2]//a";
        //    CheckAndClick(xPathFilterDate, wait);

        //    driver.Navigate().Refresh();

        //    string xPathVideos = "//ytd-video-renderer";
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xPathVideos)));
        //    var videos = driver.FindElements(By.XPath(xPathVideos));

        //    string xPathTitle = ".//a[@id = 'video-title']";
        //    string xPathViews = ".//div[@id = 'metadata-line']//child::span[1]";
        //    string xPathChannel = ".//div[@id = 'channel-info']//div[@id = 'text-container']//a";

        //    foreach (IWebElement video in videos)
        //    {
        //        var titleInfo = video.FindElement(By.XPath(xPathTitle));
        //        var titleName = titleInfo.GetAttribute("title");
        //        var titleLink = titleInfo.GetAttribute("href");

        //        var viewsInfo = video.FindElement(By.XPath(xPathViews));
        //        var views = viewsInfo.Text.Split(" ")[0];

        //        var channelInfo = video.FindElement(By.XPath(xPathChannel));
        //        var channelName = channelInfo.Text;
        //        var channelLink = channelInfo.GetAttribute("href");

        //        Console.WriteLine(titleName);
        //        Console.WriteLine(titleLink);
        //        Console.WriteLine(views);
        //        Console.WriteLine(channelName);
        //        Console.WriteLine(channelLink);



        //        js.ExecuteScript("console.log(arguments[0])", video);
        //    }
        //}

        //static void secondTry()
        //{
        //    //var youtubeScraper = new YoutubeScraper();
        //    //var result = youtubeScraper.GetVideos("mr beast");
        //    //var resultString = JsonConvert.SerializeObject(result);
        //    //Console.WriteLine(resultString);
        //}

        //static void scraperTest()
        //{
        //    //var indeedScraper = new IndeedScraper();
        //    //var result = indeedScraper.GetJobs("lasser", "mol");
        //    //var resultString = JsonConvert.SerializeObject(result);
        //    //Console.WriteLine(resultString);
        //}

        //static void scraperTest2()
        //{
        //    var sharedScraper = new SimpleScraper();
        //    var steamScraper = new SteamScraper(sharedScraper);
        //    var youtubeScraper = new YoutubeScraper(sharedScraper);
        //    var indeedScraper = new IndeedScraper(sharedScraper);

        //    var steamResult = steamScraper.GetGames("", false, "reviews", new List<string> { "adventure" }, new List<string> { "co-op" });
        //    var steamResultString = JsonConvert.SerializeObject(steamResult);
        //    var youtubeResult = youtubeScraper.GetVideos("louis cole");
        //    var youtubeResultString = JsonConvert.SerializeObject(youtubeResult);
        //    var indeedResult = indeedScraper.GetJobs("lasser", "mol");
        //    var indeedResultString = JsonConvert.SerializeObject(indeedResult);

        //    Console.WriteLine(youtubeResultString);
        //    Console.WriteLine(steamResultString);
        //    Console.WriteLine(indeedResultString);
        //}
        //static void scrapeDBYoutube()
        //{
        //    string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            
        //    string connectionString = "Data Source=" + absolutePath;

        //    var dbManager = new DBManager(connectionString);
        //    var sharedScraper = new SimpleScraper();
        //    var youtubeScraper = new YoutubeScraper(sharedScraper);

        //    var youtubeResult = youtubeScraper.GetVideos("toyota");
        //    var youtubeResultString = JsonConvert.SerializeObject(youtubeResult);

        //    var youtubeSearchReturn = dbManager.InsertYoutube(youtubeResult.Item1, youtubeResult.Item2);
        //    var videosQuery = dbManager.GetYoutubeVideos(youtubeSearchReturn);

        //    Console.WriteLine(videosQuery.Count);

        //    foreach(YoutubeVideo video in videosQuery)
        //    {
        //        foreach (PropertyInfo info in video.GetType().GetProperties())
        //        {
        //            string name = info.Name;
        //            object value = info.GetValue(video);
        //            Console.WriteLine("{0}={1}", name, value);
        //        }
        //        Console.WriteLine("");
        //    }


        //}

        //static void scrapeDBIndeed()
        //{
        //    string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";

        //    string connectionString = "Data Source=" + absolutePath;

        //    var dbManager = new DBManager(connectionString);
        //    var sharedScraper = new SimpleScraper();
        //    var indeedScraper = new IndeedScraper(sharedScraper);

        //    var indeedResult = indeedScraper.GetJobs("monteur", "balen");
        //    var indeedResultString = JsonConvert.SerializeObject(indeedResult);

        //    var indeedSearchReturn = dbManager.InsertIndeed(indeedResult.Item1, indeedResult.Item2);
        //    var jobsQuery = dbManager.GetIndeedJobs(indeedSearchReturn);

        //    Console.WriteLine(jobsQuery.Count);

        //    foreach (IndeedJob video in jobsQuery)
        //    {
        //        foreach (PropertyInfo info in video.GetType().GetProperties())
        //        {
        //            string name = info.Name;
        //            object value = info.GetValue(video);
        //            Console.WriteLine("{0}={1}", name, value);
        //        }
        //        Console.WriteLine("");
        //    }


        //}

        //static void scrapeDBSteam()
        //{
        //    string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";

        //    string connectionString = "Data Source=" + absolutePath;

        //    var dbManager = new DBManager(connectionString);
        //    var sharedScraper = new SimpleScraper();
        //    var steamScraper = new SteamScraper(sharedScraper);

        //    var steamResult = steamScraper.GetGames("", false, "reviews", new List<string> {"indie" }, new List<string> {"singleplayer" } );
        //    var steamResultString = JsonConvert.SerializeObject(steamResult);

        //    Console.WriteLine(steamResultString);

        //    var steamSearchReturn = dbManager.InsertSteam(steamResult.Item1, steamResult.Item2);
        //    var jobsQuery = dbManager.GetSteamGames(steamSearchReturn);

        //    Console.WriteLine(jobsQuery.Count);

        //    foreach (SteamGame video in jobsQuery)
        //    {
        //        foreach (PropertyInfo info in video.GetType().GetProperties())
        //        {
        //            string name = info.Name;
        //            object value = info.GetValue(video);
        //            Console.WriteLine("{0}={1}", name, value);
        //        }
        //        Console.WriteLine("");
        //    }
        //}

        //static string getDBInsertString<T>()
        //{
        //    var propertyNames = new List<string>();
        //    var insertTemplate = "insert into {0}({1}) values ({2})";

        //    var insertType = typeof(T);

        //    var className = insertType.Name;
        //    string columnString = "";
        //    string valueString = "";

        //    foreach (PropertyInfo info in insertType.GetProperties())
        //    {
        //        string name = info.Name;
        //        propertyNames.Add(name);
        //    }

        //    for(int i = 0; i < propertyNames.Count; i++)
        //    {
        //        var propertyName = propertyNames[i];

        //        columnString += propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        //        valueString += "@" + propertyName;

        //        if(i != propertyNames.Count - 1 )
        //        {
        //            columnString += ", ";
        //            valueString += ", ";
        //        }

        //    }


        //    return string.Format(insertTemplate, className, columnString, valueString);

        //}
       

        
    }
}
