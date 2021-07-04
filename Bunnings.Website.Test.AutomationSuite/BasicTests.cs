using Bunnings.Website.Test.PageObjects.PageObjects;
using Bunnings.Website.Test.WebCommon;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Bunnings.Website.Test.AutomationSuite
{

    [TestFixture]
    public class BasicTests
    {
        protected static IWebDriver d;
        private static string host;

        [OneTimeSetUp]
        public void Initialise()
        {
            if (d == null)
            {
                SeleniumUtils.KillWebDrivers();
                ChromeOptions co = new ChromeOptions();
                d = new ChromeDriver(co);
                co.AddArgument("-incognito");

                //host = test.Properties["web_host"].ToString();
                host = TestContext.Parameters["web_host"];
                d.Navigate().GoToUrl(host);
                d.Manage().Window.Maximize();
            }
            else
            {
            }
        }

        /// <summary>
        /// Basic reset method.
        /// You can have more complex ways of getting where you want the test to start.
        /// </summary>
        [SetUp]
        public void ResetContext()
        {
            d.Navigate().GoToUrl(host);
            // Easy way of getting back to the homepage. 
        }

        /// <summary>
        /// Positive test scenario expects at least 1 result. If 0 then error.
        /// </summary>
        [Test]
        public void SearchForItem()
        {
            //ResetContext();
            
            SearchWidget search = new SearchWidget(d);
            search.SearchForItem("conduit");
            SearchLandingPage landingPage = new SearchLandingPage(d);

            Assert.IsTrue(Regex.IsMatch(landingPage.results, "We found [1-9][0-9]* results for"));
        }

        /// <summary>
        /// Search for an item, then confirm that the item is displayed in the list when another search happens
        /// </summary>
        [Test]
        public void CheckRecentSearches()
        {
            //ResetContext();
            var searchTerm = "bbq";

            SearchWidget search = new SearchWidget(d);
            search.SearchForItem(searchTerm);
            new SearchLandingPage(d);// wait for landing page, otherwise we can get a StaleElement
            search.Refresh(); // Need to refresh
            var searchHistory = search.GetRecentSearches();
            Assert.That(searchHistory, Contains.Item(searchTerm));
            

        }

        [Test]
        public void UsePopularSearches()
        {
            //ResetContext();
            SearchWidget search = new SearchWidget(d);
            search.ClearRecentSearches();

            

        }


    }
}
