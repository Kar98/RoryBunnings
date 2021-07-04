using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.WebCommon
{
    public static class IWebDriverEx
    {
        // TO-DO - Make the get/find element methods generic so you can get rid of all the variations
        public static IWebElement FindElement(this IWebDriver driver, By filter, TimeSpan timeout)
        {
            IWebElement element;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //element = wait.Until(ExpectedConditions.ElementIsVisible(filter));
            element = wait.Until(c => c.FindElement(filter));
            return element;
        }

        public static IWebElement FindElementNoThrow(this IWebDriver driver, By filter, TimeSpan? timeout = null)
        {
            try
            {
                return driver.FindElement(filter, timeout ?? TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public static IList<IWebElement> FindElementsTimed(this IWebDriver driver, By filter)
        {
            Stopwatch t = new Stopwatch();
            try
            {
                t.Start();
                return driver.FindElements(filter);
            }
            finally
            {
                t.Stop();
                //Console.WriteLine($"Find Elements {filter.ToString()} completed  {t.Elapsed.TotalSeconds}");
            }
        }

        public static IList<IWebElement> FindElementsTimedNoThrow(this IWebDriver driver, By filter)
        {
            try
            {
                return driver.FindElementsTimed(filter);
            }
            catch
            {
                return null;
            }
        }
    }
}
