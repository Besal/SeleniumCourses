using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    public class MovesHelper : SeleniumWebdriverContainer
    {
        public MovesHelper(IWebDriver driver) : base(driver)
        {
        }

        public void MoveToPreviousPage()
        {
            Driver.Navigate().Back();
        }
    }
}
