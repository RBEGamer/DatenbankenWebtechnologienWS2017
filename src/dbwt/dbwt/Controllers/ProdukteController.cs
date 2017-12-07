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
            ViewData["Title"] = "e-Mensa | Verfügbare Speisen";
            ViewData["KategorieName"] = "Bestseller";
           // ViewData["kategorie"] = gen_kategorie_cbx();
            return View();

        }

        //kategorie=2&only_avariable=only_avariable&without_meat=without_meat&only_vegan=only_vegan
        [HttpGet]
        public IActionResult Index(String kategorie, String only_avariable,String without_meat, String only_vegan)
        {
            if (kategorie == "" || kategorie == null)
            {
                ViewData["KategorieName"] = "Bestseller";
            }
            else
            {
                ViewData["KategorieName"] = DB_ACCESS.Instance.read_kategori_by_id(kategorie);
            }
            return View();
        }

        public List<ProdukteDesc> get_products_desc(bool is_vegan, bool is_vegetarisch, bool is_bio){


            List<ProdukteDesc> produkte = new List<ProdukteDesc>();




           


            //TODO LOAD PRODUCTS
            //SORT THEM BY FILTER
            return produkte;
        }




    
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
