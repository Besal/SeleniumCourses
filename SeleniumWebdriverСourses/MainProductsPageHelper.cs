using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    public class MainProductsPageHelper : SeleniumWebdriverContainer
    {
        public MainProductsPageHelper(IWebDriver driver) : base(driver)
        {
        }

        public void OpenProductsInformation()
        {
            FindByXpathAndClick("//div[@id='box-most-popular']//li[@class='product column shadow hover-light'][1]", "товар");
        }
    }
}
