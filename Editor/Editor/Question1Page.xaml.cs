using App.Classes;
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

namespace Editor
{
    /// <summary>
    /// Логика взаимодействия для Question1Page.xaml
    /// </summary>
    public partial class Question1Page : Page
    {

        public Question1Page()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxQuestion.Text != null && TextBoxAnswer1.Text != null && TextBoxAnswer2.Text != null && TextBoxAnswer3.Text != null && TextBoxAnswer4.Text != null)
            {
                var newquestion = new QuestionModel1
                {
                    Question = TextBoxQuestion.Text,
                    Answer1 = TextBoxAnswer1.Text,
                    Answer2 = TextBoxAnswer2.Text,
                    Answer3 = TextBoxAnswer3.Text,
                    Answer4 = TextBoxAnswer4.Text,
                    TopicId = EditorClass.edcl.SelectedTopic.Id
                };
                if (RadioButton1.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer1;
                if (RadioButton2.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer2;
                if (RadioButton3.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer3;
                if (RadioButton4.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer4;
                EditorClass.edcl.AddQuestion1(newquestion);
                Clear();
            }
        }

        public void Clear()
        {
            TextBoxQuestion.Text = null;
            TextBoxAnswer1.Text = null;
            TextBoxAnswer2.Text = null;
            TextBoxAnswer3.Text = null;
            TextBoxAnswer4.Text = null;
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
            RadioButton3.IsChecked = false;
            RadioButton4.IsChecked = false;
        }
    }
}
