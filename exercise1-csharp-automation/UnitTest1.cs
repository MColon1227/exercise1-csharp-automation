using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Diagnostics;

namespace exercise1_csharp_automation
{
  public class WebDriverFactory
  {
    public static IWebDriver _driver;

    protected WebDriverFactory(BrowserType type)
    {
      _driver = WebDriver(type);
    }

    public enum BrowserType
    {
      Chrome,
      Firefox
    }

    [TearDown]
    public void Close()
    {
      _driver.Close();
    }

    public static IWebDriver WebDriver(BrowserType type)
    {
      IWebDriver driver = null;
      switch (type)
      {
        case BrowserType.Firefox:
          driver = FirefoxDriver();
          break;
        case BrowserType.Chrome:
          driver = ChromeDriver();
          break;
        default:
          throw new NotSupportedException();
      }
      return driver;
    }

    private static IWebDriver FirefoxDriver()
    {
      Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\Users\marisol.colon\RiderProjects\geckodriver.exe");
      FirefoxOptions options = new FirefoxOptions();
      IWebDriver driver = new FirefoxDriver(options);
      return driver;

    }

    private static IWebDriver ChromeDriver()
    {
      Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Users\marisol.colon\RiderProjects\chromedriver.exe");
      ChromeOptions options = new ChromeOptions();
      IWebDriver driver = new ChromeDriver(options);
      return driver;
    }
  }

  class SetMethods
  {
    #region FIELDS

    public const string TEST_PAGE = "https://www.google.com/";
    public const string FACEBOOK = "https://www.facebook.com/";

    public static void EnterText(IWebDriver _driver, string element, string value, string elementtype)
    {
      if (elementtype == "Id")
        _driver.FindElement(By.Id(element)).SendKeys(value);
      if (elementtype == "Name")
        _driver.FindElement(By.Name(element)).SendKeys(value);
      if (elementtype == "CssSelector")
        _driver.FindElement(By.CssSelector(element)).Click();

      Console.WriteLine(value);

    }

    public static void Click(IWebDriver _driver, string element, string elementtype)
    {
      if (elementtype == "Id")
        _driver.FindElement(By.Id(element)).Click();
      if (elementtype == "CssSelector")
        _driver.FindElement(By.CssSelector(element)).Click();
      if (elementtype == "Name")
        _driver.FindElement(By.Name(element)).Click();
    }

    #endregion

  }

  [TestFixture(BrowserType.Chrome)]
  [TestFixture(BrowserType.Firefox)]

  public class Tests : WebDriverFactory
  {
    

    public Tests(BrowserType browser) : base(browser) { }

    [Test]
    public void Test()
    {

      //Go to "Facebook" homepage
      _driver.Navigate().GoToUrl(SetMethods.FACEBOOK);

      //Verify that the following text is displayed, "the UI for the message was changed"
      String textValidation = _driver.FindElement(By.CssSelector("._8eso")).Text;
      Assert.IsTrue(textValidation.Contains("Connect with friends and the world around you on Facebook."), textValidation + "does not exist");
      Console.WriteLine("Successfully passed!");

      //Click in Create New Account button
      SetMethods.Click(_driver , "._6ltg a[role='button']", "CssSelector");

      //Fill Firstname, Lastname and Mobile Number.
      _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

      SetMethods.Click(_driver, "firstname", "Name");
      SetMethods.EnterText(_driver, "firstname", "Marisol", "Name");
      

      SetMethods.Click(_driver, "lastname", "Name");
      SetMethods.EnterText(_driver, "lastname", "Colon", "Name");

      SetMethods.EnterText(_driver, "reg_email__", "33-16785915", "Name");
 
      //Add other name
      SetMethods.Click(_driver, "firstname", "Name");
      SetMethods.EnterText(_driver, "firstname", "Other-Name", "Name");
    }

    [Test]
    public void Test2()
    {
      try
      {
        _driver.Navigate().GoToUrl(SetMethods.FACEBOOK);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        string elementDoNotExist = _driver.FindElement(By.XPath("//html[@id='facebook']//div[@id='reg_pages_msg']")).Displayed.ToString();
        if (elementDoNotExist == "Create a Page")
        {
          Console.WriteLine("Text: exist");
        }
        else
        {
          Console.WriteLine("Text: do not exist ");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine( e.Message );
      }
    }
  }
}

