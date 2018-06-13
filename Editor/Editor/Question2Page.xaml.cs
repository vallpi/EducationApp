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
    /// Логика взаимодействия для Question2Page.xaml
    /// </summary>
    public partial class Question2Page : Page
    {
        EditorClass edcl = EditorClass.GetEditorClass();

        public Question2Page()
        {
            InitializeComponent();
            edcl.AddClick += ButtonAdd_Click;
        }

        private void ButtonAdd_Click()
        {
            if (TextBoxQuestion.Text != null && TextBoxAnswer.Text != null && TextBoxQuestionNumber.Text != null)
            {
                var newquestion = new QuestionModel2
                {
                    Question = TextBoxQuestion.Text,
                    QuestionNumber = int.Parse(TextBoxQuestionNumber.Text),
                    CorrectAnswer = TextBoxAnswer.Text,
                    TopicId = edcl.SelectedTopic.Id
                };
                edcl.AddQuestion2(newquestion);
                Clear();
            }
        }

        public void Clear()
        {
            TextBoxQuestion.Text = null;
            TextBoxAnswer.Text = null;
        }
    }
}
