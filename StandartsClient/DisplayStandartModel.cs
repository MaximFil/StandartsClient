using StandartsClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandartsClient
{
    public class DisplayStandartModel
    {
        public int Number { get; set; }

        public Label Header { get; set; }

        public CheckBox AddToFavorite { get; set; }

        public Label DisplayDetails { get; set; }

        public Label Details { get; set; }

        public Standart Standart { get; set; }

        public void DisplayStandartToForm(Form form)
        {
            InitializeLabelHeader();
            InitializeCheckBoxAddToFavorite();
            InitializeLabelDisplayDetails();
            InitializeLabelDetails();
            form.Controls.AddRange(new Control[] { Header, AddToFavorite, DisplayDetails, Details });
        }

        public void ChangeFormSize(Form form)
        {

        }
        
        public void InitializeLabelHeader()
        {
            Header = new Label();
            Header.Location = new System.Drawing.Point(10, 10);
            Header.Name = "headerLabel";
            Header.AutoSize = true;
            Header.TabIndex = 1;
            Header.Text = Standart.Header;
        }

        public void InitializeCheckBoxAddToFavorite()
        {
            AddToFavorite = new CheckBox();
            AddToFavorite.AutoSize = true;
            AddToFavorite.Location = new System.Drawing.Point(10, 40);
            AddToFavorite.Name = "addToFavoriteCheckBox";
            AddToFavorite.UseVisualStyleBackColor = true;
            AddToFavorite.Text = "Избранное";
        }

        public void InitializeLabelDisplayDetails()
        {
            DisplayDetails = new Label();
            DisplayDetails.Location = new System.Drawing.Point(10, 60);
            DisplayDetails.Name = "displayDetailsLabel";
            DisplayDetails.AutoSize = true;
            DisplayDetails.Text = "\u25B6 Подробнее";
        }

        public void InitializeLabelDetails()
        {
            Details = new Label();
            Details.Location = new System.Drawing.Point(10, 80);
            Details.Name = "detailsLabel";
            Details.AutoSize = true;
            Details.Text = Standart.Details;
        }        
    }
}
