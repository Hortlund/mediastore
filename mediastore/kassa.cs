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
            //menu form;
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
            foreach (Produkt aPart in products)
            {
                Console.WriteLine(aPart);
            }

        }
        private void updateShoppingCart(string amount)
        {
            List<Produkt> shoppingcart = List.getShoppingCart();

            listBox2.DataSource = null;
            listBox2.DataSource = shoppingcart;
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = amount;
            foreach (Produkt aPart in shoppingcart)
            {
                Console.WriteLine(aPart);
            }

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

        private void button4_Click(object sender, EventArgs e)
        {
            
            Produkt r = ((Produkt)listBox1.SelectedItem);
            List.addShoppingCart(r);
            updateShoppingCart(textBox1.Text);
        }
    }
}
