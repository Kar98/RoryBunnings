using OpenQA.Selenium;
using System;
using System.Reflection;

namespace Bunnings.Website.Test.WebCommon.Objects
{
    public abstract class WebPageBase : PageBase
    {
        protected Uri webroot;
        protected By pageLocator;
        public WebPageBase(
            IWebDriver driv,
            string WebRoot = null,
            By pageLocator = null,
            TimeSpan? timeout = null) : base(driv, pageLocator, timeout, false)
        {
            if (WebRoot != null)
            {
                webroot = new Uri(WebRoot);
            }
            this.pageLocator = pageLocator;
            Navigate();
            ValidateElement(pageLocator, timeout ?? TimeSpan.FromSeconds(20));
            Refresh();
        }

        public By GetPageLocator()
        {
            return this.pageLocator;
        }
        protected override void Navigate()
        {
            var pageAttribute = this.GetType().GetCustomAttribute(typeof(PageAttribute));
            if (pageAttribute != null && ((PageAttribute)pageAttribute).RelativeUrl != null)
            {
                Uri url = null;
                if (webroot != null)
                {
                    url = new Uri(webroot, ((PageAttribute)pageAttribute).RelativeUrl);
                }
                else
                {
                    url = new Uri(((PageAttribute)pageAttribute).RelativeUrl);
                }
                driver.Navigate().GoToUrl(url);
            }
        }
    }
}
