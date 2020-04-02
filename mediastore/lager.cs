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
    public partial class lager : Form
    {

       
        Filehandle filehandle = new Filehandle();
        public lager()
        {
            InitializeComponent();
        }

        public static void runMenu()
        {
            //menu form;
            Application.Run(new menu());
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            filehandle.readFile();
            updateListBox();
        }

        private void huvudmenyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runMenu));
            t.Start();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Produktnamn: " + ((Produkt)listBox1.SelectedItem).name;
                label2.Text = "Varunummer: " + ((Produkt)listBox1.SelectedItem).id;
                label3.Text = "Pris: " + ((Produkt)listBox1.SelectedItem).price;
                label4.Text = "Antal: " + ((Produkt)listBox1.SelectedItem).amount;
                label10.Text = "Leverantör: " + ((Produkt)listBox1.SelectedItem).supplier;
            }
            catch
            {

            }
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

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out int id);
            int.TryParse(textBox3.Text, out int price);
            int.TryParse(textBox4.Text, out int amount);
            List.add(id, textBox1.Text, price, amount, textBox5.Text);
            updateListBox();
            filehandle.writeFile();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                //List<Produkt> products = List.getList();
                //listBox1.DataSource = null;
                Produkt r = ((Produkt)listBox1.SelectedItem);
                List.remove(r);
                //listBox1.DataSource = products;
                listBox1.DisplayMember = "Name";
                label1.Text = "Produktnamn: ";
                label2.Text = "Varunummer: ";
                label3.Text = "Pris: ";
                label4.Text = "Antal: ";
                label10.Text = "Leverantör: ";
                updateListBox();
                filehandle.writeFile();

            }
            catch
            {

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            int.TryParse(textBox7.Text, out int antal);
            List.leverans(textBox6.Text, antal);
            updateListBox();
            filehandle.writeFile();
            textBox6.Text = "";
            textBox7.Text = "";


        }
    }
}
