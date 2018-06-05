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
    /// Interaction logic for SubjectInfo.xaml
    /// </summary>
    public partial class SubjectInfo : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();

        public SubjectInfo()
        {
            InitializeComponent();
            textBlock_Subject1.Text = _repo.GetSubject(1);
            textBlock_Subject2.Text = _repo.GetSubject(2);
            textBlock_Subject3.Text = _repo.GetSubject(3);
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
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

        private void ButtonStartTest_Click(object sender, RoutedEventArgs e)
        {
            var topic_name = (string)listBox_Themes.SelectedItem;
            if (_repo.GetQuestionType() == 1)
                this.NavigationService.Navigate(new System.Uri("TestQuestion1.xaml", UriKind.Relative));
            else
            {
                if (_repo.GetQuestionType() == 2)
                {
                    this.NavigationService.Navigate(new System.Uri("TestQuestion2.xaml", UriKind.Relative));
                }
            }
            if (_repo.GetQuestionType() == 0)
            {
                this.NavigationService.Navigate(new System.Uri("UserAndProgress.xaml", UriKind.Relative));
            }
        }

        private void listBox_Themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBlock_ThemeTheory.Text = null;
            var topic_name = (string)listBox_Themes.SelectedItem;
            if (topic_name != null)
            {
                var theory = _repo.GetTopicTheory(topic_name);
                for (int i = 0; i < theory.Count; i++)
                    textBlock_ThemeTheory.Text += theory[i] + Environment.NewLine;
            }
        }
    }
}
