using System.Collections.Generic;
using NUnit.Framework;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    //[TestFixture(BrowserType.Ie)]
    public class CheckRegistration : SeleniumWebdriverBase
    {
        public CheckRegistration(BrowserType browser) 
            : base(browser)
        {
        }

        [Test]
        public void CheckingRegistration()
        {
            FindByXpathAndClick(".//*[@id='sidebar']//i[@class='fa fa-chevron-circle-left']",
            "кнопку главной страницы");
            FindByXpathAndClick(
                ".//*[@id='box-account-login']//a[@href and contains(text(), 'New customers click here')]",
                "кнопку регистрации пользователя");
            var eMail = GenerateEmail();
            var password = GeneratePassword();
            var fieldsAndValues = new Dictionary<string, string>
            {
                {"firstname", "testfirstname"},
                {"lastname", "testlastname"},
                {"address1", "address"},
                {"postcode", "55555"},
                {"city", "testcity"},
                {"email", eMail},
                {"phone", "+112345678"},
                {"password", password},
                {"confirmed_password", password}
            };
            foreach (var field in fieldsAndValues)
            {
                FindByNameAndType(field.Key, field.Value);
            }
            FindByXpathAndClick(".//*[@class='select2-hidden-accessible']/option[text()= 'United States']", "кнопку выбора страны");
            FindByXpathAndClick("//select[@name='zone_code']/option[text()='California']", "кнопку выбора штата");
            FindByNameAndClick("create_account", "кнопку создания аккаунта");
            FindByXpathAndClick(".//*[@id='box-account']//a[@href and text()='Logout']", "кнопку \"Logout\"");
            FindByNameAndType("email", eMail);
            FindByNameAndType("password", password);
            FindByNameAndClick("login", "кнопку \"Login\"");
            FindByXpathAndClick(".//*[@id='box-account']//a[@href and text()='Logout']", "кнопку \"Logout\"");
        }

        public string GenerateEmail()
        {
            return $"regtest{RandomGenerator.Next(0, 10000)}@tes{RandomGenerator.Next(0, 10000)}il.klj";
        }

        public string GeneratePassword()
        {
            return $"regtest{RandomGenerator.Next(0, 100000)}";
        }

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
