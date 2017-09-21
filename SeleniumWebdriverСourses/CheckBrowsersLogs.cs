using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    //[TestFixture(BrowserType.Firefox)]
    //[TestFixture(BrowserType.Ie)]
    public class CheckBrowsersLogs : SeleniumWebdriverBase
    {
        public CheckBrowsersLogs(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void CheckBrowserLogsAfterClickOnProducts()
        {
            Driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            var linksCount = FindElements(By.XPath(".//*[@id='content']//a[@title='Edit']"));
            for (int i = 0; i < linksCount.Count; i++)
            {
                var links = FindElements(By.XPath(".//*[@id='content']//a[@title='Edit']")).Skip(i).First();
                links.Click();
                var logs = Driver.Manage().Logs.GetLog("browser");
                CheckEmptinessOfAnyErrorsInBrowserLogs(logs);
                MovesHelper.MoveToPreviousPage();
                var logsAfterReturning = Driver.Manage().Logs.GetLog("browser");
                CheckEmptinessOfAnyErrorsInBrowserLogs(logsAfterReturning);
            }
            
        }

        private static void CheckEmptinessOfAnyErrorsInBrowserLogs(ReadOnlyCollection<LogEntry> logs)
        {
            Assert.IsTrue(logs.Count == 0);
        }
    }
}
