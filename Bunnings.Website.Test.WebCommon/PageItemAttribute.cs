using OpenQA.Selenium;
using System;

namespace Bunnings.Website.Test.WebCommon
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PageItemAttribute : Attribute
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string XPath { get; set; }
        public string Name { get; set; }
        public string LinkText { get; set; }
        public string CssSelector { get; set; }
        public int TimeoutSec { get; set; }
        public bool IsRequired { get; set; }

        public PageItemAttribute(bool isRequired = false) : base()
        {
            IsRequired = isRequired;
        }

        public By GetItemLocator()
        {
            if (!string.IsNullOrWhiteSpace(this.Id))
            {
                return By.Id(this.Id);
            }

            if (!string.IsNullOrWhiteSpace(this.XPath))
            {
                return By.XPath(this.XPath);
            }

            if (!string.IsNullOrWhiteSpace(this.Class))
            {
                return By.ClassName(this.Class);
            }

            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                return By.Name(this.Name);
            }

            if (!string.IsNullOrWhiteSpace(this.LinkText))
            {
                return By.LinkText(this.LinkText);
            }

            if (!string.IsNullOrWhiteSpace(this.LinkText))
            {
                return By.CssSelector(this.CssSelector);
            }

            throw new NotImplementedException("Failed to get item locator on attribute.");
        }
    }
}
