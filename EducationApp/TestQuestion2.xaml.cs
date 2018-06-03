using System;
using App.Classes.Interface;
using App.Classes;
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
    /// Interaction logic for TestQuestion2.xaml
    /// </summary>
    public partial class TestQuestion2 : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();

        public TestQuestion2()
        {
            InitializeComponent();
        }

        private void Hyperlink1_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(1);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink2_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(2);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink3_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(3);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void ButtonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new System.Uri("TestQuestion1.xaml", UriKind.Relative));
        }
    }
}
