using StandartsClient.Models;
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
    public partial class BelStandart : Form
    {
        private readonly StandartService standartService;

        private Dictionary<int, Standart> Standarts { get; set; }

        public BelStandart(int standartId)
        {
            InitializeComponent();
            this.standartService = new StandartService();
            this.Standarts = StandartsClient.Standarts.Items != null 
                ? StandartsClient.Standarts.Items[standartId].ToDictionary(x => x.Id) 
                : this.standartService.GetStandartsByTypeId(standartId).GetAwaiter().GetResult();
        }

        private void BelStandart_Load(object sender, EventArgs e)
        {
            var standart = Standarts.First().Value;
            var displayModel = new DisplayStandartModel() { Standart = standart };
            displayModel.DisplayStandartToForm(this);
        }
    }
}
