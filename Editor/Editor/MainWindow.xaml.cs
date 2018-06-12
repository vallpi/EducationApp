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

        public MainWindow()
        {
            InitializeComponent();
            ComboBoxSubjects.ItemsSource = EditorClass.edcl.GetSubjectList();
        }

        private void ComboBoxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Subject)ComboBoxSubjects.SelectedItem;
            EditorClass.edcl.SelectedSubject = selected;
            EditorClass.edcl.SelectedTopic = null;
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = selected.Topics;
        }

        private void ListBoxTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Topic)ListBoxTopics.SelectedItem;
            EditorClass.edcl.SelectedTopic = selected;
        }

        private void None_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedSubject != null)
                Frame1.NavigationService.Navigate(new Uri("NonePage.xaml", UriKind.Relative));
        }

        private void Topic_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Question1_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedTopic != null)
                Frame1.NavigationService.Navigate(new Uri("Question1Page.xaml", UriKind.Relative));
        }

        private void Question2_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedTopic != null)
                Frame1.NavigationService.Navigate(new Uri("Question2Page.xaml", UriKind.Relative));
        }

        private void Theory_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateAll()
        {
        }

    }
}
