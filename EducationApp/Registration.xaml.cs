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
            if (string.IsNullOrWhiteSpace(TextBoxFullName.Text))
            {
                MessageBox.Show("Введите имя");
                TextBoxFullName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxEmail.Text))
            {
                MessageBox.Show("Введите Email");
                TextBoxEmail.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordBox.Password.ToString()))
            {
                MessageBox.Show("Введите пароль");
                PasswordBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин");
                TextBoxLogin.Focus();
                return;
            }
            if (!_repo.Registration(TextBoxFullName.Text, TextBoxEmail.Text, TextBoxLogin.Text, PasswordBox.Password.ToString()))
            { MessageBox.Show("Данный Email уже зарегистрирован"); }
        }
    }
}
