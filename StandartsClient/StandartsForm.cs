using StandartsClient.Models;
using StandartsClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandartsClient
{
    public partial class StandartsForm : Form
    {
        private readonly StandartService standartService;
        private readonly UserService userService;

        private LinkedList<DisplayStandartModel> DisplayStandartModel { get; set; }

        private Dictionary<int, Standart> Standarts { get; set; }
        private Dictionary<int, FavoriteStandart> FavoriteStandarts { get; set; }

        public StandartsForm(StandartTypesEnum standartType)
        {
            InitializeComponent();
            this.standartService = new StandartService();
            if(StandartTypes.Items == null)
            {
                StandartTypes.Items = standartService.GetStandartTypes().GetAwaiter().GetResult();
            }
            var standartTypeModel = StandartTypes.Items[standartType.ToString()];
            this.Standarts = StandartsClient.Standarts.Items != null
                ? StandartsClient.Standarts.Items[standartTypeModel.Id].ToDictionary(x => x.Id) 
                : this.standartService.GetStandartsByTypeId(standartTypeModel.Id).GetAwaiter().GetResult();
            this.Text = standartTypeModel.StandartTypeName;
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
            this.FavoriteStandarts = standartService.GetFavoriteStandarts(UserProfile.Id).GetAwaiter().GetResult();
        }

        private void BelStandart_Load(object sender, EventArgs e)
        {
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            foreach(var standart in Standarts)
            {
                var cheked = this.FavoriteStandarts.ContainsKey(standart.Key);
                linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(standart.Value, panel1, cheked, linkedListNode?.Value));
            }
        }

        private void BelStandart_Resize(object sender, EventArgs e)
        {
            var linkedListNode = DisplayStandartModel?.First;
            while(linkedListNode != null)
            {
                var width = panel1.Width;
                var height = panel1.Height;
                linkedListNode?.Value?.ChangeFormSize(width, height, linkedListNode?.Previous?.Value);
                linkedListNode = linkedListNode?.Next;
            }
        }

        private void StandartsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
