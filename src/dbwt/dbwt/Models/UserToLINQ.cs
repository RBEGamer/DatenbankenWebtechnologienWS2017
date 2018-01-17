using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace dbwt.Models
{
    public class UserToLINQ
    {

        XDocument d;


        public XDocument getLINQ_TABLE()
        {
            return d;
        }

            public UserToLINQ()
            {


            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe
            con1.Open();
            MySqlCommand cmd;

            List<FeNutzer> n = new List<FeNutzer>();
                //GET IMAGE
                try
                {
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE 1";
                    MySqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                    FeNutzer nutzer = new FeNutzer();
                    nutzer.Nr = int.Parse(r["Nr"].ToString());
                    nutzer.Aktiv = bool.Parse(r["Aktiv"].ToString());
                    nutzer.Vorname = r["Vorname"].ToString();
                    nutzer.Nachname = r["Nachname"].ToString();
                    nutzer.Loginname = r["Loginname"].ToString();
                    nutzer.Email = r["Email"].ToString();
                    nutzer.Hash = r["Hash"].ToString();
                    nutzer.Salt = r["Salt"].ToString();
                    nutzer.Algorythmus = r["Algorythmus"].ToString();
                    nutzer.Strech = int.Parse(r["Strech"].ToString());
                    nutzer.LetzterLogin = r["LetzterLogin"].ToString();
                    nutzer.Anlegedatum = r["Anlegedatum"].ToString();
                    nutzer.Benutzerrolle = r["Benutzerrolle"].ToString();
                    nutzer.verified = bool.Parse(r["verified"].ToString());
                    nutzer.admin = bool.Parse(r["admin"].ToString());
                    n.Add(nutzer);
                }
                }
                catch (Exception)
                {

                }




            List<XElement> elements = new List<XElement>();

            for (int i = 0; i < n.Count; i++)
            {

                XElement tmp = new XElement("Nutzer");
                tmp.Add(new XAttribute("Nr", n[i].Nr.ToString()));
                tmp.Add(new XAttribute("Aktiv", n[i].Aktiv));
                tmp.Add(new XAttribute("Vorname", n[i].Vorname.ToString()));
                tmp.Add(new XAttribute("Nachname", n[i].Nachname.ToString()));
                tmp.Add(new XAttribute("Email", n[i].Email.ToString()));
                tmp.Add(new XAttribute("Loginname", n[i].Loginname.ToString()));
                tmp.Add(new XAttribute("Hash", n[i].Hash.ToString()));
                tmp.Add(new XAttribute("Salt", n[i].Salt.ToString()));
                tmp.Add(new XAttribute("Algorythmus", n[i].Algorythmus.ToString()));
                tmp.Add(new XAttribute("Strech", n[i].Strech.ToString()));
                tmp.Add(new XAttribute("LetzterLogin", n[i].LetzterLogin.ToString()));
                tmp.Add(new XAttribute("Anlegedatum", n[i].Anlegedatum.ToString()));
                tmp.Add(new XAttribute("Benutzerrolle", n[i].Benutzerrolle.ToString()));
                tmp.Add(new XAttribute("verified", n[i].verified.ToString()));
                tmp.Add(new XAttribute("admin", n[i].admin.ToString()));
                elements.Add(tmp);
            }

             d = new XDocument(new XElement("FE-Nutzer", elements.ToArray()));
           
       

        }

           
        }
    
}
