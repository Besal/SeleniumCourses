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
            FindByXpathAndClick(".//*[@id='sidebar']//i[@class='fa fa-chevron-circle-left']", "кнопку главной страницы");
            var listOfProducts = Driver
                .FindElements(By.XPath("//div[@class='image-wrapper']"))
                .Select(el => el.FindElements(By.XPath("./div[contains(@class, 'sticker') and contains(text(), '')]"))).ToList();
            foreach (var element in listOfProducts)
            {
                var countOfStickers = element.Count(sticker => sticker.Displayed);
                Assert.AreEqual(1, countOfStickers, $"Стикеров у товара {countOfStickers}, а должен быть только один");
            }
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit(); 
        }
    }

    
}
