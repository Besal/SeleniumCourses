using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    public class CartPageHelper : SeleniumWebdriverContainer
    {
        public CartPageHelper(IWebDriver driver) : base(driver)
        {
        }

        public void ClickToProductsDeleteButton()
        {
            FindByNameAndClick("remove_cart_item", "кнопку удаления товара");
        }

        public bool CheckAnyProductsExistsInCart()
        {
            return CheckElementExists(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
        }

        public ReadOnlyCollection<IWebElement> FindAllProductsInCart()
        {
            return FindElements(By.XPath("//tr[not(@class='header')]/td[@class='sku']"));
        }
    }
}
