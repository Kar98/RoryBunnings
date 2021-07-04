using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunnings.Website.Test.AutomationSuite.DBObjs
{
    public class DbQueries
    {
        readonly string connStr;

        public DbQueries(string connStr)
        {
            this.connStr = connStr;
        }

        public string GetGenericCustomer()
        {
            string query = "Select TOP 1 CustomerID FROM Customers WHERE SomethingUseful = 1";
            return RunQuery(query);
        }

        private string RunQuery(string query)
        {
            using (SqlConnector conn = new SqlConnector(connStr))
            {
                try
                {
                    Console.WriteLine(query);
                    var usefulValue = conn.RunQuery(query).Tables[0].Rows[0][0].ToString();
                    return usefulValue;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception("The query did not return any results");
                }
            }
        }
    }
}
