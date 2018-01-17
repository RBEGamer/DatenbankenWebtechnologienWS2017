using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class FeNutzer
    {
        public int Nr { get; set; }
        public bool Aktiv { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Loginname { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Algorythmus { get; set; }
        public int Strech { get; set; }
        public string LetzterLogin { get; set; }
        public string Anlegedatum { get; set; }
        public string Benutzerrolle { get; set; }
        public bool verified { get; set; }
        public bool admin { get; set; }

    }
}
