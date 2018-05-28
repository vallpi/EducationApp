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
    /// Interaction logic for SubjectInfoAndTests.xaml
    /// </summary>
    public partial class SubjectInfoAndTests : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();
        public SubjectInfoAndTests()
        {
            InitializeComponent();
            Subject1.Text = _repo.GetSubject(1);
            Subject2.Text = _repo.GetSubject(2);
            Subject3.Text = _repo.GetSubject(3);
            ListBoxTopics.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink1_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(1);
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink2_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(2);
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink3_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(3);
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = _repo.ReturnSubjectTopics();
        }
    }
}
