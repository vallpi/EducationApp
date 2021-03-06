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
    /// Interaction logic for UsersProgress.xaml
    /// </summary>
    public partial class UsersProgress : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();
        public UsersProgress()
        {
            InitializeComponent();
            dataGrid_Progress.ItemsSource = _repo.GetScore();
        }
    }
}
