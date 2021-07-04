using System;
using System.Collections.Generic;
using System.Data;

namespace Bunnings.Website.Test.DBObjects
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
            return RunQuerySingleReturn(query);
        }

        public List<string> GetPopularSearches()
        {
            string query = "Select PopularWords FROM PopularSearch";
            var res = RunQueryDataSet(query);
            // foreach() { do some data transformation here to output into List } 
            var returnList = new List<string>();
            return returnList;
        }

        public List<string> GetPopularSearchesStubbed()
        {
            var returnList = new List<string>();
            return returnList;
        }

        /// <summary>
        /// This needs a proper connection string, but it will work.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns></returns>
        private string RunQuerySingleReturn(string query)
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

        private DataSet RunQueryDataSet(string query)
        {
            using (SqlConnector conn = new SqlConnector(connStr))
            {
                try
                {
                    Console.WriteLine(query);
                    var usefulValue = conn.RunQuery(query);
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
