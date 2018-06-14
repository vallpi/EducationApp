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
            var question = _repo.ReturnQuestionModel2();
            if (question != null)
            {
                textBlock_Question.Text = question.Question;
            }
            else
            {
                UserAndProgress userAndProgress = new UserAndProgress();
                NavigationService.Navigate(userAndProgress);
            }
            TextBlockSubject.Text = _repo.GetSelectedSubjectString();
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink1_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(1);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink2_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(2);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink3_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(3);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void ButtonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            _repo.CheckAnswer(textBox_Answer.Text, 2);
            if (_repo.GetNextQuestion() == "TestQuestion2.xaml")
            {
                var question = _repo.ReturnQuestionModel2();
                if (question != null)
                {
                    textBlock_Question.Text = question.Question;
                }
            }
            else
            {
                string getnq = _repo.GetNextQuestion();
                if (getnq != null)
                    this.NavigationService.Navigate(new System.Uri(getnq, UriKind.Relative));
                else
                {
                    var res = new Result();
                    res.ShowActivated = false;
                    res.Show();
                    this.NavigationService.Navigate(new Uri("SubjectInfo.xaml", UriKind.Relative));
                }
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
        }
    }
}
