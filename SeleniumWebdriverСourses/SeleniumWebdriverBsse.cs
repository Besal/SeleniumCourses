using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace SeleniumWebdriverСourses
{
    public abstract class SeleniumWebdriverBase : SeleniumWebdriverContainer
    {
        public BrowserType Browser;
        public static string BaseUrl;
        public Exception Exception { get; set; }
        protected readonly MovesHelper MovesHelper;
        protected readonly MainProductsPageHelper MainProductsPageHelper;
        protected readonly ProductsPageHelper ProductsPageHelper;
        protected readonly CartPageHelper CartPageHelper;

        protected SeleniumWebdriverBase(BrowserType type):base(CreateWebDriver(type))
        {
            Browser = type;
            DesiredCapabilities desCaps = new DesiredCapabilities();
            desCaps.SetCapability("acceptSslCerts", true);
            MovesHelper = new MovesHelper(Driver);
            MainProductsPageHelper = new MainProductsPageHelper(Driver);
            ProductsPageHelper = new ProductsPageHelper(Driver);
            CartPageHelper = new CartPageHelper(Driver);
        }

        private static IWebDriver CreateWebDriver(BrowserType type)
            {
                IWebDriver driver = null;

                switch (type)
                {
                    case BrowserType.Ie:
                        driver = InternetExplorerDriver();
                        break;
                    case BrowserType.Firefox:
                        driver = FirefoxDriver();
                        break;
                    case BrowserType.Chrome:
                        driver = ChromeDriver();
                        break;
                }
                return driver;
            }

            private static IWebDriver InternetExplorerDriver()
            {
                var options = new InternetExplorerOptions
                {
                    EnsureCleanSession = true,
                    IgnoreZoomLevel = true,
                    EnableNativeEvents = false,
                    PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager,

                };
                IWebDriver driver = new InternetExplorerDriver(options);
                return driver;
            }

            private static IWebDriver FirefoxDriver()
            {
                FirefoxBinary timeoutbBinary = new FirefoxBinary();
                timeoutbBinary.Timeout = TimeSpan.FromMilliseconds(10000);
                FirefoxOptions options = new FirefoxOptions();
                options.SetPreference("pageLoadStrategy", "eager");
                options.SetPreference("acceptSslCerts", true);
                options.SetPreference("cssSelectorsEnabled", true);
                IWebDriver driver = new FirefoxDriver(options);
                return driver;
            }

            private static IWebDriver ChromeDriver()
            {
                IWebDriver driver = new ChromeDriver();
                return driver;
            }

            public enum BrowserType
            {
                Ie,
                Chrome,
                Firefox
            }

        [OneTimeSetUp]
        public virtual void Setup()
        {
            SetTimeoutOptions();
            BaseUrl = "http://localhost/litecart/admin";
            Driver.Navigate().GoToUrl(BaseUrl);
            Authorisation();
        }

        
        [OneTimeTearDown]
        public virtual void TearDown()
        {
            try
            {
                FindByXpathAndClick(".//*[@id='sidebar']//i[@class='fa fa-sign-out fa-lg']", "кнопку выхода");
            }
            finally 
            {
                Driver.Close();
                Driver.Quit();
            }
        }
    }
}
