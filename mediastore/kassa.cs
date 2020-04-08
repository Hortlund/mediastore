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
        Filehandle filehandle = new Filehandle();
        public kassa()
        {
            InitializeComponent();
        }

        public static void runMenu()
        {
            Application.Run(new menu());
        }

        private void huvudmenyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runMenu));
            t.Start();
            this.Close();
        }

        private void kassa_Load(object sender, EventArgs e)
        {
            filehandle.readFile();
            updateListBox();
        }

        private void updateListBox()
        {
            List<Produkt> products = List.getList();

            listBox1.DataSource = null;
            listBox1.DataSource = products;
            listBox1.DisplayMember = "Name";

        }
        private void updateShoppingCart()
        {
            List<Produkt> shoppingcart = List.getShoppingCart();

            listBox2.DataSource = null;
            listBox2.DataSource = shoppingcart;
            listBox2.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void button4_Click(object sender, EventArgs e)
        {
            
            if(!int.TryParse(textBox1.Text, out int amount))
            {
                MessageBox.Show("Ange ett antal varor att lägga till", "Information", MessageBoxButtons.OK);
                textBox1.Text = "";
            }
            else
            {

                Produkt r = ((Produkt)listBox1.SelectedItem);
                if(List.addShoppingCart(r, amount))
                {
                    updateListBox();
                    updateShoppingCart();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Varukorgen är redan tom", "Information", MessageBoxButtons.OK);
            }
            else
            {
                List.shoppingClear();
                updateListBox();
                updateShoppingCart();
                label8.Text = "Total pris: ";
                try
                {
                    label14.Text = "Produktnamn: ";
                    label12.Text = "Pris: ";
                    label11.Text = "Antal: ";
                }
                catch
                {

                }
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Varukorgen är redan tom", "Information", MessageBoxButtons.OK);
            }else
            {
                Produkt r = ((Produkt)listBox2.SelectedItem);
                List.shoppingRemove(r);
                updateListBox();
                updateShoppingCart();
                label8.Text = "Total pris: ";
                try
                {
                    label14.Text = "Produktnamn: ";
                    label12.Text = "Pris: ";
                    label11.Text = "Antal: ";
                }
                catch
                {

                }
                updateListBox();
                updateShoppingCart();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Lägg till något att köpa!", "Information", MessageBoxButtons.OK);
            }
            else
            {
                List.buy();
                updateListBox();
                updateShoppingCart();
                label14.Text = "Produktnamn: ";
                label12.Text = "Pris: ";
                label11.Text = "Antal: ";
                label8.Text = "Total pris: ";
                filehandle.writeFile();
                MessageBox.Show("Köpet genomfört!", "Information", MessageBoxButtons.OK);

            }

        }
    }
}
