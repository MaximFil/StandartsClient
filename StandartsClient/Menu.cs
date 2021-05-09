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
            var belForm = new BelStandart(1);
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

        private async void Menu_Load(object sender, EventArgs e)
        {
            StandartTypes.Items = await standartService.GetStandartTypes();
        }
    }
}
