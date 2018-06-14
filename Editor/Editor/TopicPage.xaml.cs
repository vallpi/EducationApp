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
    /// Логика взаимодействия для TopicPage.xaml
    /// </summary>
    public partial class TopicPage : Page
    {
        EditorClass edcl = EditorClass.GetEditorClass();

        public TopicPage()
        {
            InitializeComponent();
            edcl.AddClick += ButtonAdd_Click;
        }

        private void ButtonAdd_Click()
        {
            if (!String.IsNullOrWhiteSpace(TextBoxTopicName.Text))
            {
                var newtopic = new Topic
                {
                    Name = TextBoxTopicName.Text,
                    SubjectId = edcl.SelectedSubject.Id
                };
                edcl.AddTopic(newtopic);
                Clear();
            }
        }

        public void Clear()
        {
            TextBoxTopicName.Text = null;
        }
    }
}
