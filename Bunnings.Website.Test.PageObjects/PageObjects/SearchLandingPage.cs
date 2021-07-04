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
    public class SearchLandingPage : WebPageBase
    {
        [PageItem(XPath = "//div[@class='totalResults']")]
        public string results;

        public SearchLandingPage(IWebDriver driv) : base(driv, pageLocator: By.XPath("//div[@class='searchTerm']"))
        {
        }
    }
}
