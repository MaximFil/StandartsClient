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
    public partial class User : Form
    {
        private readonly StandartService standartService;

        private Dictionary<string, List<FavoriteStandart>> FavoriteStandarts { get; set; }
        private LinkedList<DisplayStandartModel> DisplayStandartModel { get; set; }

        public User()
        {
            InitializeComponent();
            this.standartService = new StandartService();
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
            this.FavoriteStandarts = standartService.GetFavoriteStandarts(UserProfile.Id).GetAwaiter().GetResult().Values.GroupBy(s => s.StandartTypeName).ToDictionary(x =>x.Key, x => x.ToList());
        }

        private void User_Load(object sender, EventArgs e)
        {
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            foreach (var standart in FavoriteStandarts)
            {
                var addTypeName = true;
                foreach(var item in standart.Value)
                {
                    linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(new Standart { Id = item.StandartId, Details = item.StandartDetails, Header = item.StandartHeader}, panel1, true, linkedListNode?.Value, true, addTypeName, item.StandartTypeName));
                    addTypeName = false;
                }
            }
        }

        private void User_Resize(object sender, EventArgs e)
        {
            var linkedListNode = DisplayStandartModel?.First;
            while (linkedListNode != null)
            {
                var width = panel1.Width;
                var height = panel1.Height;
                linkedListNode?.Value?.ChangeFormSize(width, height, linkedListNode?.Previous?.Value);
                linkedListNode = linkedListNode?.Next;
            }
        }
    }
}
