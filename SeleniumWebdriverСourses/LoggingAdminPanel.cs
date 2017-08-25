using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    //[TestFixture(BrowserType.Ie)]
    //[TestFixture(BrowserType.Firefox)]
    public class LoggingAdminPanel : SeleniumWebdriverBase
    {
        public LoggingAdminPanel(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void CheckSomeElementsInAdminPanel()
        {
            var listOfSections = new List<string>
            {
                "Appearence",
                "Catalog",
                "Countries",
                "Currencies",
                "Customers",
                "Geo Zones"
            };

            foreach (var section in listOfSections)
            {
                Assert.IsTrue(CheckElementExists(
                    By.XPath($"//*[@id='app-']//span[@class='name' and contains(text(), '{section}')]")));
            }
        }
    }
    
}
