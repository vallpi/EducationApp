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
        public TopicPage()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var newtopic = new Topic
            {
                Name = TextBoxTopicName.Text,
                SubjectId = EditorClass.edcl.SelectedSubject.Id
            };
            EditorClass.edcl.AddTopic(newtopic);
            Clear();
        }

        public void Clear()
        {
            TextBoxTopicName.Text = null;
        }
    }
}
