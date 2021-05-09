using StandartsClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using StandartsClient.Models;

namespace StandartsClient
{
    public partial class Auth : Form
    {
        private readonly StandartService standartService;
        private readonly UserService userService;

        private const string loginLabelText = "У меня уже есть аккаунт";
        private const string signupLabelText = "Создать новый аккаунт";

        private bool isLogin = true;
        private HashSet<string> usedUserNames;
        public Auth()
        {
            InitializeComponent();
            this.standartService = new StandartService();
            this.userService = new UserService();
            this.usedUserNames = new HashSet<string>();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                if (usedUserNames.Contains(textBox1.Text))
                {
                    label4.Visible = true;
                }
                else
                {
                    label4.Visible = false;
                }
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Standarts.Items = await standartService.GetStandarts();
            StandartTypes.Items = await standartService.GetStandartTypes();
            this.usedUserNames = await userService.GetUsedUserNames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var login = textBox1.Text;
                var password = textBox2.Text;
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Логин или пароль должен содержать валидные символы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var loginRegex = new Regex(@"[^\u0021-\u007E]+");
                var loginAdditionalRegex = new Regex("\"|\'");
                if (loginRegex.IsMatch(login) || loginAdditionalRegex.IsMatch(login))
                {
                    MessageBox.Show("Пожалуйста введите корректный логин, который соответствует правилам:" +
                        "\n-можно использовать латинские символы;" +
                        "\n-можно использовать цифры;" +
                        "\n-можно использовать спецсимволы за исключением \' и \".",
                        "Валидация логина",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var passwordRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_.]).{8,}$");
                if (!passwordRegex.IsMatch(password))
                {
                    MessageBox.Show("Пожалуйста введите корректный пароль, который соответствует правилам:" +
                        "\n-по крайней мере одна строчная английская буква;" +
                        "\n-по крайней мере одна заглавная английская буква;" +
                        "\n-по крайней мере одна цифра;" +
                        "\n-по крайней мере один спец символ;" +
                        "\n-минимум 8 символов в длину.",
                        "Валидация пароля",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var user = new Models.User();
                if (isLogin)
                {
                    user = userService.LogIn(login, password);
                }
                else
                {
                    user = userService.SignUp(login, password);
                }

                UserProfile.Id = user.Id;
                UserProfile.Login = user.Login;

                Menu menu = new Menu();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                isLogin = false;
                label3.Text = loginLabelText;
                button1.Text = "Зарегестрироваться";
            }
            else
            {
                isLogin = true;
                label3.Text = signupLabelText;
                button1.Text = "Войти";
            }
        }
    }
}
