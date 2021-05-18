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
    public partial class User : Form, ISearch
    {
        private readonly StandartService standartService;

        private Dictionary<string, List<FavoriteStandart>> FavoriteStandarts { get; set; }
        private Dictionary<string, List<FavoriteStandart>> ResultSearchFavoriteStandarts { get; set; }
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
            timer1.Start();
            var isUserForm = true;
            panel1?.Controls.Clear();
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
            if (FavoriteStandarts != null && FavoriteStandarts.Any())
            {
                foreach (var standart in FavoriteStandarts)
                {
                    var addTypeName = true;
                    foreach(var item in standart.Value)
                    {
                        linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(new Standart { Id = item.StandartId, Details = item.StandartDetails, Header = item.StandartHeader}, panel1, true, linkedListNode?.Value, isUserForm: isUserForm, addTypeName, item.StandartTypeName, search: this));
                        addTypeName = false;
                        isUserForm = false;
                    }
                }
            }
            else
            {
                new DisplayStandartModel(null, panel1, false, isUserForm: true, search: this);
            }
        }

        public void Search(string findText)
        {
            panel1.Controls.Clear();
            var isUserForm = true;
            this.ResultSearchFavoriteStandarts = FavoriteStandarts.SelectMany(s => s.Value).Where(s => s.StandartHeader.IndexOf(findText, StringComparison.InvariantCultureIgnoreCase) >= 0).GroupBy(s => s.StandartTypeName).ToDictionary(s => s.Key, s => s.ToList());
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
            if (ResultSearchFavoriteStandarts != null && ResultSearchFavoriteStandarts.Any())
            {
                foreach (var standart in ResultSearchFavoriteStandarts)
                {
                    var addTypeName = true;
                    foreach (var item in standart.Value)
                    {
                        linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(new Standart { Id = item.StandartId, Details = item.StandartDetails, Header = item.StandartHeader }, panel1, true, linkedListNode?.Value, isUserForm: isUserForm, addTypeName, item.StandartTypeName, search: this, findPattern: findText));
                        addTypeName = false;
                        isUserForm = false;
                    }
                }
            }
            else
            {
                new DisplayStandartModel(null, panel1, false, isUserForm: true, search: this, findPattern: findText);
            }
        }

        public void Back()
        {
            this.Close();
        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            var menu = new Menu();
            menu.Show();
        }

        public void ClearResultSearch()
        {
            panel1?.Controls.Clear();
            var isUserForm = true;
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
            if (FavoriteStandarts != null && FavoriteStandarts.Any())
            {
                foreach (var standart in FavoriteStandarts)
                {
                    var addTypeName = true;
                    foreach (var item in standart.Value)
                    {
                        linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(new Standart { Id = item.StandartId, Details = item.StandartDetails, Header = item.StandartHeader }, panel1, true, linkedListNode?.Value, isUserForm: isUserForm, addTypeName, item.StandartTypeName, search: this));
                        addTypeName = false;
                        isUserForm = false;
                    }
                }
            }
            else
            {
                new DisplayStandartModel(null, panel1, false, isUserForm: true, search: this);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
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
