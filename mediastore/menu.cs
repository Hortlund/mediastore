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

        public static void runLager()
        {
            //lager form;
            Application.Run(new lager());
        }
        public static void runKassa()
        {
            //kassa form;
            Application.Run(new kassa());
        }

        
        private void menu_Load(object sender, EventArgs e)
        {
            
            
        }


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
    }
}
