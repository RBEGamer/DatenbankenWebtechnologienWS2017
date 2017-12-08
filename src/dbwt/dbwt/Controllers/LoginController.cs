using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbwt.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            HttpContextNew.set_con(HttpContext);
            /*
            if (String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")))
            {
                ViewData["state"] = 0;
            }
            else
            {
                ViewData["state"] = 1;
                ViewData["user"] = HttpContext.Session.Get<String>("user");
            }
            */
            return View();
        }




        [HttpPost]
        public ActionResult Index(String username, String password, String action)
        {
            HttpContextNew.set_con(HttpContext);

            //ViewData["action"] = action;
            //ViewData["username"] = username;
            //ViewData["password"] = password;
            //ViewData["method"] = "post";

            //ViewData["method"] = "post";
            /*
            if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")) && action != null && action == "logout")
            {
                HttpContext.Session.Set<String>("user", null);
                HttpContext.Session.Set<String>("role", "Gast");
                ViewData["state"] = 0;
            }

                if (String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")))
            {

                if (username != null && password != null && action != null && action == "login")
                {
                    string hash_com = PasswordSecurity.PasswordStorage.CreateHash(password);
                    string hash_db = "";
                    string role = "";
                    MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben
                    try
                    {
                        con1.Open();
                        MySqlCommand cmd;
                        cmd = con1.CreateCommand();
                        cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Loginname`='"+username+"' LIMIT 1";
                        MySqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            hash_db += (String)r["Algorythmus"];
                            hash_db += ":";
                            hash_db += "64000";
                            hash_db += ":";
                            hash_db += ((uint)r["Strech"]).ToString();
                            hash_db += ":";
                            hash_db += (String)r["Salt"];
                            hash_db += ":";
                            hash_db += (String)r["Hash"];                
                            role = (String)r["Benutzerrolle"];
                        }
                        con1.Close();
                        if (PasswordSecurity.PasswordStorage.VerifyPassword(password, hash_db))
                        {
                            ViewData["state"] = 1;
                            ViewData["user"] = username;
                            HttpContext.Session.Set<String>("user", username);
                            HttpContext.Session.Set<String>("role", role);
                        }
                        else
                        {
                            ViewData["username"] = username;
                            ViewData["state"] = 3;
                        }

                    }
                    catch (Exception e)
                    {
                        ViewData["state"] = 2;
                    }                  
            }
                else
                {
                    ViewData["state"] = 0;
                }
            }
            else
            {
                ViewData["state"] = 1;
                ViewData["user"] = HttpContext.Session.Get<String>("user");
            }
            */
            return View();
        }


    }
}
