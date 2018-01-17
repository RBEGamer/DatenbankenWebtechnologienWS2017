using System;
namespace dbwt.Models
{
    public class RegistrationData
    {


        public bool imv_email = false;
        public bool imv_user = false;
        public bool req_ok = false;
        public string loginname = "";
        public string vorname = "";
        public string nachname = "";
        public string email = "";
        public string role = "";
        public string studgang = "";
        public string tel = "";
        public string buro = "";
        public string mannr = "";
        public string password = "";
        public string password_wdh = "";
        public string grund = "";
        public string ablauf = "";
           
          
        public RegistrationData()
        {
            imv_user = false;
            req_ok = false;
            imv_email = false;
        }
    }
}
