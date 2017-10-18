﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;

namespace dbwt.Controllers
{
    public class HomeController : Controller
    {
        DB_KV_ACCESS t = new DB_KV_ACCESS();
       
        public IActionResult Index()
        {
            

            ViewData["data"] = t.ToString();
          
            return View();
        }

    

      //  public IActionResult Contact()
      //  {
      //      ViewData["Message"] = "Your contact page."
      //      return View();
       // }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
