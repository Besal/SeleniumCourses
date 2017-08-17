using NUnit.Framework;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    //[TestFixture(BrowserType.Ie)]
    //[TestFixture(BrowserType.Firefox)]

    public class FirstStepsLesson : SeleniumWebdriverBase
    {
        public FirstStepsLesson(BrowserType browser)
            : base(browser)
        {
        }

        [OneTimeSetUp]
        public override void Setup()
        {
            SetTimeoutOptions();
            Driver.Navigate().GoToUrl("http://software-testing.ru/");
        }

        [Test(Description = "Проверка title http://software-testing.ru/")]

        public void GoToPageAndCheckTitle()
        {
            Assert.IsTrue(Driver.Title.Equals("Software-Testing.Ru"));
        }

        [OneTimeTearDown]

        public override void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
