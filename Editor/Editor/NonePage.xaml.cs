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
    /// Логика взаимодействия для NonePage.xaml
    /// </summary>
    public partial class NonePage : Page
    {
        EditorClass edcl = EditorClass.GetEditorClass();


        public NonePage()
        {
            InitializeComponent();
            edcl.AddClick += ButtonAdd_Click;
        }

        private void ButtonAdd_Click()
        {
            if (!String.IsNullOrWhiteSpace(TextBoxSubjectName.Text))
            {
                var newsubject = new Subject
                {
                    Name = TextBoxSubjectName.Text
                };
                edcl.AddSubject(newsubject);
                Clear();
            }
        }

        public void Clear()
        {
            TextBoxSubjectName.Text = null;
        }
    }
}
