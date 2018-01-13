using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbwt.Controllers
{
    public class RegisterController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["imv_email"] = false;
            ViewData["imv_user"] = false;
            ViewData["req_ok"] = false;
            return View();
        }


        [HttpGet]
        public ActionResult Index(String d)
        {
            if (!String.IsNullOrEmpty(d)) {
            ViewData["req_ok"] = false;
            ViewData["loginname"] = "testuser";
            ViewData["vorname"] = "test";
            ViewData["nachname"] = "user";
            ViewData["email"] = "test@test.de";
            ViewData["role"] = "student";
            ViewData["studgang"] = "LOOOOL";
            ViewData["tel"] = "1337";
            ViewData["buro"] = "KEIN";
            ViewData["mannr"] = "424242";
            ViewData["password"] = "1234";
            ViewData["password_wdh"] = "1234";
            ViewData["imv_email"] = false;
            ViewData["imv_user"] = false;
                ViewData["grund"] = "Ich bin ein teichfisch";
                ViewData["ablauf"] = "2019-01-23";
            }

            return View();
        }



        [HttpPost]
        public ActionResult Index(String loginname,String vorname, String nachname, String email, String password, String password_wdh, String role, String matnr, String studgang, String tel, String buro, String mannr, String grund, String ablauf)
        {



            ViewData["req_ok"] = false;
            ViewData["loginname"] = loginname;
            ViewData["vorname"] = vorname;
            ViewData["nachname"] = nachname;
            ViewData["email"] = email;
            ViewData["role"] = role;
            ViewData["studgang"] = studgang;
            ViewData["tel"] = tel;
            ViewData["buro"] = buro;
            ViewData["mannr"] = mannr;
            ViewData["grund"] = grund;
            ViewData["ablauf"] = ablauf;
            ViewData["imv_email"] = false;
            ViewData["imv_user"] = false;

            if (password != "" && password_wdh == password)
            {
                ViewData["password"] = password;
                ViewData["password_wdh"] = password_wdh;
            }
            else
            {
                ViewData["password"] = "";
                ViewData["password_wdh"] = "";
                return View();
            }



            ViewData["imv_email"] = false;
            ViewData["imv_user"] = false;


            MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben
            con.Open();


            MySqlCommand cmd = con.CreateCommand();
            MySqlTransaction tr = con.BeginTransaction();
            cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Loginname` = '" + loginname +"'";
            MySqlDataReader r;
            try
            {




                cmd.Connection = con;
                cmd.Transaction = tr;
                 r = cmd.ExecuteReader();





                bool dup = false;
                while (r.Read())
                {
                    dup = true;
                }
                r.Close();
                if (dup)
                {
              
                    tr.Rollback();
                    //   con.Close();
                    ViewData["imv_user"] = true;
                    return View();
                }
             
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Email` = '"+email+"'";
                r = cmd.ExecuteReader();
                dup = false;
                while (r.Read())
                {
                    dup = true;
                }
                r.Close();
                if (dup)
                {
                    
                    tr.Rollback();
                    //  con.Close();
                    ViewData["imv_email"] = true;
                    return View();
                }
              

                string[] pw = PasswordSecurity.PasswordStorage.CreateHash(password).Split(':');
                //   format: algorithm: iterations: hashSize: salt: hash







                if (role == "student")
                {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '1', CURRENT_TIMESTAMP, '2017-01-23', 'Student', '0', '0');SELECT LAST_INSERT_ID();INSERT INTO `Student`(`Id`, `Matrikelnummer`, `Studiengang`, `FK_Fe-Nutzer`) VALUES (null,'"+matnr+"','"+studgang+"',LAST_INSERT_ID())";
                   
                }
                else if (role == "mitarbeiter")
                {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '1', CURRENT_TIMESTAMP, '2017-01-23', 'Mitarbeiter', '0', '0');INSERT INTO `dbwt_praktikum`.`Mitarbeiter` (`Id`, `Telefonnummer`, `MA-Nummer`, `Büro`, `FK_Fe-Nutzer`) VALUES (NULL, '"+tel+"', '"+mannr+"', '"+buro+ "', LAST_INSERT_ID());";
                  
                }
                else {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '1', CURRENT_TIMESTAMP, '2017-01-23', 'Gast', '0', '0');INSERT INTO `dbwt_praktikum`.`Gast` (`Id`, `Grund`, `Ablauf`, `FK_Fe-Nutzer`) VALUES (NULL, '"+grund+"', '"+ablauf+ "', LAST_INSERT_ID());";
                    
                }
                r= cmd.ExecuteReader();

                //TODO
                while (r.Read())
                {
              
                }


                r.Close();
               

                tr.Commit();


                

            }
            catch (Exception e)
            {
                //     con.Close();
               // r.Close();
                tr.Rollback();
         
                ViewData["imv_email"] = false;
                ViewData["imv_user"] = false;
                return View();
            }
            con.Close();

            ViewData["req_ok"] = true;
            return View();
        }


     



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
