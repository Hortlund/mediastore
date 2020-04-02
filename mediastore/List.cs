using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediastore
{
    class List
    {
        static List<Produkt> products = new List<Produkt>();
        static List<Produkt> shoppingcart = new List<Produkt>();

        public static void add(int id, string name, int price, int amount, string supplier)
        {
            products.Add(new Produkt() { id = id, name = name, price = price, amount = amount, supplier = supplier });

        }

        public static void addShoppingCart(Produkt id)
        {
            shoppingcart.Add(id);
        }

        public static void clear()
        {
            products.Clear();
        }

        public static void remove(Produkt id)
        {
            products.Remove(id);
        }

        public static void leverans(string lev, int antal)
        {
            // https://stackoverflow.com/questions/12986776/change-some-value-inside-the-listt
            products.FindAll(s => s.supplier.Equals(lev)).ForEach(i => i.amount += antal);
            

        }

        public static List<Produkt> getList(){
            return products;
        }
        public static List<Produkt> getShoppingCart()
        {
            return shoppingcart;
        }


    }
}
