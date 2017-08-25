using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Ie)]
    [TestFixture(BrowserType.Firefox)]

    public class AdminPanelSectionsCheck : SeleniumWebdriverBase
    {
        private const string Settings = "Settings";
        private readonly Dictionary<string, string> _collectionOfSectionsAndHeaders;

        public AdminPanelSectionsCheck(BrowserType browser) 
            : base(browser) 
        {
            #region Dictionary of sections and headers

            _collectionOfSectionsAndHeaders = new Dictionary<string, string>
            {
                {"Appearence", "Template"},
                {"Logotype", ""},
                {"Catalog", ""},
                {"Product Groups", ""},
                {"Option Groups", ""},
                {"Manufacturers", ""},
                {"Suppliers", ""},
                {"Delivery Statuses", ""},
                {"Sold Out Statuses", ""},
                {"Quantity Units", ""},
                {"CSV Import/Export", "CSV Import/Export"},
                {"Countries", ""},
                {"Currencies", ""},
                {"Customers", ""},
                {"Newsletter", ""},
                {"Geo Zones", ""},
                {"Languages", ""},
                {"Storage Encoding", ""},
                {"Modules", "Job Modules"},
                {"Customer", "Customer Modules"},
                {"Shipping", "Shipping Modules"},
                {"Payment", "Payment Modules"},
                {"Order Total", "Order Total Modules"},
                {"Order Success", "Order Success Modules"},
                {"Order Action", "Order Action Modules"},
                {"Orders", ""},
                {"Order Statuses", ""},
                {"Pages", ""},
                {"Reports", "Monthly Sales"},
                {"Most Sold Products", ""},
                {"Most Shopping Customers", ""},
                {Settings, Settings},
                {"Defaults", Settings},
                {"General", Settings},
                {"Listings", Settings},
                {"Images", Settings},
                {"Checkout", Settings},
                {"Advanced", Settings},
                {"Security", Settings},
                {"Slides", ""},
                {"Tax", "Tax Classes"},
                {"Tax Rates", ""},
                {"Translations", "Search Translations"},
                {"Scan Files", "Scan Files For Translations"},
                {"Users", ""},
                {"vQmods", ""}
            };
            #endregion
        }

        [Test(Description = "Checking header(h1) in every section")]
        public void CheckHeaderInEverySection()
        {
            CheckCatalogSection();
        }
        
        public void CheckCatalogSection()
        {
            foreach (var section in _collectionOfSectionsAndHeaders)
            {
                ClickToSectionAndCheckH1(section.Key, section.Value == "" ? section.Key : section.Value);
            }
        }

        private void ClickToSectionAndCheckH1(string section, string h1)
        {
            FindByXpathAndClick($"//*[@id='app-']//span[@class='name' and . = '{section}']", $"{section}");
            Assert.IsTrue(CheckElementExists(By.XPath($"//*[@id='content']/h1[contains(.,'{h1}')]")),
                $"Заголовок не соответствует разделу. Должен быть {h1}");
        }
    }
}
