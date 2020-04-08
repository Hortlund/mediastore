using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediastore
{
    class Produkt
    {
        // Simpel get; set för alla attributer som produkten har i min lösning, härifrån skapas alla nya objekt som läggs i listorna.
        public string name { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public string supplier { get; set; }

        /*Used for debugging purposes
        public override string ToString()
        {
            
            return id +" "+ name + " " + price + " " + amount;
        }
        */


    }
}
