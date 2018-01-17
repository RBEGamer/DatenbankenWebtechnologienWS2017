using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace dbwt.Controllers
{
    public class MenueXMLController : Controller
    {

        public MenuList ParseMenueFromXML(){

            XMLFactory xml = new XMLFactory();

            MenuList men = xml.getMenuList();
            MenuList res = new MenuList();

            try
            {  

                MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe
                con1.Open();
                MySqlCommand cmd;
                String img_path = "";
                if (men.Highlight >= 0) {
                    //GET IMAGE
                    try
                    {
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `Bild` WHERE `Id` = '"+ men.Highlight.ToString()+ "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        img_path = r["DetailsBildPfad"].ToString();
                    }
                    }
                    catch (Exception)
                    {

                 
                    }
                }
                men.Image = img_path;

                // res = men;
                res.Highlight = men.Highlight;
                res.Image = men.Image;
                res.Motto = men.Motto;
                res.Produkte = new List<MenuList.MenuProdukt>();
                res.Produkte.Clear();

                for (int i = 0; i < men.Produkte.Count; i++) {
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT `Gastbetrag`,`Studentenbetrag`,`Mitarbeiterbetrag`, `Produkt`.`Id` AS `Id`, `Bild`.`DetailsBildPfad`, `Beschreibung`, `Produkt`.`Titel` FROM `Produkt` LEFT JOIN `Preis` ON `Preis`.`Id` = `Produkt`.`FK_Preis` LEFT JOIN `Bild` ON `Bild`.`Id` = `Produkt`.`FK_Bild` WHERE `Produkt`.`Id`='" + men.Produkte[i].ID + "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        MenuList.MenuProdukt tmp = new MenuList.MenuProdukt();
                        tmp.ID = men.Produkte[i].ID;
                       tmp.Typ = men.Produkte[i].Typ;
                        tmp.Vegan = men.Produkte[i].Vegan;
                        tmp.Vegetarisch = men.Produkte[i].Vegetarisch;

                        tmp.preis = new MenuList.MenuProdukt.Preis(r["Studentenbetrag"].ToString(), r["Mitarbeiterbetrag"].ToString(), r["Gastbetrag"].ToString());
                       tmp.Beschreibung = r["Beschreibung"].ToString();
                        tmp.Name = r["Titel"].ToString();
                        res.Produkte.Add(tmp);
                    }
                    r.Close();



                }

              

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return res;
        }


        public IActionResult Index()
        {



            MenuList m = ParseMenueFromXML();
            //TODO TO 

            if (m.Image == "")
            {
                ViewData["img_path"] = "defaultbig.png";
            }
            else
            {
                ViewData["img_path"] = m.Image;
            }
            String tbl_string = "<table class='table table-dark'><thead><tr><th>TYP</th><th>Mahltzeit</th><th>Preis</th></tr></thead><tbody>";
            try
            {

            for (int i = 0; i < m.Produkte.Count(); i++)
            {

                String preis = "-- bitte erfragen --";

                if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && HttpContext.Session.Get<String>("role") == "Student")
                {
                    preis = m.Produkte[i].preis.Studentenbetrag.ToString() + "€";
                }

                else if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && HttpContext.Session.Get<String>("role") == "Mitarbeiterbetrag")
                {
                    preis = m.Produkte[i].preis.Mitarbeiterbetrag.ToString() + "€";
                }
                else
                {
                    preis = m.Produkte[i].preis.Gastbeitrag.ToString() + "€";
                }
                tbl_string += "<tr><td>" + m.Produkte[i].Typ.ToString() + "</td><td><p><b>" + m.Produkte[i].Name.ToString() + "</b></p><br><p>" + m.Produkte[i].Beschreibung.ToString() + "</p></td><td>" + preis + "</td></tr>";

            }
        }
               catch (Exception)
            {

             
            }


            tbl_string += "<tbody></table>";
            ViewData["table"] = tbl_string;
            return View();
        }
    }
}


