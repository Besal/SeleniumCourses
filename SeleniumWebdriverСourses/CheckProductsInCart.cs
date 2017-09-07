using System;
using System.Collections.Generic;
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
    public class CheckProductsInCart : SeleniumWebdriverBase
    {
        public CheckProductsInCart(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void CheckAddingAndDeletingProductsInCart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(2000));
            ClickToMainPageButton();
            for (int i = 1; i < 4; i++)
            {
                FindByXpathAndClick("//div[@id='box-most-popular']//li[@class='product column shadow hover-light'][1]", "товар");
                AddProductToCart(wait, i);
                Driver.Navigate().Back();
            }
            FindByXpathAndClick("//*[@id='cart']/a[text()='Checkout »']", "кнопку входа в корзину");
            if (CheckElementExists(By.XPath("//tr[not(@class='header')]/td[@class='sku']")))
            {
                var productsCount = Driver.FindElements(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
                for (int i = 0; i < productsCount.Count; i++)
                {
                    DeleteAllProductsInCart(wait);
                }
            }
        }

        private void DeleteAllProductsInCart(WebDriverWait wait)
        {
            var productsInCart = Driver.FindElements(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
            FindByNameAndClick("remove_cart_item", "");
            wait.Until(ExpectedConditions.StalenessOf(productsInCart.First()));
        }

        private void AddProductToCart(WebDriverWait wait, int i)
        {
            if (CheckElementExists(By.XPath(".//*[@id='box-product']//select[@name='options[Size]']")))
            {
                FindByXpathAndClick("//*[@id='box-product']//select[@name='options[Size]']/option[@value='Small']",
                    "кнопку выбора размера товара");
            }
            FindByXpathAndClick("//button[@value='Add To Cart']", "кнопку добавления в корзину");
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath($"//*[@id='cart']//span[@class='quantity' and text()='{i}']")));
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
