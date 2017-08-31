using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebdriverСourses
{
    public abstract class SeleniumWebdriverContainer
    {
        protected IWebDriver Driver { get;}
        protected readonly Actions Actions;
        protected readonly Random RandomGenerator;

        protected SeleniumWebdriverContainer(IWebDriver driver)
        {
            Driver = driver;
            Actions = new Actions(Driver);
            RandomGenerator = new Random();
        }

        public void FindByAndClick(By by, string message)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Message = message;
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
            Driver.FindElement(by).Click();
        }

        public void FindByCssAndClick(string selector, string message)
        {
            FindByAndClick(By.CssSelector(selector), "Не удалось кликнуть на " + message);
        }

        public void FindByXpathAndClick(string selector, string message)
        {
            FindByAndClick(By.XPath(selector), "Не удалось кликнуть на " + message);
        }

        public void FindByLinkTextAndClick(string selector, string message)
        {
            FindByAndClick(By.LinkText(selector), "Не удалось кликнуть на " + message);
        }

        public void FindByIdAndClick(string selector, string message)
        {
            FindByAndClick(By.Id(selector), "Не удалось кликнуть на " + message);
        }

        public void FindByCssAndType(string selector, string text)
        {
            Driver.FindElement(By.CssSelector(selector)).Clear();
            Driver.FindElement(By.CssSelector(selector)).SendKeys(text);
        }

        public void FindByXpathAndType(string selector, string text)
        {
            Driver.FindElement(By.XPath(selector)).Clear();
            Driver.FindElement(By.XPath(selector)).SendKeys(text);
        }

        public void FindByIdAndType(string selector, string text)
        {
            Driver.FindElement(By.Id(selector)).Clear();
            Driver.FindElement(By.Id(selector)).SendKeys(text);
        }

        public void FindByNameAndType(string selector, string text)
        {
            Driver.FindElement(By.Name(selector)).Clear();
            Driver.FindElement(By.Name(selector)).SendKeys(text);
        }

        public bool CheckElementExists(By by)
        {
            return Driver.FindElements(by).Any();
        }

        public bool CheckElementIsVisible(By by)
        {
            return CheckElementExists(by) && Driver.FindElement(by).Displayed;
        }

        public void Wait(int timeout = 3000)
        {
            Thread.Sleep(timeout);
        }

        public void FindByXpathAndDoubleClick(string selector, string message = null)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Message = message;
            wait.Until(ExpectedConditions.ElementExists(By.XPath(selector)));
            Actions.DoubleClick(Driver.FindElement(By.XPath(selector))).Perform();
        }

        public void FindByNameAndClick(string selector, string message)
        {
            FindByAndClick(By.Name(selector), "Не удалось кликнуть на " + message);
        }

        public void SetTimeoutOptions()
        {
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        public virtual void Authorisation()
        {
            FindByNameAndType("username", "admin");
            FindByNameAndType("password", "admin");
            FindByNameAndClick("login", "кнопку логина");
        }
    }
}
