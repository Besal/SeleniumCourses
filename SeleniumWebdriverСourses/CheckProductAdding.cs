using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    //[TestFixture(BrowserType.Ie)]
    class CheckProductAdding : SeleniumWebdriverBase
    {
        public CheckProductAdding(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void AddProduct()
        {
            string pathToFile = AppDomain.CurrentDomain.BaseDirectory + @"\Gizmo.jpg";
            FindByXpathAndClick(".//*[@id='app-']/a/span[text()='Catalog']", "кнопку каталога");
            FindByXpathAndClick(".//*[@id='content']//a[text()=' Add New Product']", "кнопку добавления товара");
            FindByNameAndType("name[en]", "Gizmo");
            FindByNameAndType("code", "GZM");
            FindByNameAndClick("product_groups[]", "кнопку выбора пола");
            Driver.FindElement(By.XPath(".//*[@id='tab-general']//input[@name='new_images[]']")).SendKeys(pathToFile);
        }
    }
}
