using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediastore
{
    //Gjorde den här klassen statiskt då infon annars försvann från listorna när man skulle läsa/skriva från fil
    //Det skulle gå att ändra på hur man kallar på klasserna för att göra den här icke statiskt och skapa instanser av den
    //men pga tidsbrist så har jag inte kunnat ändra på det då jag började men det här sättet.
    static class List
    {
        //Listorna som ska användas för att spara objekten för produkter respektive varukorg.
        static private List<Produkt> products = new List<Produkt>();
        static private List<Produkt> shoppingcart = new List<Produkt>();

        //Funktioner för produkt listan nedan.
        //Add funktion för att skapa nya objekt av produktklassen och sedan lässa till dem i listan.
        public static void add(int id, string name, int price, int amount, string supplier)
        {
            products.Add(new Produkt() { id = id, name = name, price = price, amount = amount, supplier = supplier });
        }

        //Kollar ifall en produkt med id nummer finns i listan redan, om inte så lägger den till produkten, annars returnerar false.
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
        //Tömmer produkt listan.
        public static void clear()
        {
            products.Clear();
        }
        //Tar bort en produkt från listan.
        public static void remove(Produkt id)
        {
            products.Remove(id);
        }
        //Returnerar listan.
        public static List<Produkt> getList()
        {
            return products;
        }

        //Funktioner för varukorgen nedan.
        //Returnerar varukorgen.
        public static List<Produkt> getShoppingCart()
        {
            return shoppingcart;
        }

        //Räknar ut total priset av varorna som ligger i varukorgen och returnerar det.
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

        //Skapar ett nytt objekt med antalet som läggs till i varukorget och kollar sedan ifall det finns nog många i lager för att göra det
        //Funkar det och det är ett positivt tall så läggs den till och antalet dras bort från produktlistan, annars returnerar vi falskt.
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

        //Tömmer varukorgen när vi har köpt.
        public static void buy()
        {
            shoppingcart.Clear();
        }

        //Om vi tömmer varukorgen så lägger vi tillbaka antalet av varorna till produktlistan.
        public static void shoppingClear()
        {
            foreach (Produkt item in shoppingcart)
            {
                products.FindAll(s => s.id.Equals(item.id)).ForEach(i => i.amount += item.amount);
            }
            shoppingcart.Clear();
        }

        //Tar vi bort en produkt så  lägger vi tillbaka antalet där också.
        public static void shoppingRemove(Produkt id)
        {
            //find all är inte nödvädnigt då det bara gäller en produkt. Men metod grupperna fungerar bra ihop och jag har 
            //problem med att hitta en bättre lösning på det att lägga tillbaka antalet som den här.
            products.FindAll(s => s.id.Equals(id.id)).ForEach(i => i.amount += id.amount);
            shoppingcart.Remove(id);
        }

        //Funktioner för grossistleveranser.
        public static Boolean leverans(string lev, int antal)
        {
            //Kollar ifall en leverantör redan finns registrerad.
            var match = products.FindAll(s => s.supplier.Equals(lev));
            //Om inte returnerar vi falskt.
            if (match.Count == 0)
            {
                return false;
            }
            else
            {
                //Annrs så ökar vi antalet som angets vid funktions anropen och ökar det för alla varor med samma leverantör.
                products.FindAll(s => s.supplier.Equals(lev)).ForEach(i => i.amount += antal);
                return true;
            }

        }
    }
}
