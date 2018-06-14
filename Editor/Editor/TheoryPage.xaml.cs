using App.Classes.Main_Classes;
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
    /// Логика взаимодействия для TheoryPage.xaml
    /// </summary>
    public partial class TheoryPage : Page
    {
        EditorClass edcl = EditorClass.GetEditorClass();

        public TheoryPage()
        {
            InitializeComponent();
            edcl.AddClick += ButtonAdd_Click;
        }


        private void ButtonAdd_Click()
        {
            if (!String.IsNullOrWhiteSpace(TextBoxTheory.Text))
            {
                var newtheory = new Theory()
                {
                    Text = TextBoxTheory.Text,
                    TopicId = edcl.SelectedTopic.Id
                };
                edcl.AddTheory(newtheory);
                Clear();
            }
        }

        public void Clear()
        {
            TextBoxTheory.Text = null;
        }
    }
}
