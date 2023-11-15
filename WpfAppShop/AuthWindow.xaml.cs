using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using WpfAppShop.Models;

namespace WpfAppShop
{
    public partial class AuthWindow: Window
    {
        bool isNeedCaptcha = false;
        string captchaText;
        List<TextBlock> captchaBlocs;
        
        public AuthWindow()
        {
            InitializeComponent();

            captchaBlocs = new List<TextBlock> { TBlockCanvasFirst, TBlockCanvasSecond, TBlockCanvasThird, TBlockCanvasFour};
        }

        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text;
            string password = TBoxPassword.Text;

            if (isNeedCaptcha && GridCaptcha.Visibility == Visibility.Collapsed)
            {
                captchaText = GenerateCaptcha(4);


                for (int i = 0; i < captchaBlocs.Count; i++)
                    captchaBlocs[i].Text = captchaText[i].ToString();

                GridCaptcha.Visibility = Visibility.Visible;

                return;
            }

            if (isNeedCaptcha && GridCaptcha.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Введите капчу");

                return;
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Введите логин");

                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль");

                return;
            }

            User? user = App.dbContext.Users.FirstOrDefault(x => x.Login.Equals(login));

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                isNeedCaptcha = true;

                return;
            }

            if (!user.Password.Equals(password))
            {
                MessageBox.Show("Неверный пароль");
                isNeedCaptcha = true;

                return;
            }

            App.currentUser = user;

            TBoxCaptcha.Clear();
            TBoxLogin.Clear();
            TBoxPassword.Clear();

            MainWindow mWindow = new MainWindow(this);

            mWindow.Show();

            Hide();
        }

        private void ButtonCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxCaptcha.Text.Equals(captchaText))
            {
                GridCaptcha.Visibility = Visibility.Collapsed;

                isNeedCaptcha = false;
            }
            else
            {
                MessageBox.Show("Введите капчу еще раз");

                captchaText = GenerateCaptcha(4);

                for (int i = 0; i < captchaBlocs.Count; i++)
                    captchaBlocs[i].Text = captchaText[i].ToString();
            }

            TBoxCaptcha.Text = "";
        }

        string GenerateCaptcha(int length)
        {
            Random rnd = new Random();
            string result = "";

            List<string> nums = new List<string>() { "0", "1", "2", "3", "4", "5", "7", "8", };
            List<string> alphabet = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "t" };

            for (int i = 0;i < length; i++)
            {
                if (rnd.Next(0,2) == 1)
                    result += nums[rnd.Next(0, nums.Count())];
                else
                    result += alphabet[rnd.Next(0, alphabet.Count())];
            }

            return result;
        }
    }
}
