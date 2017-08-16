using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeleniumWebdriverСourses
{
    [TestFixture(BrowserType.Chrome)]
    //[TestFixture(BrowserType.Ie)]
    //[TestFixture(BrowserType.Firefox)]

    class FirstStepsLesson : SeleniumWebdriverBase
    {
        public FirstStepsLesson(BrowserType browser)
            : base(browser)
        {
        }

        [Test(Description = "Проверка title http://software-testing.ru/")]

        public void GoToPageAndCheckTitle()
        {
            Assert.IsTrue(Driver.Title.Equals("Software-Testing.Ru"));
        }
    }
}
