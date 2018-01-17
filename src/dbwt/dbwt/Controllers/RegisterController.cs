using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using System.Diagnostics;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbwt.Controllers
{
    public class RegisterController : Controller
    {
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            RegistrationData regdata = new RegistrationData();
            ViewData["imv_email"] = false;
            regdata.imv_email = false;
            ViewData["imv_user"] = false;
            regdata.imv_user = false;
            ViewData["req_ok"] = false;
            regdata.req_ok = false;
            return View(regdata);
        }





        [HttpPost]
        public ActionResult Index(String loginname,String vorname, String nachname, String email, String password, String password_wdh, String role, String matnr, String studgang, String tel, String buro, String mannr, String grund, String ablauf)
        {

            RegistrationData regdata = new RegistrationData();

            ViewData["req_ok"] = false;
            regdata.req_ok = false;
            ViewData["loginname"] = loginname;
            regdata.loginname = loginname;
            ViewData["vorname"] = vorname;
            regdata.vorname = vorname;
            ViewData["nachname"] = nachname;
            regdata.nachname = nachname;
            ViewData["email"] = email;
            regdata.email = email;
            ViewData["role"] = role;
            regdata.role = role;
            ViewData["studgang"] = studgang;
            regdata.studgang = studgang;
            ViewData["tel"] = tel;
            regdata.tel = tel;
            ViewData["buro"] = buro;
            regdata.buro = buro;
            ViewData["mannr"] = mannr;
            regdata.mannr = mannr;
            ViewData["grund"] = grund;
            regdata.grund = grund;
            ViewData["ablauf"] = ablauf;
            regdata.ablauf = ablauf;
            ViewData["imv_email"] = false;
            regdata.imv_email = false;
            ViewData["imv_user"] = false;
            regdata.imv_user = false;

            if (password != "" && password_wdh == password)
            {
                ViewData["password"] = password;
                ViewData["password_wdh"] = password_wdh;
                regdata.password = password;
                regdata.password_wdh = password_wdh;
            }
            else
            {
                ViewData["password"] = "";
                ViewData["password_wdh"] = "";
                regdata.password = "";
                regdata.password_wdh = "";
                return View(regdata);
            }



            ViewData["imv_email"] = false;
            regdata.imv_email = false;
            ViewData["imv_user"] = false;
            regdata.imv_user = false;


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
                    regdata.imv_user = true;
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
                    regdata.imv_email = true;
                    return View();
                }
              

                string[] pw = PasswordSecurity.PasswordStorage.CreateHash(password).Split(':');
                //   format: algorithm: iterations: hashSize: salt: hash







                if (role == "student")
                {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '18', CURRENT_TIMESTAMP, '2017-01-23', 'Student', '0', '0');SELECT LAST_INSERT_ID();INSERT INTO `Student`(`Id`, `Matrikelnummer`, `Studiengang`, `FK_Fe-Nutzer`) VALUES (null,'"+matnr+"','"+studgang+"',LAST_INSERT_ID())";
                   
                }
                else if (role == "mitarbeiter")
                {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '18', CURRENT_TIMESTAMP, '2017-01-23', 'Mitarbeiter', '0', '0');INSERT INTO `dbwt_praktikum`.`Mitarbeiter` (`Id`, `Telefonnummer`, `MA-Nummer`, `Büro`, `FK_Fe-Nutzer`) VALUES (NULL, '"+tel+"', '"+mannr+"', '"+buro+ "', LAST_INSERT_ID());";
                  
                }
                else {
                    cmd.CommandText = "INSERT INTO `dbwt_praktikum`.`FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES (NULL, '0', '" + vorname + "', '" + nachname + "', '" + loginname + "', '" + email + "', '" + pw[4] + "', '" + pw[3] + "', 'sha1', '18', CURRENT_TIMESTAMP, '2017-01-23', 'Gast', '0', '0');INSERT INTO `dbwt_praktikum`.`Gast` (`Id`, `Grund`, `Ablauf`, `FK_Fe-Nutzer`) VALUES (NULL, '"+grund+"', '"+ablauf+ "', LAST_INSERT_ID());";
                    
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
                regdata.imv_user = false;
                regdata.imv_email = false;
                return View();
            }
            con.Close();

            ViewData["req_ok"] = true;
            regdata.req_ok = true;
            return View(regdata);
        }


     



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
