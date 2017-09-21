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
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(2000));
            ClickToMainPageButton();
            for (int i = 1; i < 4; i++)
            {
                MainProductsPageHelper.OpneProductsInformation();
                ProductsPageHelper.AddFewProductsToCart(wait, i);
                MovesHelper.MoveToPreviousPage();
            }
            ProductsPageHelper.ClickToCartButton();
            if (CheckElementExists(By.XPath("//tr[not(@class='header')]/td[@class='sku']")))
            {
                var productsCount = FindElements(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
                for (int i = 0; i < productsCount.Count; i++)
                {
                    DeleteAllProductsInCart(wait);
                }
            }
        }

        private void DeleteAllProductsInCart(WebDriverWait wait)
        {
            var productsInCart = FindElements(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
            CartPageHelper.ClickToProductsDeleteButton();
            wait.Until(ExpectedConditions.StalenessOf(productsInCart.First()));
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
