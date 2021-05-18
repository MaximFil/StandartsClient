using StandartsClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StandartsClient.Services;

namespace StandartsClient
{
    public class DisplayStandartModel
    {
        public int? Id { get; set; }

        public Label UserName { get; set; }

        public Label FindText { get; set; }

        public TextBox FindTextBox { get; set; }

        public Button FindButton { get; set; }

        public Button ClearResultSearch { get; set; }

        public Button BackButton { get; set; }

        public Label EmptyResult { get; set; }

        public Label TypeName { get; set; }

        public Label Header { get; set; }

        public CheckBox AddToFavorite { get; set; }

        public Label Details { get; set; }

        public Standart Standart { get; set; }

        public Panel Panel { get; set; }

        public ISearch Search { get; set; }

        private bool IsAddedToFavorite { get; set; }
        private bool IsAddedTypeName { get; set; }
        private string typeName { get; set; }
        private string FindPattern { get; set; }
        private readonly UserService userService;
        private const int leftIndent = 30;
        private const int bottomIndent = 15;

        public DisplayStandartModel(Standart standart, Panel panel, bool isAddedToFavorite, DisplayStandartModel displayStandartModel = null, bool isUserForm = false, bool addTypeName = false, string typeName = "", string findPattern = "", ISearch search = null)
        {
            this.Standart = standart;
            this.Id = standart?.Id;
            this.userService = new UserService();
            this.IsAddedToFavorite = isAddedToFavorite;
            this.IsAddedTypeName = addTypeName;
            this.typeName = typeName;
            this.Panel = panel;
            this.Search = search;
            this.FindPattern = findPattern;
            DisplayStandartToForm(panel, displayStandartModel, isUserForm);
        }

        public void DisplayStandartToForm(Panel panel, DisplayStandartModel standartModel = null, bool isUserForm = false)
        {
            if (isUserForm)
            {
                InitializeLabelUserName(panel);
            }
            if (standartModel == null)
            {   
                InitializeButtonBack(panel);
                InitializeLabelFindText(panel);
                InitializeTextboxFindText(panel);
                InitializeFindButton(panel);
                InitializeClearResultSearchButton(panel);
            }
            if(Standart == null)
            {
                InitializeLabelEmptyResult(panel);
            }
            if(this.Standart != null)
            {
                InitializeLabelTypeName(panel, standartModel);
                InitializeLabelHeader(panel, standartModel);
                InitializeCheckBoxAddToFavorite(panel);
                InitializeLabelDetails(panel);
            }
        }

        private void InitializeLabelUserName(Panel panel)
        {
            UserName = new Label();
            UserName.Location = new System.Drawing.Point(10, 10);
            UserName.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            UserName.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            UserName.Name = "userNameLabel";
            UserName.AutoSize = true;
            UserName.Text = UserProfile.Login;
            UserName.TabIndex = 1;
            panel.Controls.Add(UserName);
        }

        private void InitializeButtonBack(Panel panel)
        {
            BackButton = new Button();
            BackButton.Location = new System.Drawing.Point(10, UserName != null ? UserName.Location.Y + UserName.Size.Height + 5 : 14);
            BackButton.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            BackButton.Name = "backButton";
            BackButton.Size = new System.Drawing.Size(100, 25);
            BackButton.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            BackButton.Text = "Назад";
            BackButton.TabIndex = 15;
            BackButton.Click += new EventHandler(this.Back_Click);
            panel.Controls.Add(BackButton);
        }

        private void InitializeLabelFindText(Panel panel)
        {
            FindText = new Label();
            FindText.Location = new System.Drawing.Point(10, BackButton.Location.Y + BackButton.Size.Height + 5);
            FindText.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            FindText.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FindText.Name = "findTextLabel";
            FindText.AutoSize = true;
            FindText.Text = "Поиск";
            FindText.TabIndex = 2;
            panel.Controls.Add(FindText);
        }

        private void InitializeTextboxFindText(Panel panel)
        {
            FindTextBox = new TextBox();
            FindTextBox.Location = new System.Drawing.Point(FindText.Location.X + FindText.Size.Width + 3, BackButton.Location.Y + BackButton.Size.Height + 5);
            FindTextBox.Name = "findTextBox";
            FindTextBox.Size = new System.Drawing.Size(100, 25);
            FindTextBox.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            FindTextBox.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FindTextBox.Text = this.FindPattern;
            FindTextBox.TabIndex = 3;
            panel.Controls.Add(FindTextBox);
        }

        private void InitializeFindButton(Panel panel)
        {
            FindButton = new Button();
            FindButton.Location = new System.Drawing.Point(FindTextBox.Location.X + FindTextBox.Size.Width + 3, BackButton.Location.Y + BackButton.Size.Height + 5);
            FindButton.Name = "findButton";
            FindButton.Size = new System.Drawing.Size(100, 25);
            FindButton.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            FindButton.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FindButton.Text = "Поиск";
            FindButton.TabIndex = 4;
            FindButton.Click += new EventHandler(this.Search_Click);
            panel.Controls.Add(FindButton);
        }

        private void InitializeClearResultSearchButton(Panel panel)
        {
            ClearResultSearch = new Button();
            ClearResultSearch.Location = new System.Drawing.Point(FindButton.Location.X + FindButton.Size.Width + 3, BackButton.Location.Y + BackButton.Size.Height + 5);
            ClearResultSearch.Name = "clearButton";
            ClearResultSearch.Size = new System.Drawing.Size(100, 25);
            ClearResultSearch.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            ClearResultSearch.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ClearResultSearch.Text = "\u2716";
            ClearResultSearch.TabIndex = 10;
            ClearResultSearch.Click += new EventHandler(this.ClearSearch_Click);
            panel.Controls.Add(ClearResultSearch);
        }

        private void InitializeLabelEmptyResult(Panel panel)
        {
            EmptyResult = new Label();
            EmptyResult.Location = new System.Drawing.Point(10, FindButton != null ? FindButton.Location.Y + FindButton.Size.Height + 5 : 40);
            EmptyResult.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            EmptyResult.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            EmptyResult.Name = "emptyResultLabel";
            EmptyResult.AutoSize = true;
            EmptyResult.Text = "Ничего не найдено!";
            EmptyResult.TabIndex = 11;
            panel.Controls.Add(EmptyResult);
        }

        private void InitializeLabelTypeName(Panel panel, DisplayStandartModel standartModel = null)
        {
            if (IsAddedTypeName)
            {
                var coordinateY = standartModel != null ? standartModel.Details.Location.Y + standartModel.Details.Height + 15 : FindTextBox.Location.Y + FindTextBox.Size.Height + 15;
                TypeName = new Label();
                TypeName.Location = new System.Drawing.Point(10, coordinateY);
                TypeName.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
                TypeName.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TypeName.Name = "typeNameLabel";
                TypeName.AutoSize = true;
                TypeName.Text = typeName;
                TypeName.TabIndex = 5;
                panel.Controls.Add(TypeName);
            }
        }

        private void InitializeLabelHeader(Panel panel, DisplayStandartModel standartModel = null)
        {
            Header = new Label();
            var coordinateY = 10;
            if (this.IsAddedTypeName)
            {
                coordinateY = TypeName.Location.Y + TypeName.Size.Height + 15;
            }
            else if(standartModel != null)
            {
                coordinateY = standartModel.Details.Location.Y + standartModel.Details.Size.Height + 15;
            }
            else if(FindTextBox != null)
            {
                coordinateY = FindTextBox.Location.Y + FindTextBox.Size.Height + 15;
            }
            Header.Location = new System.Drawing.Point(10, coordinateY);
            Header.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            Header.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Header.Name = "headerLabel";
            Header.AutoSize = true;
            Header.Text = Standart?.Header;
            Header.TabIndex = 6;
            panel.Controls.Add(Header);
        }

        private void InitializeCheckBoxAddToFavorite(Panel panel)
        {
            AddToFavorite = new CheckBox();
            AddToFavorite.AutoSize = true;
            AddToFavorite.Location = new System.Drawing.Point(12, Header.Location.Y + 5 + Header.Size.Height);
            AddToFavorite.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            AddToFavorite.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AddToFavorite.Name = "addToFavoriteCheckBox";
            AddToFavorite.UseVisualStyleBackColor = true;
            AddToFavorite.Text = "Избранное";
            AddToFavorite.Checked = IsAddedToFavorite;
            AddToFavorite.TabIndex = 7;
            AddToFavorite.Click += new EventHandler(this.AddStandartToFavorite);
            panel.Controls.Add(AddToFavorite);
        }

        private void InitializeLabelDetails(Panel panel)
        {
            Details = new Label();
            Details.Location = new System.Drawing.Point(10, AddToFavorite.Location.Y + 5 + AddToFavorite.Size.Height);
            Details.Name = "detailsLabel";
            Details.MaximumSize = new System.Drawing.Size(panel.Width - leftIndent, panel.Height - bottomIndent);
            Details.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Details.AutoSize = true;
            Details.Text = "Подробнее:\n" + Standart?.Details;
            Details.TabIndex = 8;
            panel.Controls.Add(Details);
        }

        public void ChangeFormSize(int panelWidth, int panelHeight, DisplayStandartModel standartModel = null)
        {
            ResizeLabelTypeName(panelWidth, panelHeight, standartModel);
            ResizeLabelHeader(panelWidth, panelWidth, standartModel);
            ResizeCheckBoxAddToFavorite(panelWidth, panelHeight);
            ResizeLabelDetails(panelWidth, panelHeight);
        }

        private void ResizeLabelTypeName(int panelWidth, int panelHeight, DisplayStandartModel standartModel = null)
        {
            if (TypeName != null)
            {
                var coordinateY = standartModel != null ? standartModel.Details.Location.Y + standartModel.Details.Height + 15 : FindTextBox.Location.Y + FindTextBox.Size.Height + 15;
                TypeName.MaximumSize = new System.Drawing.Size(panelWidth - leftIndent, panelHeight - bottomIndent);
                TypeName.Location = new System.Drawing.Point(10, coordinateY);
            }
        }

        private void ResizeLabelHeader(int panelWidth, int panelHeight, DisplayStandartModel standartModel = null)
        {
            var coordinateY = 10;
            if (this.IsAddedTypeName)
            {
                coordinateY = TypeName.Location.Y + TypeName.Size.Height + 15;
            }
            else if (standartModel != null)
            {
                coordinateY = standartModel.Details.Location.Y + standartModel.Details.Size.Height + 15;
            }
            else if (FindTextBox != null)
            {
                coordinateY = FindTextBox.Location.Y + FindTextBox.Size.Height + 15;
            }
            Header.MaximumSize = new System.Drawing.Size(panelWidth - leftIndent, panelHeight - bottomIndent);
            Header.Location = new System.Drawing.Point(10, coordinateY);
        }

        private void ResizeCheckBoxAddToFavorite(int panelWidth, int panelHeight)
        {
            AddToFavorite.MaximumSize = new System.Drawing.Size(panelWidth - leftIndent, panelHeight - bottomIndent);
            AddToFavorite.Location = new System.Drawing.Point(12, Header.Location.Y + 5 + Header.Size.Height);
        }

        private void ResizeLabelDetails(int panelWidth, int panelHeight)
        {
            Details.MaximumSize = new System.Drawing.Size(panelWidth - leftIndent, panelHeight - bottomIndent);
            Details.Location = new System.Drawing.Point(10, AddToFavorite.Location.Y + 5 + AddToFavorite.Size.Height);
        }

        private async void AddStandartToFavorite(object sender, EventArgs e)
        {
            if (AddToFavorite.Checked)
            {
                await userService.AddStandartToFavorites(UserProfile.Id, Id.Value);
            }
            else
            {
                await userService.DeleteStandartFromFavorites(UserProfile.Id, Id.Value);
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            this.Search.Search(FindTextBox.Text);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Search.Back();
        }

        private void ClearSearch_Click(object sender, EventArgs e)
        {
            this.Search.ClearResultSearch();
            if(FindTextBox != null)
            {
                FindTextBox.Text = string.Empty;
            }
        }
    }
}
