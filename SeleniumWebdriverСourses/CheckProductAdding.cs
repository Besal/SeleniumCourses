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
            var pathToFile = AppDomain.CurrentDomain.BaseDirectory + @"\Gizmo.jpg";
            FindByXpathAndClick("//*[@id='app-']/a/span[text()='Catalog']", "кнопку каталога");
            FindByXpathAndClick("//*[@id='content']//a[text()=' Add New Product']", "кнопку добавления товара");
            FillGeneralTab(pathToFile);
            FillInformationTab();
            FillPricesTab();
            FindByNameAndClick("save", "кнопку сохранения");
            ClickToMainPageButton();
            CheckProductAvailability();
        }

        private void CheckProductAvailability()
        {
            Assert.IsTrue(CheckElementExists(By.XPath("//div[@class='name' and text()='Gizmo']")),
                "Гизмо нет в списке товаров");
        }

        private void FillPricesTab()
        {
            FindByXpathAndClick("//a[@href='#tab-prices']", "вкладку Prices");
            FindByNameAndType("purchase_price", "10");
            FindByXpathAndClick("//select/option[@value='USD']", "выбор валюты");
            FindByNameAndType("gross_prices[USD]", "11");
        }

        private void FillInformationTab()
        {
            FindByXpathAndClick("//a[@href='#tab-information']", "вкладку Information");
            FindByXpathAndClick("//select/option[contains(text(), 'ACME Corp.')]", "выбор производителя");
            FindByNameAndType("keywords", "GZM");
            FindByNameAndType("short_description[en]", "GZM");
            FindByNameAndType("head_title[en]", "Gizmo");
            FindByNameAndType("meta_description[en]", "Gizmo");
        }

        private void FillGeneralTab(string pathToFile)
        {
            FindByXpathAndClick("//input[@value='1']", "radio-button доступности товара");
            FindByNameAndType("name[en]", "Gizmo");
            FindByNameAndType("code", "GZM");
            FindByNameAndClick("product_groups[]", "кнопку выбора пола");
            Driver.FindElement(By.XPath("//*[@id='tab-general']//input[@name='new_images[]']")).SendKeys(pathToFile);
            Driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("05-09-2017");
            Driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("05-09-2018");
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
