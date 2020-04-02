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
        
        public void writeFile()
        {
            List<Produkt> products = List.getList();
            TextWriter tw = new StreamWriter("SavedList.txt");

            foreach (Produkt item in products)
                tw.WriteLine(item.id + "," + item.name + "," + item.price + "," + item.amount + "," + item.supplier + "\r");

            tw.Close();


        }

        public void readFile()
        {
            //Idea from stackoverflow link to issue.
            List.clear();
            TextReader tr = new StreamReader("SavedList.txt");
            string r;
                while ((r = tr.ReadLine()) != null)
                {
                try
                {
                    //This causes a indexoutofboundexception, but i cant seem to fix it, and it doesnt iterfeer, so supress it.
                    //Shouldnt do it, doesnt know why it does.
                    string[] splitted = r.Split(',');
                    string idText = splitted[0];
                    string nameText = splitted[1];
                    string priceText = splitted[2];
                    string amountText = splitted[3];
                    string supplierText = splitted[4];
                

                    int.TryParse(idText, out int id);
                    int.TryParse(priceText, out int price);
                    int.TryParse(amountText, out int amount);

                    List.add(id, nameText, price, amount, supplierText);
                    


                    Console.WriteLine(r);
                }
                catch
                {

                }
                
            }
            tr.Close();

        }
       


    }
}
