using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Ie)]
    [TestFixture(BrowserType.Firefox)]
    public class CheckLabelsInProducts : SeleniumWebdriverBase
    {
        public CheckLabelsInProducts(BrowserType browser)
            : base(browser)
        {
        }

        [Test]
        public void CheckStickers()
        {
            FindByXpathAndClick(".//*[@id='sidebar']//i[@class='fa fa-chevron-circle-left']",
                "кнопку главной страницы");
            var listOfProducts = Driver
                .FindElements(By.XPath("//div[@class='image-wrapper']"))
                .Select(el => el.FindElements(By.XPath("./div[contains(@class, 'sticker') and contains(text(), '')]")))
                .ToList();
            foreach (var element in listOfProducts)
            {
                var countOfStickers = element.Count(sticker => sticker.Displayed);
                Assert.AreEqual(1, countOfStickers, $"Стикеров у товара {countOfStickers}, а должен быть только один");
            }
        }

        [Test]
        public void CheckCountriesBlockSorting()
        {
            FindByXpathAndClick("//span[@class='name' and contains(text(), 'Countries')]", "кнопку стран");
            var listOfCoutries = GetListOfElementsText("//table[@class='dataTable']//tr[@class='row']/td[5]/a[@href and text()]");
                CheckPairSorted(listOfCoutries);

            var nonZeroZonesLinks =
                GetListOfElementsWithAttribute(".//tbody/tr[@class='row' and td[position()=6 and text()!='0']]/td[5]/a",
                    "href");
            foreach (var elementLink in nonZeroZonesLinks)
            {
                Driver.FindElement(By.XPath($"//a[@href='{elementLink}']")).Click();
                var zonesInCountry = GetListOfElementsText(".//*[@id='table-zones']//td[text()][3]");
                    CheckPairSorted(zonesInCountry);
                Driver.Navigate().Back();
            }
        }
        
        [Test]
        public void CheckGeozonesBlockSorting()
        {
            FindByXpathAndClick("//span[@class and text()='Geo Zones']", "кнопку гео зон");
            var geoZonesLinks = GetListOfElementsWithAttribute("//td/a[@href and not(@title)]", "href");
            foreach (var zonesLink in geoZonesLinks)
            {
                Driver.FindElement(By.XPath($"//a[@href='{zonesLink}']")).Click();
                var listOfZonesForEdit =
                    GetListOfElementsText(".//*[@id='table-zones']//td[3]//option[@selected='selected' and @value]");
                    CheckPairSorted(listOfZonesForEdit);
                Driver.Navigate().Back();
            }
        }

        private List<string> GetListOfElementsWithAttribute(string selector, string attribute)
        {
            return Driver.FindElements(By.XPath(selector))
                .Select(el => el.GetAttribute(attribute))
                .ToList();
        }
        //CollectionAssert.AreEqual(list.OrderBy(e => e), list);

        private static void CheckPairSorted(List<string> list)
        {
            for (int i = 1; i < list.Count; i++)
                Assert.AreEqual(1, string.Compare(list[i], list[i - 1], StringComparison.InvariantCulture),
                "Страны находятся не в алфавитном порядке");
        }

        private List<string> GetListOfElementsText(string selector)
        {
            return Driver.FindElements(By.XPath(selector))
                .Select(el => el.Text)
                .ToList();
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}

