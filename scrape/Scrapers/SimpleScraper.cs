using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace scrape
{
    class SimpleScraper
    {
        public IWebDriver Driver;
        private WebDriverWait Wait;
        public SimpleScraper()
        {
            var options = new FirefoxOptions();
            //options.AddArguments("--headless");
            Driver = new FirefoxDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        public void GoTo(string URL)
        {
            Driver.Navigate().GoToUrl(URL);
        }
        public IWebElement GetElement(string XPath)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(XPath)));
        }

        public IReadOnlyCollection<IWebElement> GetElements(string XPath)
        {
            GetElement(XPath);
            return Driver.FindElements(By.XPath(XPath));
        }

        public void CheckAndClick(string XPath)
        {
            while (true)
            {
                IWebElement button = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
                try
                {
                    button.Click();
                    break;
                }
                catch
                {
                    Console.WriteLine("Failed to click, button wasn't clickable");
                }
            }
        }

        public void Stop()
        {
            Driver.Quit();
        }


        //public void CheckAndClick(string XPath, Action cathFunction)
        //{
        //    while (true)
        //    {
        //        IWebElement button;
        //        try
        //        {
        //            button = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
        //            try
        //            {
        //                button.Click();
        //                cathFunction();
        //                break;
        //            }
        //            catch
        //            {
        //                Console.WriteLine("oeps");
        //            }
        //        }
        //        catch
        //        {
        //            cathFunction();
        //        }
                
                
        //    }
        //}

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }
    }
}
