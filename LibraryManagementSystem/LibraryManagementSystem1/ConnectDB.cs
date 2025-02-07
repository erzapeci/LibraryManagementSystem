using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem1
{
    internal class ConnectDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryManagementSystem"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
