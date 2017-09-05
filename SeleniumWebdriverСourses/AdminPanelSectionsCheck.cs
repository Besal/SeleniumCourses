using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Ie)]
    [TestFixture(BrowserType.Firefox)]

    public class AdminPanelSectionsCheck : SeleniumWebdriverBase
    {

        public AdminPanelSectionsCheck(BrowserType browser) 
            : base(browser) 
        {
        }

        [Test(Description = "Checking header(h1) in every section")]
        public void CheckHeaderInEverySection()
        {
            var sections = Driver.FindElements(By.XPath("//*[@id='box-apps-menu']/li"));
            for (int i = 1; i < sections.Count; i++)
            {
                var sectionsForClick = Driver.FindElements(By.XPath($"//*[@id='box-apps-menu']/li[{i}]"));
                sectionsForClick.First().Click();
                var subsections =
                    Driver.FindElements(By.XPath(
                        "//ul[@id='box-apps-menu']/li[@class='selected']/ul[@class='docs']/li"));
                if (subsections.Any())
                {
                    for (int s = 1; s < subsections.Count + 1; s++)
                    {
                        var subsectionsForClick =
                            Driver.FindElements(By.XPath(
                                $"//*[@id='box-apps-menu']/li[@class='selected']/ul[@class='docs']/li[{s}]"));
                        subsectionsForClick.First().Click();
                        Assert.IsTrue(CheckElementExists(By.XPath(".//*[@id='content']/h1")));
                    }
                }
            }
        }
    }
}
