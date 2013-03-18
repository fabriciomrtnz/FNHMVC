using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE; //Requires IEDriverServer.exe search google code for it
using OpenQA.Selenium.Chrome; //Same as above
using OpenQA.Selenium.Safari; //Requires Safari 5 or above
using OpenQA.Selenium.Android; //Havent tested it. 

namespace FNHMVC.Test
{
    //Use this unit test to navigate through website and verify user interaction
    [TestClass()]
    public class NavigationTest
    {
        string URL {get; set;}

        [TestMethod()]
        public void SimpleNavigationTest()
        {
            // Create a new instance of the Firefox driver.

            // Notice that the remainder of the code relies on the interface, 
            // not the implementation.

            // Further note that other drivers (InternetExplorerDriver,
            // ChromeDriver, etc.) will require further configuration 
            // before this example will work. See the wiki pages for the
            // individual drivers at http://code.google.com/p/selenium/wiki
            // for further information.
            IWebDriver driver = new FirefoxDriver();

            //Notice navigation is slightly different than the Java version
            //This is because 'get' is a keyword in C#
            driver.Navigate().GoToUrl("http://www.google.com/");

            // Find the text input element by its name
            IWebElement query = driver.FindElement(By.Name("q"));

            // Enter something to search for
            query.SendKeys("Cheese");

            // Now submit the form. WebDriver will find the form for us from the element
            query.Submit();

            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });

            //Close the browser
            driver.Quit();
        }
    }
}
