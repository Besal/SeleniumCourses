using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Ie)]
    [TestFixture(BrowserType.Firefox)]
    public class CheckNewTabs : SeleniumWebdriverBase
    {
        public CheckNewTabs(BrowserType browser) 
            : base(browser) 
        {
        }

        [Test]
        public void CheckOpenningNewTabs()
        {
            FindByXpathAndClick(".//*[@id='app-']//span[@class='name' and text()='Countries']", "раздел стран");
            FindByXpathAndClick(".//*[@id='content']/div/a[@href]", "кнопку добавления страны");
            var externalLinks = Driver.FindElements(By.XPath(".//*[@id='content']//i[@class='fa fa-external-link']"));
            foreach (var externalLink in externalLinks)
            {
                externalLink.Click();
                var mainWindow = Driver.CurrentWindowHandle;
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                var newWindow = Driver.CurrentWindowHandle;
                Assert.AreNotEqual(mainWindow, newWindow);
                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }
        }
    }
}
