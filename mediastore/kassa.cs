using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mediastore
{
    public partial class kassa : Form
    {
        //Skapar en ny instans av filhanteraren
        Filehandle filehandle = new Filehandle();
        public kassa()
        {
            InitializeComponent();
        }

        public static void runMenu()
        {
            Application.Run(new menu());
        }

        //Detta görs på samma sätt som förklarat i menu.cs klassen.
        private void huvudmenyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runMenu));
            t.Start();
            this.Close();
        }

        //När klasses laddas så läser vi även in all data från fil till produkt listan så att den finns tillgänglig för programmet.
        private void kassa_Load(object sender, EventArgs e)
        {
            filehandle.readFile();
            updateListBox();
        }

        // Funktion för att refresha listboxen efter att ändringar har skett.
        private void updateListBox()
        {
            List<Produkt> products = List.getList();

            listBox1.DataSource = null;
            listBox1.DataSource = products;
            listBox1.DisplayMember = "Name";

        }
        //Funktion för att uppdattera varukorgen efter att ändringar har skett.
        private void updateShoppingCart()
        {
            List<Produkt> shoppingcart = List.getShoppingCart();

            listBox2.DataSource = null;
            listBox2.DataSource = shoppingcart;
            listBox2.DisplayMember = "Name";
        }

        //Funktion för att sätta labeltexterna till värdet för respektive produkt man har valt.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Den kastar ett null reference excpetion här, för att man inte har valt något direkt när man uppdaterar den.
            try
            {
                label6.Text = "Produktnamn: " + ((Produkt)listBox1.SelectedItem).name;
                label5.Text = "Varunummer: " + ((Produkt)listBox1.SelectedItem).id;
                label3.Text = "Pris: " + ((Produkt)listBox1.SelectedItem).price;
                label4.Text = "Antal: " + ((Produkt)listBox1.SelectedItem).amount;
                label10.Text = "Leverantör: " + ((Produkt)listBox1.SelectedItem).supplier;
            }
            catch
            {

            }

        }

        // Samma som ovan fast med varukorgen.
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label14.Text = "Produktnamn: " + ((Produkt)listBox2.SelectedItem).name;
                label12.Text = "Pris: " + ((Produkt)listBox2.SelectedItem).price;
                label11.Text = "Antal: " + ((Produkt)listBox2.SelectedItem).amount;
            }
            catch
            {

            }

        }

        //Funktion för att lägga till varor till varukorgen.
        private void button4_Click(object sender, EventArgs e)
        {
            //Kollar ifall det är siffror som anges.
            if(!int.TryParse(textBox1.Text, out int amount))
            {
                MessageBox.Show("Ange ett antal varor att lägga till", "Information", MessageBoxButtons.OK);
                textBox1.Text = "";
            }
            else
            {
                //Gör en referens till det valda objektet och skickar med det till funktionen i listan.
                Produkt r = ((Produkt)listBox1.SelectedItem);
                if(List.addShoppingCart(r, amount))
                {
                    updateListBox();
                    updateShoppingCart();
                    //Sätter total priset till summan av alla varor i varukorgen.
                    label8.Text = "Total pris: " + List.totalPrice().ToString();
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Välj ett annat antal", "Information", MessageBoxButtons.OK);
                    textBox1.Text = "";
                }
                    
            }
        }

        //Funktion för att tömma hela varukorgen.
        private void button3_Click(object sender, EventArgs e)
        {
            //Ifall den redan är tom så skicka en notis.
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Varukorgen är redan tom", "Information", MessageBoxButtons.OK);
            }
            else
            {
                //Tömmer listan och uppdaterar listboxar och text.
                List.shoppingClear();
                updateListBox();
                updateShoppingCart();
                label8.Text = "Total pris: ";
                label14.Text = "Produktnamn: ";
                label12.Text = "Pris: ";
                label11.Text = "Antal: ";
            }
                
        }

        //Funktion för att ta bort det valda objektet från varukorgen.
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Varukorgen är redan tom", "Information", MessageBoxButtons.OK);
            }else
            {
                //Skapar en objekt referens till valda objektet och skickar det till list funktionen för att ta bort det.
                Produkt r = ((Produkt)listBox2.SelectedItem);
                List.shoppingRemove(r);
                updateListBox();
                updateShoppingCart();
                label8.Text = "Total pris: ";
                label14.Text = "Produktnamn: ";
                label12.Text = "Pris: ";
                label11.Text = "Antal: ";
            }
            
        }

        //Funktion för att köpa vara.
        private void button1_Click(object sender, EventArgs e)
        {
            //Ifall varukorgen är tom så igen ge en notis om detta.
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Lägg till något att köpa!", "Information", MessageBoxButtons.OK);
            }
            else
            {
                //Gå till funktionen som hanterar reducering av lager.
                List.buy();
                //Uppdatera listor och text så det syns att varorna har dragits borts.
                updateListBox();
                updateShoppingCart();
                label14.Text = "Produktnamn: ";
                label12.Text = "Pris: ";
                label11.Text = "Antal: ";
                label8.Text = "Total pris: ";
                //Spara det nya värderna till filen så det laddas till nästa gång filhanteraren läser listorna.
                filehandle.writeFile();
                //Visa meddelande om att köpet har gått igenom.
                MessageBox.Show("Köpet genomfört!", "Information", MessageBoxButtons.OK);

            }

        }
    }
}
