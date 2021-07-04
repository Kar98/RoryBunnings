using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Bunnings.Website.Test.AutomationSuite.DBObjs
{
    public class SqlConnector : IDisposable
    {
        private SqlConnection connection = null;

        public SqlConnector(string cnstr)
        {
            connection = new SqlConnection(cnstr);
        }

        public DataSet RunQuery(string query, Dictionary<string, object> parameters = null, SqlTransaction ts = null)
        {
            try
            {
                if (ts == null)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                }

                using (var cmd = connection.CreateCommand())
                {
                    cmd.Transaction = ts;
                    cmd.CommandText = query;
                    if (parameters != null)
                    {
                        FillParameterCollection(cmd.Parameters, parameters);
                    }

                    var ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        connection.Close();
                    }

                    return ds;
                }
            }
            catch
            {
                if (ts != null)
                {
                    ts.Rollback();
                }

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }

        private void FillParameterCollection(SqlParameterCollection collection, Dictionary<string, object> parameters)
        {
            foreach (var p in parameters)
            {
                if (p.Value == null)
                {
                    collection.AddWithValue(p.Key, DBNull.Value);
                }
                else
                {
                    collection.AddWithValue(p.Key, p.Value);
                }
            }
        }
    }
}
