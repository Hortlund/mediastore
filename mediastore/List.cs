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

        //https://stackoverflow.com/questions/4651285/checking-if-a-list-of-objects-contains-a-property-with-a-specific-value
        public static void add(int id, string name, int price, int amount, string supplier)
        {

            products.Add(new Produkt() { id = id, name = name, price = price, amount = amount, supplier = supplier });
        }

        public static Boolean exists(int id, string name, int price, int amount, string supplier)
        {
            var match = products.Find(s => s.id.Equals(id));
            if (match == null)
            {
                add(id, name, price, amount, supplier);
                return true;
            }

            return false;
        }

        public static int totalPrice()
        {
            int totalPrice = 0, itemPrice = 0;
            foreach (Produkt item in shoppingcart)
            {
                itemPrice += item.price * item.amount;
                totalPrice = itemPrice;

            }
            Console.WriteLine(totalPrice);
            return totalPrice;
        }


        public static Boolean addShoppingCart(Produkt id, int antal)
        {
            Produkt buyP = new Produkt() { id = id.id, name = id.name, price = id.price, amount = antal, supplier = id.supplier };
            if (buyP.amount > id.amount || antal <= 0)
            {
                return false;
            }
            products.FindAll(s => s.id.Equals(id.id)).ForEach(i => i.amount -= antal);
            shoppingcart.Add(buyP);
            return true;
        }



        public static void clear()
        {
            products.Clear();
        }

        public static void buy()
        {
            shoppingcart.Clear();
        }

        public static void shoppingClear()
        {
            foreach(Produkt item in shoppingcart)
            {
                products.FindAll(s => s.id.Equals(item.id)).ForEach(i => i.amount += item.amount);
            }
            shoppingcart.Clear();
        }

        public static void remove(Produkt id)
        {
                products.Remove(id);
        }
        public static void shoppingRemove(Produkt id)
        {
            products.FindAll(s => s.id.Equals(id.id)).ForEach(i => i.amount += id.amount);
            shoppingcart.Remove(id);
        }

        public static Boolean leverans(string lev, int antal)
        {
            var match = products.FindAll(s => s.supplier.Equals(lev));
            // https://stackoverflow.com/questions/12986776/change-some-value-inside-the-listt
            
            if(match.Count == 0)
            {
                return false;
            }
            else
            {
                products.FindAll(s => s.supplier.Equals(lev)).ForEach(i => i.amount += antal);
                return true;
            }

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
