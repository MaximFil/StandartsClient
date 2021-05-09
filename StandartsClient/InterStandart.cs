﻿using StandartsClient.Models;
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
    public partial class InterStandart : Form
    {
        private readonly StandartService standartService;
        private readonly UserService userService;

        private LinkedList<DisplayStandartModel> DisplayStandartModel { get; set; }

        private Dictionary<int, Standart> Standarts { get; set; }

        public InterStandart(StandartTypesEnum standartType)
        {
            InitializeComponent();
            this.standartService = new StandartService();
            if (StandartTypes.Items == null)
            {
                StandartTypes.Items = standartService.GetStandartTypes().GetAwaiter().GetResult();
            }
            var standartTypeModel = StandartTypes.Items[standartType.ToString()];
            this.Standarts = StandartsClient.Standarts.Items != null
                ? StandartsClient.Standarts.Items[standartTypeModel.Id].ToDictionary(x => x.Id)
                : this.standartService.GetStandartsByTypeId(standartTypeModel.Id).GetAwaiter().GetResult();
            this.Text = standartTypeModel.StandartTypeName;
            this.DisplayStandartModel = new LinkedList<DisplayStandartModel>();
        }

        private void InterStandart_Load(object sender, EventArgs e)
        {
            LinkedListNode<DisplayStandartModel> linkedListNode = null;
            foreach (var standart in Standarts)
            {
                //linkedListNode = DisplayStandartModel.AddLast(new DisplayStandartModel(standart.Value, panel1, linkedListNode?.Value));
            }
        }

        private void InterStandart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
