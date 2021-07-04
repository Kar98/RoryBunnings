using Bunnings.Website.Test.WebCommon;
using Bunnings.Website.Test.WebCommon.Objects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.PageObjects.PageObjects
{
    public class SearchWidget : WebPageBase
    {
        [PageItem(Id = "custom-css-outlined-input")]
        public IWebElement searchBox;
        [PageItem(XPath = "//button[@id='crossIcon']")]
        public IWebElement searchButton;
        
        public SearchWidget(IWebDriver driv) : base(driv, pageLocator: By.Id("header-search"))
        {

        }

        public void OpenFlyout()
        {
            //If flyout not open, then open, otherwise load already opened flyout
            if (driver.FindElementNoThrow(By.Id("flyout"),TimeSpan.FromSeconds(2)) == null)
            {
                searchBox.Click();
            }
            new SearchFlyout(driver); // Do this to wait for the element to be loaded
            // Better than doing another wait.until(blah) as it will mean we need to change code in 2 spots.
        }

        public void SearchForItem(string itemText)
        {
            searchBox.SendKeys(itemText);
            Refresh(); // Need to call refresh, because the button is only displayed when text is entered.
            searchButton.Click();
        }

        public bool IsRecentSearchMode()
        {
            if (driver.FindElement(By.Id("SearchFlyout")).Displayed)
            {
                SearchFlyout flyout = new SearchFlyout(driver);
                return flyout.IsRecentSearchMode();
            }
            else
            {
                return false; // Could also throw exception if we wanted to differentiate between it actually
                // not being visible, vs the flyout not being displayed.
            }
        }

        public List<string> GetRecentSearches()
        {
            OpenFlyout();
            SearchFlyout flyout = new SearchFlyout(driver);
            if (!flyout.IsRecentSearchMode())
            {
                // Can also return empty List, depends on how you want to use this.
                // Can also use a custom exception to filter out front end exceptions and selenium exceptions
                throw new Exception("No recent searches have been done. Ensure a search has happened beforehand");
            }
            return flyout.GetRecentSearches();
        }

        public void ClearRecentSearches()
        {
            OpenFlyout();
            SearchFlyout flyout = new SearchFlyout(driver);
            if (flyout.IsRecentSearchMode())
            {
                flyout.ClickClearRecentSearches();
            }
        }

        public List<string> GetPopularSearches()
        {
            OpenFlyout();
            SearchFlyout flyout = new SearchFlyout(driver);
            if (flyout.IsRecentSearchMode())
            {
                flyout.ClickClearRecentSearches();
            }
            return flyout.GetPopularSearches();
        }
    }

    

    public class SearchFlyout : WebPageBase
    {
        [PageItem(XPath = "//div[contains(@class,'MuiGrid-root leftSection')]")]
        IWebElement leftSection;
        [PageItem(XPath = "//button[contains(@class,'ClearSuggestionsContainer')]")]
        IWebElement clearRecentSearchButton;

        public SearchFlyout(IWebDriver driv) : base(driv, pageLocator: By.Id("flyout"))
        {
        }

        public bool IsRecentSearchMode()
        {
            if(leftSection == null)
            {
                return false;
            }
            else
            {
                try
                {
                    var el = leftSection.FindElement(By.XPath(".//button[contains(@class,'ClearSuggestionsContainer')]"));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public void ClickClearRecentSearches()
        {
            clearRecentSearchButton.Click();
            AutomationUtils.WaitForWithContext(driver, leftSection, By.XPath(".//div[contains(@class,'PopularRecentSuggestions')]"));
        }

        public List<string> GetRecentSearches()
        {
            var searchItems = leftSection.FindElements(By.XPath(".//div[contains(@class,'terms')]")).ToList();
            List<string> returnList = new List<string>();
            searchItems.ForEach(e => returnList.Add(e.Text));
            return returnList;
        }

        public List<string> GetPopularSearches()
        {
            var searchItems = leftSection.FindElements(By.XPath(".//div[contains(@class,'terms')]")).ToList();
            List<string> returnList = new List<string>();
            searchItems.ForEach(e => returnList.Add(e.Text));
            return returnList;
        }
    }

}
