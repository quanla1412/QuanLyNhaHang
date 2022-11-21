using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.ConnectDB
{
    public class ConnectDB
    {
        private static string connectionString = ConnectionString._connectionString;
        private static SqlConnection _connection = null;
        public ConnectDB()
        {
        }

        public static List<List<string>> ReadData(string queryString)
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                }
                _connection.Open();

                SqlCommand sqlCommand = new SqlCommand(queryString, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<List<string>> list = new List<List<string>>();
                if(reader == null)
                {
                    list.Add(new List<string>());
                } else
                {
                    while (reader.Read())
                    {
                        List<string> listChild = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            listChild.Add(reader[i].ToString());
                        }
                        list.Add(listChild);
                    }
                }

                _connection.Close();

                return list;
            } catch
            {
                return null;
            }
        }

        public static bool InsertData(string queryString)
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                }
                _connection.Open();

                SqlCommand sqlCommand = new SqlCommand(queryString, _connection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result == 1)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }

        public static bool UpdateData(string queryString)
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                }
                _connection.Open();

                SqlCommand sqlCommand = new SqlCommand(queryString, _connection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result == 1)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
