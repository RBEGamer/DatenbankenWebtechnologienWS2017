using System;
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
       
       
        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("user"))){ ViewData["login"] = true; } else { ViewData["login"] = false; }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
