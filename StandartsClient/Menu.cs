using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandartsClient
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var belForm = new BelStandart();
            belForm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rusForm = new RusStandart();
            rusForm.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var interForm = new InterStandart();
            interForm.Show();
            this.Close();
        }
    }
}
