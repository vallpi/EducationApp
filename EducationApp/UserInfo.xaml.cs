﻿using App.Classes;
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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();

        public UserInfo()
        {
            InitializeComponent();
            textBlock_UserName.Text = _repo.GetUserData("FullName");
            textBlock_Email.Text = _repo.GetUserData("Email");
            textBlock_Login.Text = _repo.GetUserData("Login");
        }
    }
}
