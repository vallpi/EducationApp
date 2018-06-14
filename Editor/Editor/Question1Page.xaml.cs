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
        EditorClass edcl = EditorClass.GetEditorClass();

        public Question1Page()
        {
            InitializeComponent();
            edcl.AddClick += ButtonAdd_Click;
        }

        private void ButtonAdd_Click()
        {
            if (!String.IsNullOrWhiteSpace(TextBoxQuestion.Text) &&
                !String.IsNullOrWhiteSpace(TextBoxAnswer1.Text) &&
                !String.IsNullOrWhiteSpace(TextBoxAnswer2.Text) &&
                !String.IsNullOrWhiteSpace(TextBoxAnswer3.Text) &&
                !String.IsNullOrWhiteSpace(TextBoxAnswer4.Text) &&
                !String.IsNullOrWhiteSpace(TextBoxQuestionNumber.Text))
            {
                var newquestion = new QuestionModel1
                {
                    Question = TextBoxQuestion.Text,
                    QuestionNumber = int.Parse(TextBoxQuestionNumber.Text),
                    Answer1 = TextBoxAnswer1.Text,
                    Answer2 = TextBoxAnswer2.Text,
                    Answer3 = TextBoxAnswer3.Text,
                    Answer4 = TextBoxAnswer4.Text,
                    TopicId = edcl.SelectedTopic.Id
                };
                if (RadioButton1.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer1;
                if (RadioButton2.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer2;
                if (RadioButton3.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer3;
                if (RadioButton4.IsChecked == true) newquestion.CorrectAnswer = newquestion.Answer4;
                edcl.AddQuestion1(newquestion);
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
