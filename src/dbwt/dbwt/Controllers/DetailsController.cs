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
using Microsoft.AspNetCore.Mvc;

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
           
            if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && (HttpContext.Session.Get<String>("role") == "Student" || HttpContext.Session.Get<String>("role") == "Gast" || HttpContext.Session.Get<String>("role") == "Mitarbeiter"))
            {       
                ViewData["role"] = HttpContext.Session.Get<String>("role");
            }
            else
            {
                ViewData["role"] = "Gast";
            }
            if(id != null)
            {
                ViewData["prodid"] = id;
            }
            else
            {
                //TODO SET HEADER
            }
           
          
            return View();
        }
        
    }
}
