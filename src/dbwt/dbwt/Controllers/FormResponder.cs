using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;


namespace dbwt.Controllers
{
 
 
 
        public class FormResponder : Controller
        {


            public IActionResult Index()
            {
                return View();
            }
            

        [HttpGet]
        public ActionResult Write(String key, String value)
        {
            if(key == "" || value == ""){
                return Content("err");
            }

            DB_KV_ACCESS.Instance.write_db_date(new KV_PAIR(key, value));
            return Content("ok");
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Content(DB_KV_ACCESS.Instance.get_json_data());
        }

        [HttpGet]
        public ActionResult GetTable()
        {
            return Content(DB_KV_ACCESS.Instance.get_html_table());
        }

            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }



        }

}
