using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Newtonsoft.Json;

namespace scrape
{
    class IndeedScraper
    {
        private Dictionary<string, string> XPaths;
        private SimpleScraper Scraper;
        private IJavaScriptExecutor js;
        public IndeedScraper(SimpleScraper scraper)
        {
            XPaths = new Dictionary<string, string>();
            XPaths.Add("whatInput", "//input[@id = 'text-input-what']");
            XPaths.Add("whereInput", "//input[@id = 'text-input-where']");
            XPaths.Add("searchButton", "//form[@id = 'whatWhereFormId']//button");
            XPaths.Add("dateFilterButton", "//button[contains(@id, 'filter-dateposted')]");
            XPaths.Add("3days", "//ul[@id = 'filter-dateposted-menu']/li[2]//a");

            Scraper = scraper;
            js = (IJavaScriptExecutor)Scraper.Driver;
            //Scraper.GoTo("https://be.indeed.com/");

        }

        public Tuple<IndeedSearch, List<IndeedJob>> GetJobs(string whatSearchTerm, string whereSearchTerm)
        {

            Scraper.GoTo("https://be.indeed.com/");

            
            var indeedSearch = new IndeedSearch() { 
                SearchTermWhat = whatSearchTerm,
                SearchTermWhere = whereSearchTerm
            };

            var indeedJobs = new List<IndeedJob>();
            //Scraper.CheckAndClick(XPaths["whatInput"]);
            Scraper.GetElement(XPaths["whatInput"]).SendKeys(whatSearchTerm);
            Scraper.GetElement(XPaths["whereInput"]).SendKeys(whereSearchTerm + Keys.Enter);

            Scraper.CheckAndClick(XPaths["dateFilterButton"]);
            while (true)
            {
                try
                {
                    Scraper.CheckAndClick(XPaths["3days"]);
                    break;
                }
                catch
                {
                    Scraper.CheckAndClick(XPaths["dateFilterButton"]);
                }
            }
            
            
            Scraper.Refresh();


            var jobs = Scraper.GetElements("//div[@id = 'mosaic-provider-jobcards']/a");


            foreach(IWebElement job in jobs)
            {
                var indeedJob = new IndeedJob();

                indeedJob.JobTitle = job.FindElement(By.XPath(".//h2[contains(@class,'jobTitle')]/span")).Text;
                

                var companyInfo = job.FindElement(By.XPath(".//span[contains(@class, 'companyName')]"));

                indeedJob.Company = companyInfo.Text;
                indeedJob.Location = job.FindElement(By.XPath(".//div[contains(@class, 'companyLocation')]")).Text;
                indeedJob.JobLink = job.GetAttribute("href");

                indeedJobs.Add(indeedJob);

                js.ExecuteScript("console.log(arguments[0])", job);

            }

            //var stringDing = JsonConvert.SerializeObject(indeedJobs);
            //Console.WriteLine(stringDing);

            return Tuple.Create(indeedSearch, indeedJobs);

            //Scraper.CheckAndClick(XPaths["whereInput"]);
        }
    }
}
