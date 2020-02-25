using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace BillAutomation      //DO NOT change the namespace name
{
    public static class DBHandler    //DO NOT change the class name
    {
        //Implement the methods as per the description
        static string connStr = ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }
    }
}
