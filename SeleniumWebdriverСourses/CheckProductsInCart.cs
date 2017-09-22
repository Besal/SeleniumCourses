using System;
using System.Linq;
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
            for (var i = 1; i < 4; i++)
            {
                MainProductsPageHelper.OpenProductsInformation();
                ProductsPageHelper.AddFewProductsToCart(wait, i);
                MovesHelper.MoveToPreviousPage();
            }
            ProductsPageHelper.ClickToCartButton();
            if (CartPageHelper.CheckAnyProductsExistsInCart())
            {
                var productsCount = CartPageHelper.FindAllProductsInCart();
                for (var i = 0; i < productsCount.Count; i++)
                {
                    DeleteAllProductsInCart(wait);
                }
            }
            else
            {
                Assert.IsFalse(CartPageHelper.CheckAnyProductsExistsInCart(), "Products don't exist in cart");
            }
        }

        private void DeleteAllProductsInCart(WebDriverWait wait)
        {
            var productsInCart = CartPageHelper.FindAllProductsInCart();
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
