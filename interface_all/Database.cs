using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace interface_all
{
    internal class Database
    {

        //建立只可以讀取的db資料
        static readonly string server = "140.118.197.223";
        static readonly string port = "3306";
        static readonly string user = "test";
        static readonly string password = "Aa12345@";
        static readonly string database = "try";
        //連線資訊
        public static string ConnString = $"server = {server} ; port = {port} ; user id = {user} ; password = {password} ; database = {database}";
        //連線
        public MySqlConnection conn = new MySqlConnection(ConnString);

        //判斷連現狀態=>連線成功回傳true
        public bool connect_db()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //判斷連現狀態=>關閉成功回傳true
        public bool close_db()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }




        //插入腦波資料
        public  void BulkInsertToMySQL_ch1( string tableName, List<string> rows)
         {
             using (MySqlConnection connection = new MySqlConnection(ConnString))
             {
                 connection.Open();

                 using (MySqlCommand command = new MySqlCommand())
                 {
                     command.Connection = connection;

                     // Build the SQL INSERT command
                     command.CommandText = $"INSERT INTO {tableName} (ch1) VALUES {string.Join(",", rows)}";

                     // Execute the command
                     command.ExecuteNonQuery();
                 }
             }
         }
 //清除3秒暫存腦波資料
        public void DELETETabledata_ch1(string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Build the SQL DROP command
                    command.CommandText = $"DELETE FROM {tableName} WHERE ch1 IS NOT NULL";

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }

        public void BulkInsertToMySQL_ch2(string tableName, List<string> rows)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Build the SQL INSERT command
                    command.CommandText = $"INSERT INTO {tableName} (ch2) VALUES {string.Join(",", rows)}";

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertNameToMySQL(string tableName, string name, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Build the SQL INSERT command
                    command.CommandText = $"INSERT INTO {tableName} (name,date) VALUES (@name,@date)";
                    // Add parameters
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@date", date);

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();
            var type = typeof(T);   
            //Add rows
            for(int i=0; i < list.Count(); i++)
            {
                dt.Rows.Add(dt.NewRow());
            }


            foreach (var prop in type.GetProperties())
            {
                DataColumn dc = new DataColumn(prop.Name);
                dc.DataType = prop.PropertyType;
                dt.Columns.Add(dc);

                //add data
                int rowIdx = 0;
                foreach (var item in list)
                {
                    DataRow dr = dt.Rows[rowIdx++];
                    dr[prop.Name] = prop.GetValue(item);
                }
            }
            return dt;
        }
    }

}
