using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Newtonsoft.Json;


using dbwt.Models;

namespace dbwt
{



    public class KV_PAIR{
        public string key { get; set; }
        public string value { get; set; }

        public KV_PAIR(string k, string v){
            key = k;
            value = v;
        }

        public override string ToString()
        {
            return string.Format("[KV_PAIR: key={0}, value={1}]", key, value);
        }
    }


    public class DB_KV_ACCESS
    {

        private static DB_KV_ACCESS instance;

        public List<KV_PAIR> values;
        MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();


        public static DB_KV_ACCESS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB_KV_ACCESS();
                }
                return instance;
            }
        }

        public DB_KV_ACCESS()
        {
            values = new List<KV_PAIR>();

            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "root";
            conn_string.Database = "dbwt_praktikum";
            conn_string.Port = 32770;

            read_db_data();
        }


        void read_db_data(){
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();
                values.Clear();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `test_db` WHERE 1", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(new KV_PAIR(reader.GetString(1), reader.GetString(2)));
                    }
                }
                conn.Close();
            }
        }



        public void write_db_date(KV_PAIR _kv){
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();   
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `test_db` (`key`, `value`) VALUES ('" + _kv.key + "','"+ _kv.value + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public  override string ToString()
        {
            read_db_data();
            return "KEY : " + values[values.Count()-1].key + " VALUE : " + values[values.Count()-1].value + "";
        }

        public string get_json_data(){
            read_db_data();
           return JsonConvert.SerializeObject(values);
        }


        public string get_html_table(){
            read_db_data();
            string table = "<table width='100%'><tr><th>KEY</th><th>VALUE</th></tr>";
            foreach (KV_PAIR n in values)
            {
                table += "<tr><td>"+ n.key+"</td><td>"+ n.value+"</td></tr>";
            }
            return table;
        }
    }
}




