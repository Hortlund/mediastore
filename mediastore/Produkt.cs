using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediastore
{
    class Produkt
    {
        public string name { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public string supplier { get; set; }



        /*private List<Produkt> products = new List<Produkt>();
        public List<Produkt> Prods()
        {
            

            return products;
        }

        public void addProduct(int id, string name, int price, int amount)
        {
            products.Add(new Produkt() { id = id, name = name, price = price, amount = amount });
            
        }

        
    */
        public override string ToString()
        {
            return id +" "+ name + " " + price + " " + amount;
        }


    }
}
