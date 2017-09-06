using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    //[TestFixture(BrowserType.Ie)]
    [TestFixture(BrowserType.Firefox)]
    public class CheckProductsInformation : SeleniumWebdriverBase
    {
        public CheckProductsInformation(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void CheckProductsFontStylesAndText()
        {
            ClickToMainPageButton();
            var nodeOfElement = GetNodeOfElement(".//*[@id='box-campaigns']//li[@class='product column shadow hover-light']");
            var textOfFirstElementOnMainPage = nodeOfElement.Select(el => el
                .FindElement(By.XPath(
                    "./a//div[@class='name']"))
                .Text).ToList().First();
            var regularPriceValue = nodeOfElement
                .Select(el => el.FindElement(By.XPath("./a//div[@class='price-wrapper']/s[@class='regular-price']"))
                    .Text)
                .ToList()
                .First();
            var salePriceValue = nodeOfElement
                .Select(el => el
                    .FindElement(By.XPath("./a//div[@class='price-wrapper']/strong[@class='campaign-price']"))
                    .Text)
                .ToList()
                .First();
            CheckColorsAndPropertiesValues(nodeOfElement, Color.FromArgb(119, 119, 119));
            CheckPropertiesValues(nodeOfElement, ".//strong[@class='campaign-price']", "font-weight",
                Browser == BrowserType.Firefox ? "900" : "bold");
            nodeOfElement.First().Click();
            var nodeOfPriceinOpennedProduct = GetNodeOfElement(".//*[@id='box-product']//div[@class='price-wrapper']");
            var textInOpennedProduct = Driver.FindElement(By.XPath(".//*[@id='box-product']//h1[@class='title']")).Text;
            var regularPriceValueInOpennedProduct = Driver
                .FindElement(By.XPath(".//*[@id='box-product']//s[@class='regular-price']")).Text;
            var salePriceValueInOpennedProduct = Driver.FindElement(By.XPath(".//*[@id='box-product']//strong[@class='campaign-price']")).Text;
            CheckPropertiesValues(nodeOfPriceinOpennedProduct, ".//strong[@class='campaign-price']", "font-weight",
                Browser == BrowserType.Firefox ? "700" : "bold");
            CheckColorsAndPropertiesValues(nodeOfPriceinOpennedProduct, Color.FromArgb(102, 102, 102));
            CheckValuesEquals(textOfFirstElementOnMainPage, textInOpennedProduct, regularPriceValue, regularPriceValueInOpennedProduct, salePriceValue, salePriceValueInOpennedProduct);
        }

        private static void CheckValuesEquals(string textOfFirstElementOnMainPage, string textInOpennedProduct,
            string regularPriceValue, string regularPriceValueInOpennedProduct, string salePriceValue,
            string salePriceValueInOpennedProduct)
        {
            Assert.AreEqual(textOfFirstElementOnMainPage, textInOpennedProduct,
                $"Текст при открытии товара {textInOpennedProduct}, а должен быть {textOfFirstElementOnMainPage}.");
            Assert.AreEqual(regularPriceValue, regularPriceValueInOpennedProduct,
                $"Сумма обычной цены при открытии товара {regularPriceValueInOpennedProduct}, а должна быть {regularPriceValue}.");
            Assert.AreEqual(salePriceValue, salePriceValueInOpennedProduct,
                $"Сумма цены со скидкой при октрытии товара {salePriceValueInOpennedProduct}, а должна быть {salePriceValue}.");
        }

        private void CheckColorsAndPropertiesValues(ReadOnlyCollection<IWebElement> elementNode, Color colorGray)
        {
            CheckPriceColor(elementNode, colorGray, ".//s[@class='regular-price']",
                "text-decoration-color");
            CheckPropertiesValues(elementNode, ".//s[@class='regular-price']", "text-decoration-line",
                "line-through");
            CheckPriceColor(elementNode, Color.FromArgb(204, 0, 0), ".//strong[@class='campaign-price']",
                "color");
        }

        private static void CheckPropertiesValues(ReadOnlyCollection<IWebElement> nodeOfElement, string selector, string nameOfProperty, string expectedPropertyValue)
        {
            var fontWeightValue = nodeOfElement.Select(el => el
                    .FindElement(By.XPath(selector))
                    .GetCssValue(nameOfProperty))
                .First();
            Assert.AreEqual(expectedPropertyValue, fontWeightValue, $"Стиль шрифта {fontWeightValue}, должен быть {expectedPropertyValue}.");
        }

        private void CheckPriceColor(ReadOnlyCollection<IWebElement> nodeOfElement, Color color, string selector, string property)
        {
            var priceColor = nodeOfElement.Select(el => el
                    .FindElement(By.XPath(selector))
                    .GetCssValue(property))
                    .First();
            Assert.AreEqual(color, ParseColor(priceColor),
                $"Получен цвет {ParseColor(priceColor)}, а должен быть {color}");

        }

        private ReadOnlyCollection<IWebElement> GetNodeOfElement(string selector)
        {
            var nodeOfElement =
                Driver.FindElements(By.XPath(selector));
            return nodeOfElement;
        }

        private Color ParseColor(string color)
        {
            var colorRegex = new Regex(@"\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})(?:,\s*\d)?\)");
            var match = colorRegex.Match(color);
            Assert.IsTrue(match.Success, $"Не удалось распарсить цвет из строки {color}");

            return Color.FromArgb(
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value));
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
