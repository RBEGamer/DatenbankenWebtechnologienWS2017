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

        public class ProdukteDesc{


        }

        public String set_kategorie = "";
        public IActionResult Index()
        {

            //1st  alle produkte inkl kategorie in ein feld
            //SELECT COUNT(*) FROM `Produkt` WHERE 1
            ViewData["title"] = "Verfügbare Speisen";
           // ViewData["kategorie"] = gen_kategorie_cbx();
            return View();

        }
        [HttpGet]
        public IActionResult Index(String kategorie)
        {
            //1st  alle produkte inkl kategorie in ein feld
            //SELECT COUNT(*) FROM `Produkt` WHERE 1
            //ViewData["title"] = "Verfügbare Speise (n "+kategorie+")";
            //ViewData["kategorie"] = gen_kategorie_cbx();
           // ViewData["Title"] = "";
            return View();

        }

        public List<ProdukteDesc> get_products_desc(bool is_vegan, bool is_vegetarisch, bool is_bio){


            List<ProdukteDesc> produkte = new List<ProdukteDesc>();




           


            //TODO LOAD PRODUCTS
            //SORT THEM BY FILTER
            return produkte;
        }


        public String gen_kategorie_cbx(){

            //SELECT * FROM `Kategorie` WHERE `Oberkategorie` IS NULL
            //<optgroup label='Hauptkategorie'>
            //</optgroup>

            List<DB_ACCESS.DB_KAT> lcat = DB_ACCESS.Instance.read_kategorien();
           


            String gen_cbx = "<select name='kategorie'>";
            gen_cbx += "<option value='1'> Döner </option>";
            gen_cbx += "</select";
            //TODO
            return gen_cbx;
        }

    
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
