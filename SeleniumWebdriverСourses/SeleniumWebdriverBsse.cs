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

        protected SeleniumWebdriverBase(BrowserType type):base(CreateWebDriver(type))
        {
            Browser = type;
            DesiredCapabilities desCaps = new DesiredCapabilities();
            desCaps.SetCapability("acceptSslCerts", true);
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
                DesiredCapabilities caps = DesiredCapabilities.Firefox();
                caps.SetCapability(CapabilityType.AcceptSslCertificates, true);
                caps.SetCapability(CapabilityType.PageLoadStrategy, "eager");
                caps.SetCapability(CapabilityType.SupportsFindingByCss, true);
                caps.SetCapability(CapabilityType.HasNativeEvents, false);
                IWebDriver driver = new FirefoxDriver(caps);
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
            BaseUrl = "http://software-testing.ru/";
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        
        [OneTimeTearDown]
        public virtual void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
