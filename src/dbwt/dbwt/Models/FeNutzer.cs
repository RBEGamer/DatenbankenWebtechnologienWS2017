using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class FeNutzer
    {
        public DateTime CreatedOnDate { get; set; }
      
        public string NameWithEmail { get; set; }

        public DateTime LastLogin { get; set; }

        public bool isAdmin { get; set; }

        public bool isVerified { get; set; }

        public String Rolle { get; set; }

        public int FeUserID { get; set; }
    }
}
