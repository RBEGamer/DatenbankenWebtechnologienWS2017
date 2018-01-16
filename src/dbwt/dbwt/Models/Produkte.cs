using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace dbwt
{

    public class MenuList
    {
        public List<MenuProdukt> Produkte = null;
        public string Motto { get; set; }

        public class MenuProdukt
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }


        public MenuList()
        {
            Produkte = new List<MenuProdukt>();
        }

        public void addProdukt(int id, string name)
        {
            MenuProdukt item = new MenuProdukt();
            item.ID = id;
            item.Name = name;

            Produkte.Add(item);
        }

    }
}