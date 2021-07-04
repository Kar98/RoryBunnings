using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.WebCommon.Objects
{
    public abstract class PageBase
    {
        public IWebDriver driver;

        public PageBase(
            IWebDriver driv,
            By pageLocator = null,
            TimeSpan? timeout = null,
            bool autoLoad = true)
        {
            driver = driv;
            var tout = timeout ?? TimeSpan.FromSeconds(10);

            if (autoLoad)
            {
                ValidateElement(pageLocator, tout);
                Refresh();
            }
        }

        public void ValidateElement(By filter, TimeSpan timeout)
        {
            if (filter != null)
            {

                IWebElement item = driver.FindElementNoThrow(filter, timeout);

                if (item == null)
                {
                    throw new WebTestException($"Failed to find {filter} on {GetType()} Screen", null);
                }
            }
        }

        protected virtual void Navigate()
        {

        }

        /// <summary>
        /// Will reapply the values, not refresh the browser
        /// </summary>
        public void Refresh()
        {

            // iterate properties
            foreach (var property in this.GetType().GetRuntimeProperties())
            {
                var attrs = (PageItemAttribute[])property.GetCustomAttributes(typeof(PageItemAttribute), false);

                foreach (var attr in attrs)
                {
                    try
                    {
                        property.SetValue(this, driver.FindElement(attr.GetItemLocator()));
                    }
                    catch
                    {
                        if (attr.IsRequired)
                        {
                            throw;
                        }
                        else
                        {
                            property.SetValue(this, null);
                        }
                    }
                }
            }

            // iterate fields
            foreach (var field in this.GetType().GetRuntimeFields())
            {
                var attrs = (PageItemAttribute[])field.GetCustomAttributes
                    (typeof(PageItemAttribute), false);

                foreach (var attr in attrs)
                {
                    try
                    {
                        if (field.FieldType == typeof(IWebElement))
                            field.SetValue(this, driver.FindElement(attr.GetItemLocator()));
                        else if (field.FieldType == typeof(string))
                            field.SetValue(this, driver.FindElement(attr.GetItemLocator()).Text);
                        else if (field.FieldType == typeof(int))
                        {
                            var str = driver.FindElement(attr.GetItemLocator()).Text;
                            field.SetValue(this, Int32.Parse(str));
                        }
                        else if (field.FieldType == typeof(List<IWebElement>))
                        {
                            field.SetValue(this, driver.FindElements(attr.GetItemLocator()).ToList());
                        }
                    }
                    catch
                    {
                        if (attr.IsRequired)
                        {
                            throw;
                        }
                        else
                        {
                            field.SetValue(this, null);
                        }
                    }
                }
            }
        }
    }
}
