using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace dbwt.Controllers
{
    public class DetailsController : Controller
    {
       
       //role
        //preis
        //name
        //beschreibung
        //zutaten
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public ActionResult Index(String id)
        {
            

           
            return View();
        }

    }
}
