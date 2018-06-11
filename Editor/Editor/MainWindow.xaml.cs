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

namespace Editor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository _repo = Factory.Instance.GetRepository();

        public MainWindow()
        {
            InitializeComponent();
            ComboBoxSubjects.ItemsSource = _repo.GetSubjectList();
            Frame1.NavigationService.Navigate( new Uri("Question1Page.xaml", UriKind.Relative));
        }

        private void ComboBoxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Subject)ComboBoxSubjects.SelectedItem;
            EditorClass.edcl.SelectedSubject = selected;
        }
    }
}
