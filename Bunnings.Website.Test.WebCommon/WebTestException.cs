using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.WebCommon
{
    public class WebTestException : Exception
    {
        public WebTestException(string message) : base(message)
        {

        }

        public WebTestException(string message, Exception ex) : base(message, ex)
        {

        }


    }
}
