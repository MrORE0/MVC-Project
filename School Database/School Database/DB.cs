using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace School_Database
{
    class DB
    {
        public static SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0R58GC3\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True");

        public static SqlCommand cmd = new SqlCommand("", connection);
        public static DataSet dataSet;
        public static SqlDataAdapter dataAdapter;
        public static BindingSource bindingSrc;
        public static string sql;

        public static void openConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                   //if you want to check if your DB connection is okay or not
                   //MessageBox.Show("Connection is: " + connection.State.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Open Connection has Failed:" + ex.Message);
            }
        }

        public static void closeConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    //MessageBox.Show("Connection is: " + connection.State.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Close Connection Error:" + ex.Message);
            }
        }
    }
}
