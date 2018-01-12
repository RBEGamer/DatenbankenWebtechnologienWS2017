using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class Bestellung
    {
        public string nameOfUserWhoOrdered { get; set; }

        public DateTime timeOfOrder { get; set; }

        public List<Produkt> orderedProducts { get; set; }

    }
}
