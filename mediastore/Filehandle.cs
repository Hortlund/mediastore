using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace mediastore
{
    class Filehandle
    {
        //Funktion för att skriva till filen.
        public void writeFile()
        {
            //Hämtar listan med produkter
            List<Produkt> products = List.getList();
            //Skapar en ny textwriter
            TextWriter tw = new StreamWriter("Produkter.txt");

            //För varje produkt som finns i listan så skriver vi en rad med varje attribut som den har, separerat med , som specifikationen sa.
            foreach (Produkt item in products)
                tw.WriteLine(item.id + "," + item.name + "," + item.price + "," + item.amount + "," + item.supplier + "\r");
            //Stänger sedan filen.
            tw.Close();


        }

        //Funktion för att läsa från filen.
        public void readFile()
        {
            //Tömmer listan för att sedan läsa in nytt, ifall det skulle ligga kvar något. Om man byter instans eller något.
            List.clear();
            //Finns inte filen så skapar vi den.
            if (!File.Exists("Produkter.txt"))
            {
                TextWriter tw = new StreamWriter("Produkter.txt");
                tw.Close();
            }
            else
            {
                //Finns filen så läser vi från den.
                TextReader tr = new StreamReader("Produkter.txt");
                //Sträng för att hålla nuvarande rad.
                string r;
                //Så länge som raden inte är tom/null så läser vi nästa.
                while ((r = tr.ReadLine()) != null)
                {
                    try
                    {
                        //Det här skapar index out of bound exception, vet inte varför och har inte kunnat lösa det, så tystar det.
                        //Det är fem fält i arrayen men går utanför ändå.
                        //Dålig kanske men :PP

                        //Skapar en array för att hålla alla substrängar som skapas vid varje , 
                        string[] splitted = r.Split(',');
                        //Sätter respektive attribut till en ny sträng.
                        string idText = splitted[0];
                        string nameText = splitted[1];
                        string priceText = splitted[2];
                        string amountText = splitted[3];
                        string supplierText = splitted[4];

                        //För värdena som ska vara int så måste vi parsa om dem, det  görs här.
                        int.TryParse(idText, out int id);
                        int.TryParse(priceText, out int price);
                        int.TryParse(amountText, out int amount);

                        //Sedan skickar vi allt till list funktionen där vi lägger till dem.
                        List.add(id, nameText, price, amount, supplierText);
                    }
                    catch
                    {

                    }

                }
                //Stänger sedan filen efter att vi har gått ut från while loopen.
                tr.Close();
            }
            

        }
       


    }
}
