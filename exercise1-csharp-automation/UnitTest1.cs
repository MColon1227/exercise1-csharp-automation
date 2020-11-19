using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace exercise1_csharp_automation
{
    public class Tests
    {
        IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            //Initialize Chrome driver
            _driver = new ChromeDriver();
        }

        [Test]
        public void Test1()
        {
            //Go to "Facebook" homepage
            _driver.Url = "https://www.facebook.com/";

            //Verify that the following text is displayed
            String textValidation = _driver.FindElement(By.CssSelector("._8eso")).Text;
            Assert.IsTrue(textValidation.Contains("Connect with friends and the world around you on Facebook."), textValidation + "does not exist");

            //Click in Create New Account button
            IWebElement createAccountButton = _driver.FindElement(By.CssSelector("form#u_0_a  a[role='button']"));
            createAccountButton.Click();
            
            //Fill Firstname, Lastname and Mobile Number.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement firstname = _driver.FindElement(By.Id("u_1_b"));
            firstname.Click();
            firstname.SendKeys("Marisol");
            
            IWebElement lastname = _driver.FindElement(By.Id("u_1_d"));
            lastname.Click();
            lastname.SendKeys("Colon");

            IWebElement movileNumber = _driver.FindElement(By.Id("u_1_g"));
            firstname.Click();
            firstname.SendKeys("ADDVALUE");
            movileNumber.SendKeys("2533627171");

            //Assert to validate the text "It’s quick and easy."
            Assert.IsTrue(_driver.PageSource.Contains("It’s quick and easy."));

            try
            {
                _driver.FindElement(By.XPath("//div[text() = 'test-exercise.']"));
            }

            catch (NoSuchElementException ex)
            {
                Debug.WriteLine("ExceptionHandled");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}