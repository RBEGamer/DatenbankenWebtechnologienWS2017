using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Newtonsoft.Json;


using dbwt.Models;

namespace dbwt
{


    public class KAT_DESC
    {
        public String name;
        public int id;

       public KAT_DESC(String _n, int _id)
        {
            name = _n;
            id = _id;
        }
        public KAT_DESC()
        {

        }
    }

    public class DB_ACCESS
    {
        private static DB_ACCESS instance;
        private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();

        public String get_conn_string()
        {
            return conn_string.ToString();
        }

        public static DB_ACCESS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB_ACCESS();
                }
                return instance;
            }
        }

        public DB_ACCESS()
        {
            conn_string.Server = "marcelochsendorf.com";
            conn_string.UserID = "dbwt";
            conn_string.Password = "dbwt";
            conn_string.Database = "dbwt_praktikum";
            conn_string.Port = 3306;
        }


        public class DB_KAT_ENTRY{
            public int Id;
            public String Bezeichnung;
        }
        public class DB_KAT
        {
            public String Oberkategorie;
            public List<DB_KAT_ENTRY> entries;

            public DB_KAT(){
                entries = new List<DB_KAT_ENTRY>();
                entries.Clear();
            }

        }

        public List<DB_KAT> read_kategorien(){
            List<DB_KAT> read_kat = new List<DB_KAT>();
            //READ ALL CATEGORIES WITHOUT A PARENT CATEGORY
            DB_KAT kats = new DB_KAT();
            kats.Oberkategorie = "Hauptkategorie";
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();       
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Kategorie` WHERE `Oberkategorie` IS NULL", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        DB_KAT_ENTRY tmp = new DB_KAT_ENTRY();
                        tmp.Id = (int)reader["Id"];
                        tmp.Bezeichnung = (String)reader["Bezeichnung"];
                        kats.entries.Add(tmp);
                    }
                }
                conn.Close();
            }
            read_kat.Add(kats);
            return read_kat;
        }


        public String read_kategori_by_id(String _id)
        {
            String name = "";
            DB_KAT kats = new DB_KAT();
            kats.Oberkategorie = "Hauptkategorie";
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Kategorie` WHERE `Id`='"+ _id  +"'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        name = (String)reader["Bezeichnung"];
                 
                    }
                }
                conn.Close();
            }
      
            return name;
        }


        /*
                public void write_db_date(KV_PAIR _kv){
                    using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
                    {
                        conn.Open();   
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO `test_db` (`key`, `value`) VALUES ('" + _kv.key + "','"+ _kv.value + "')", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
        */







    }
}




