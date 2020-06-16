using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain.Helper
{
    public class KeyValueSQlParameter
    {
        public KeyValueSQlParameter(string key, Object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get;}
        public Object Value { get;}
    }

    public class SQLRaw
    {
        public List<KeyValuePair<string, object>> Fields { get; set; }
    }

    public static class SQLHelper
    {
        public static List<SQLRaw> ExcecuteSQL(string connectionString, string queryString, params KeyValueSQlParameter[] parameters)
        {
            //string queryString = @"SELECT *
            //                      FROM Orders
            //                      INNER JOIN OrderItems ON Orders.Id=OrderItems.OrderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                foreach(var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    var result = new List<SQLRaw>();
                    //List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
                    while (reader.Read())
                    {
                        var raw = new SQLRaw()
                        {
                            Fields = new List<KeyValuePair<string, object>>()
                        };
                        for (int lp = 0; lp < reader.FieldCount; lp++)
                        {
                            raw.Fields.Add(new KeyValuePair<string, object> (reader.GetName(lp), reader.GetValue(lp)));
                        }
                        result.Add(raw);
                    }
                    return result;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }

            return null;
        }
    }
}
