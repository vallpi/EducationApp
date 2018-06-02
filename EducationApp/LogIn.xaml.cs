using App.Classes;
using App.Classes.Interface;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducationApp
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();
        public LogIn()
        {
            InitializeComponent();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Login.Text))
            {
                MessageBox.Show("Введите логин");
                textBox_Login.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordBox_Password.Password.ToString()))
            {
                MessageBox.Show("Введите пароль");
                passwordBox_Password.Focus();
                return;
            }
            // Авторизация
            if (_repo.Authorization(textBox_Login.Text, passwordBox_Password.Password.ToString()))
            {
                AppWindow app = new AppWindow();
                app.ShowDialog();
            }
            else
                MessageBox.Show("Неверный Email или пароль");
        }
    }
}
