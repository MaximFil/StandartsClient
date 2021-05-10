using StandartsClient.Services;
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
        private readonly StandartService standartService;

        private bool dispose = true;
        public Menu()
        {
            InitializeComponent();
            this.standartService = new StandartService();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dispose = false;
            var form = new StandartsForm(StandartTypesEnum.Bel);
            form.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dispose = false;
            var belForm = new StandartsForm(StandartTypesEnum.Rus);
            belForm.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dispose = false;
            var belForm = new StandartsForm(StandartTypesEnum.Inter);
            belForm.Show();
            this.Close();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dispose)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dispose = false;
            var user = new User();
            user.Show();
            this.Close();
        }
    }
}
