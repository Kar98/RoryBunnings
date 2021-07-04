using Bunnings.Website.Test.PageObjects.PageObjects;
using Bunnings.Website.Test.WebCommon;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using System.Collections.Generic;
using Bunnings.Website.Test.DBObjects;

namespace Bunnings.Website.Test.AutomationSuite
{

    [TestFixture]
    public class BasicTests
    {
        protected static IWebDriver d;
        private static string host;
        private string connStr;

        private bool mobileTest = false;

        [OneTimeSetUp]
        public void Initialise()
        {
            if (d == null)
            {
                host = TestContext.Parameters["web_host"];
                connStr = TestContext.Parameters["web_db"];
                this.mobileTest = TestContext.Parameters["is_mobile"].Contains("true") ? true : false;

                SeleniumUtils.KillWebDrivers();
                ChromeOptions co = new ChromeOptions();
                if (this.mobileTest)
                {
                    // Add mobile
                    co.EnableMobileEmulation("Galaxy S5");
                }
                d = new ChromeDriver(co);
                co.AddArgument("-incognito");

                d.Navigate().GoToUrl(host);
                if (!this.mobileTest)
                {
                    d.Manage().Window.Maximize();
                }
                
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
            SearchWidget search = new SearchWidget(d, mobileTest);
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
            var searchTerm = "bbq";

            SearchWidget search = new SearchWidget(d, mobileTest);
            search.SearchForItem(searchTerm);
            new SearchLandingPage(d);// wait for landing page, otherwise we can get a StaleElement
            search.Refresh(); // Need to refresh
            var searchHistory = search.GetRecentSearches();
            Assert.That(searchHistory, Contains.Item(searchTerm));
            

        }

        [Test]
        public void UsePopularSearches()
        {
            SearchWidget search = new SearchWidget(d, mobileTest);
            var frontEndSearchResults = search.GetPopularSearches();

            // Can do a test where we get the backend info from the DB (or wherever it's stored) and compare against the front end. 
            // I don't know how this data is stored so I've assumed a SQL db, but I'm sure it's easy enough to pipe in some other one.
            DbQueries db = new DbQueries(connStr);
            var dbSearchResults = db.GetPopularSearchesStubbed(); 

            // This will fail but you will see it output the current popular searches.
            Assert.AreEqual(frontEndSearchResults, dbSearchResults);
        }


    }
}
