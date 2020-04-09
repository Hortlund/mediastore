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
        //Ny instans av filhanteringen.
        Filehandle filehandle = new Filehandle();
        public lager()
        {
            InitializeComponent();
        }

        public void runMenu()
        {
            Application.Run(new menu());
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            //Läser in filen och upptaderar listboxen.
            filehandle.readFile();
            updateListBox();
        }

        //Samma procedure som i huvudmenyn.
        private void huvudmenyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runMenu));
            t.Start();
            this.Close();
        }

        //Sätter texten till det valda objektets information.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Genererar ett null reference exception. Efter som man inte valt något när den updateras.
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

        //Uppdaterar listboxen vid änringar.
        private void updateListBox()
        {
            List<Produkt> products = List.getList();

            listBox1.DataSource = null;
            listBox1.DataSource = products;
            listBox1.DisplayMember = "Name";
            
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        //En lång radda med kontroller som ser till att rätt information anges, om inte så informeras användaren om detta.
        //Lägger annars till varan och uppdaterar listboxen och skriver till fil.
        private void button3_Click(object sender, EventArgs e)
        {
            
            List<Produkt> products = List.getList();
            if(textBox1.Text == "")
            {
                MessageBox.Show("Ange ett produktnamn!", "Information", MessageBoxButtons.OK);
            }
            else if (!int.TryParse(textBox2.Text, out int id))
            {
                MessageBox.Show("Ange ett id-nummer i siffror", "Information", MessageBoxButtons.OK);
                if (id < 0)
                {
                    MessageBox.Show("Ange ett positivt tal", "Information", MessageBoxButtons.OK);
                }
            }
            else if (!int.TryParse(textBox3.Text, out int price) || price < 0)
            {
                MessageBox.Show("Ange ett pris i siffror", "Information", MessageBoxButtons.OK);
            }
            else if(!int.TryParse(textBox4.Text, out int amount) || amount < 0)
            {
                MessageBox.Show("Ange ett positivt antal i siffror", "Information", MessageBoxButtons.OK);
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Ange en leverantör", "Information", MessageBoxButtons.OK);
            }
            else
            {
                if(!(List.exists(id, textBox1.Text, price, amount, textBox5.Text)))
            {
                    MessageBox.Show("Varan med varunumret finns redan, ange ett nytt", "Information", MessageBoxButtons.OK);
                }
                updateListBox();
                filehandle.writeFile();
            }
        }

        //Funktion för att ta bort en produkt.
        private void button1_Click_1(object sender, EventArgs e)
        {
            //Skapar en referens till valda objektet och kollar så att lager status inte är noll, är det noll tas produkten bort direkt
            //Är den inte noll så frågas använderen om de verkligen vill ta bort varan.
            //Sedan så uppdateras listboxen och ändringarna skrivs till fil.
            Produkt r = ((Produkt)listBox1.SelectedItem);
            if(r == null)
            {

            }else
            {
                int amount = ((Produkt)listBox1.SelectedItem).amount;
                if (amount != 0)
                {
                    DialogResult result = MessageBox.Show("Vill du verkligen ta bort produkten?", "Bekräftelse", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        List.remove(r);
                    }
                    else if (result == DialogResult.No)
                    {

                    }
                    else if (result == DialogResult.Cancel)
                    {

                    }
                }
                else if (amount == 0)
                {
                    List.remove(r);
                }

                listBox1.DisplayMember = "Name";
                label1.Text = "Produktnamn: ";
                label2.Text = "Varunummer: ";
                label3.Text = "Pris: ";
                label4.Text = "Antal: ";
                label10.Text = "Leverantör: ";
                updateListBox();
                filehandle.writeFile();
            }
            
        }

        //Funktion för att lägga till levereans från grossist. Med tillhörande kontroller för input.
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox6.Text == "")
            {
                MessageBox.Show("Ange en leverantör", "Information", MessageBoxButtons.OK);
            }
            else if(!int.TryParse(textBox7.Text, out int antal))
            {
                MessageBox.Show("Ange ett antal varor som levererats", "Information", MessageBoxButtons.OK);
            }
            else
            {
                //Finns inte leverantören så får man en notis om det att lägga till en.
                if(!(List.leverans(textBox6.Text, antal))){
                    MessageBox.Show("Leverantören finns inte, skapa en produkt först och registrera en leverantör", "Information", MessageBoxButtons.OK);
                    textBox6.Text = "";
                    textBox7.Text = "";
                }
                else
                {
                    //Annars så läggs varorna till och antalet ökar på alla med samma leverantör.
                    updateListBox();
                    filehandle.writeFile();
                    textBox6.Text = "";
                    textBox7.Text = "";
                }
            }
            
        }
    }
}
