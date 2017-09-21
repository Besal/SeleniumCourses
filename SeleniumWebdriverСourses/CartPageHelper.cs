using System;
using System.Collections.Generic;
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
    }
}
