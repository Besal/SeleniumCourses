using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebdriverСourses
{
    public class ProductsPageHelper : SeleniumWebdriverContainer
    {
        public ProductsPageHelper(IWebDriver driver) : base(driver)
        {
        }
        public void AddFewProductsToCart(WebDriverWait wait, int i)
        {
            if (CheckElementExists(By.XPath(".//*[@id='box-product']//select[@name='options[Size]']")))
            {
                ClickToSizeProductsButton();
            }
            ClickToAddProductsButton();
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath($"//*[@id='cart']//span[@class='quantity' and text()='{i}']")));
        }

        public void ClickToSizeProductsButton()
        {
            FindByXpathAndClick("//*[@id='box-product']//select[@name='options[Size]']/option[@value='Small']",
                "кнопку выбора размера товара");
        }

        public void ClickToAddProductsButton()
        {
            FindByXpathAndClick("//button[@value='Add To Cart']", "кнопку добавления в корзину");
        }

        public void ClickToCartButton()
        {
            FindByXpathAndClick("//*[@id='cart']/a[text()='Checkout »']", "кнопку входа в корзину");
        }
    }
}
