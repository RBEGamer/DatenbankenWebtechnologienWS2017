using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace dbwt
{



    public class KV_PAIR{

        public string key = "--K--";
        public string value = "--V--";

        public KV_PAIR(string k, string v){
            key = k;
            value = v;
        }
    }
    public class DB_KV_ACCESS
    {
        public List<KV_PAIR> values;
        public DB_KV_ACCESS()
        {
            values = new List<KV_PAIR>();


            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "root";
            conn_string.Database = "dbwt_praktikum";
            conn_string.Port = 32768;

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `test_db` WHERE 1", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         values.Add(new KV_PAIR(reader.GetString(1), reader.GetString(2)));
                    }




                }
            }
        }

        Random rnd = new Random();
        public  override string ToString()
        {

            int _v = rnd.Next(0, values.Count());

            return "KEY : " + values[_v].key + " VALUE : " + values[_v].value + "";
           
  
        }


    }
}




