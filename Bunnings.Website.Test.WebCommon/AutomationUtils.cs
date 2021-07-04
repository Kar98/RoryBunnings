using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Bunnings.Website.Test.WebCommon
{
    public class AutomationUtils
    {
        private static TimeSpan timeout = TimeSpan.FromSeconds(10); // can be made to be configurable from test config.
        public static IWebElement WaitForWithContext(IWebDriver driver, IWebElement element, By filter)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            element = wait.Until(c => c.FindElement(filter));
            return element;
        }

        public static IWebElement WaitForNoThrowWithContext(IWebDriver driver, IWebElement element,By filter)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            try
            {
                element = wait.Until(c => c.FindElement(filter));
                return element;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
