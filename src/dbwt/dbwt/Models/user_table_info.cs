using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class user_table_info
    {
        public List<user_table> list_anmeldung { get; set; }
        public List<user_table> list_registrierung { get; set; }

        public user_table_info(List<user_table> _a, List<user_table> _b)
        {
            list_registrierung = _a;
            list_anmeldung = _b;
        }
    }
}
