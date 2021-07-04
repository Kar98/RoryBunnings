using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.WebCommon
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAttribute : Attribute
    {
        public string RelativeUrl { get; set; }

        public PageAttribute() : base()
        {
        }
    }
}
