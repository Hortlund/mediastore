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
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        // Skapar 2 funktioner som låter mig skapa nya instanser av vy-klasserna lager och kassa.
        public void runLager()
        {
            Application.Run(new lager());
        }
        public void runKassa()
        {
            Application.Run(new kassa());
        }

        
        private void menu_Load(object sender, EventArgs e)
        {
            
        }

        /* Dessa funktioner körs när man klickar på någon av Lager eller Kassa knapparna
         * och kör då funktionerna tidigare nämnda, men de instanserna körs i en ny tråd vilket låter mig stänga huvudmenyn och
         * fortsätta programmet i de nya vyerna. På så vis kan bara 1 instans av vyerna vara igång samtidigt.
         * 
         * https://stackoverflow.com/questions/5201852/what-is-a-thread-really
         * https://stackoverflow.com/questions/8457906/opening-a-new-form-closing-the-old-one-c-sharp
         * https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread?view=netframework-4.8
         */
        private void lager_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runLager));
            t.Start();
            this.Close();
        }

        private void kassa_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(runKassa));
            t.Start();
            this.Close();

        }

        // Avslutar applikationen via arkiv menyn
        private void avslutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
