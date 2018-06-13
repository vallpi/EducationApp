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
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
            foreach (Subject s in _repo.GetSubjectList())
                AddSubject(s.Name);
        }

        private void AddSubject(string name)
        {
            /*Run run = new Run(name);
            Hyperlink hyper = new Hyperlink(run)
            {
                NavigateUri = new Uri("SubjectInfo.xaml", UriKind.Relative),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF8DC")),
                FontSize = 18,
            };
            hyper.RequestNavigate += Hyperlink1_Click; */

            TextBlock text = new TextBlock()
            {
                Text = name,
                FontSize = 18,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF8DC")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness { Left = 100, Right = 100 },
            };
            // text.Inlines.Add(hyper);
            var list = ListSubj.Items;
            list.Add(text);
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
            if (topic_name != null)
            {
                string getnq = _repo.GetNextQuestion();
                if (getnq != null)
                    this.NavigationService.Navigate(new System.Uri(getnq, UriKind.Relative));
                else
                {
                    var res = new Result();
                    res.Show();
                }

            }
            else
            {
                MessageBox.Show("Выберите тему");
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

        private void ListSubj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlock text = (TextBlock)ListSubj.SelectedItem;
            int id = _repo.GetSubjectList().Where(n => n.Name == text.Text).FirstOrDefault().Id;
            _repo.SelectSubject(id);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void ScrollToLeft_Click(object sender, RoutedEventArgs e)
        {
            scroll.PageLeft();
        }

        private void ScrollToRight_Click(object sender, RoutedEventArgs e)
        {
            scroll.PageRight();
        }
    }
}
