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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            // Проверка заполненности боксов и регистрация пользователя
            if (string.IsNullOrWhiteSpace(textBox_FullName.Text))
            {
                MessageBox.Show("Введите имя");
                textBox_FullName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox_Email.Text))
            {
                MessageBox.Show("Введите Email");
                textBox_Email.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordBox_Password.Password.ToString()))
            {
                MessageBox.Show("Введите пароль");
                passwordBox_Password.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox_Login.Text))
            {
                MessageBox.Show("Введите логин");
                textBox_Login.Focus();
                return;
            }
            if (!_repo.Registration(textBox_FullName.Text, textBox_Email.Text, textBox_Login.Text, passwordBox_Password.Password.ToString()))
            { MessageBox.Show("Данный Email уже зарегистрирован"); }
            AppWindow app = new AppWindow();
            app.ShowDialog();
        }
    }
}
