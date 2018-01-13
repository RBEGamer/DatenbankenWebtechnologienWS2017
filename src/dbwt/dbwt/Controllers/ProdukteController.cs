using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using MySql.Data.MySqlClient;

namespace dbwt.Controllers
{
    public class Produkte : Controller
    {

        public class ProdukteDesc
        {


        }

        public String set_kategorie = "";
        public IActionResult Index()
        {
            HttpContextNew.set_con(HttpContext);
            //1st  alle produkte inkl kategorie in ein feld
            //SELECT COUNT(*) FROM `Produkt` WHERE 1
            ViewData["Title"] = "e-Mensa | Verfügbare Speisen";
            ViewData["KategorieName"] = "Bestseller";
            // ViewData["kategorie"] = gen_kategorie_cbx();
            return View();

        }


        [HttpGet]
        public IActionResult Index(String kategorie, String only_avariable, String without_meat, String only_vegan, String limit)
        {

            //TODO  REMOVE FOR ASPNET CORE 2.0
            if (!String.IsNullOrEmpty(limit))
            {
                ViewData["limit"] = limit;
            }



            if (String.IsNullOrEmpty(HttpContext.Request.Query["kategorie"]) || HttpContext.Request.Query["kategorie"] == "")
            {
                ViewData["KategorieName"] = "Bestseller";
            }
            else
            {
                ViewData["KategorieName"] = DB_ACCESS.Instance.read_kategori_by_id(HttpContext.Request.Query["kategorie"].ToString());
                ViewData["KategorieId"] = HttpContext.Request.Query["kategorie"].ToString();
            }

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["limit"].ToString()))
            {
                ViewData["limit"] = HttpContext.Request.Query["limit"].ToString();
            }


            MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben
            try
            {
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Kategorie` WHERE `Oberkategorie` IS NULL";
                MySqlDataReader r = cmd.ExecuteReader();
                List<KAT_DESC> under_categories = new List<KAT_DESC>();

                while (r.Read())
                {
                    KAT_DESC tmp = new KAT_DESC();
                    tmp.id = (int)r["Id"];
                    tmp.name = (String)r["Bezeichnung"];
                    under_categories.Add(tmp);

                }
                con.Close();
                string kat_data = " <select name='kategorie'>";
                foreach (KAT_DESC n in under_categories)
                {
                    con.Open();
                    MySqlCommand cmd1;
                    cmd1 = con.CreateCommand();
                    cmd1.CommandText = "SELECT * FROM `Kategorie` WHERE `Oberkategorie`='" + n.id.ToString() + "'";
                    MySqlDataReader rkat = cmd1.ExecuteReader();
                    List<KAT_DESC> ucat = new List<KAT_DESC>();
                    while (rkat.Read())
                    {
                        ucat.Add(new KAT_DESC((String)rkat["Bezeichnung"], (int)rkat["Id"]));
                    }
                    con.Close();
                    if (ucat.Count() <= 0) { continue; }
                    kat_data += "<optgroup label=\"" + n.name + "\">";
                    foreach (KAT_DESC m in ucat)
                    {
                        kat_data += "<option value=\"" + m.id.ToString() + "\">" + m.name + "</ option >";
                    }
                    kat_data += "</optgroup>";  
                }
                ViewData["KAT_OBJ_DATA"] = kat_data + "</select>";
            }
            catch (Exception e)
            {

            }





            //SELECT * FROM `Produkt` LEFT JOIN `Bild` ON `Produkt`.`FK_bild` = `Bild`.`Id` WHERE 1
            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben



            try
            {
                String out_prod = "";
                con1.Open();
                MySqlCommand cmd;
                cmd = con1.CreateCommand();
                String cmdb = "SELECT * FROM `Produkt` LEFT JOIN `Bild` ON `Produkt`.`FK_bild` = `Bild`.`Id` WHERE 1 ORDER BY RAND()";
                if (ViewData["KategorieId"] != null)
                {
                    cmdb = "SELECT * FROM `Produkt` LEFT JOIN `Bild` ON `Produkt`.`FK_bild` = `Bild`.`Id` WHERE `FK_Kategorie`='" + ViewData["KategorieId"] + "' ORDER BY RAND()";
                }

                if (ViewData["limit"] != null)
                {
                    cmdb += " LIMIT " + ViewData["limit"];
                }
                cmd.CommandText = cmdb;
                MySqlDataReader r = cmd.ExecuteReader();
                List<PROD_DESC> products = new List<PROD_DESC>();
                int c = 0;
                while (r.Read())
                {
                    c++;
                    PROD_DESC tmp = new PROD_DESC();
                    tmp.Id = (int)r["Id"];
                    tmp.Beschreibung = (String)r["Titel"];
                    tmp.Ausverkauft = (bool)r["Ausverkauft"];
                    tmp.AltText = (String)r["Alt-Text"];
                    tmp.BildPfad = (String)r["IconBildPfad"];
                    tmp.BildPfadGray = (String)r["IconBildPfadGray"];
                    products.Add(tmp);
                }
                con1.Close();

                if (c <= 0)
                {
                    con1.Open();
                    out_prod += "<script>alert('UNGÜLTIGER PARAMETER')</script>";
                      ViewData["header_addition"] = "<meta http-equiv=\"refresh\" content=\"3; url=/\"/>";


                    ViewData["KategorieName"] = "Bestseller";
                    cmdb = "SELECT * FROM `Produkt` LEFT JOIN `Bild` ON `Produkt`.`FK_bild` = `Bild`.`Id` WHERE 1 ORDER BY RAND()";
                    cmd.CommandText = cmdb;
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                        PROD_DESC tmp = new PROD_DESC();
                        tmp.Id = (int)r["Id"];
                        tmp.Beschreibung = (String)r["Titel"];
                        tmp.Ausverkauft = (bool)r["Ausverkauft"];
                        tmp.AltText = (String)r["Alt-Text"];
                        tmp.BildPfad = (String)r["IconBildPfad"];
                        tmp.BildPfadGray = (String)r["IconBildPfadGray"];
                        products.Add(tmp);
                    }
                    con1.Close();
                }
                const int items_per_row = 5;
                int rows = (int)(products.Count() / items_per_row);


                out_prod += "<div class=\"row\" style=\"height: 200px; \">";
                int rc = 0;
                for (int i = 0; i < products.Count(); i++)
                {

                    rc++;
                    if (rc >= items_per_row)
                    {
                        rc = 0;
                        out_prod += "</div>";
                        out_prod += "<div class=\"row\" style=\"height: 200px; \">";
                    }


                    String vg_text = "-- NO DATA --";
                    String gclass = "";
                    if (products[i].Ausverkauft)
                    {
                        out_prod += "<div class=\"col-md-2\">";
                        out_prod += "<div class=\"speise-"+products[i].Id.ToString()+"\">";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div class=\"col-md-12\"><img class=\"img-responsive\" src=\"./assets/images/speise_icons/"+products[i].BildPfadGray+"\" width=\"100px\" height=\"100px\" alt=\""+products[i].AltText+"\"></div>";
                              out_prod +="</div>";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div class=\"col-md-12 product_sold_out_text\"><span>"+products[i].Beschreibung+"</span></div>";
                        out_prod += "</div>";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div class=\"col-md-12 product_sold_out_text\">VERGRIFFEN</div>";
                        out_prod += "</div>";
                        out_prod += "</div>";
                        out_prod += "</div>";
                    }
                    else
                        {
                        out_prod += "<div class=\"col-md-2\">";
                        out_prod += "<div class=\"speise-"+products[i].Id.ToString()+"\">";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div><img class=\"img-responsive\" src=\"./assets/images/speise_icons/"+products[i].BildPfad+"\" width=\"100px\" height=\"100px\"></div>";
                        out_prod += "</div>";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div class=\"col-md-12\"><span>"+products[i].Beschreibung+"</span></div>";
                        out_prod += "</div>";
                        out_prod += "<div class=\"row\">";
                        out_prod += "<div class=\"col-md-12\"><a href=\"Details?id="+products[i].Id.ToString()+"\"> Details </a></div>";
                        out_prod += "</div>";
                        out_prod += "</div>";
                        out_prod += "</div>";
                    }



                    


                }
                out_prod += "</div>";


               ViewData["PROD"] = out_prod;



                }
                catch (Exception e)
                {
                    
                }

            return View();
        }






        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
