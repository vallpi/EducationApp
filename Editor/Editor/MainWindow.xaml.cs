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
            EditorClass.edcl.AddQ += RadioButtonUpdate;
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
            RadioButtonUpdate();
        }

        private void None_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedSubject != null)
                Frame1.NavigationService.Navigate(new Uri("NonePage.xaml", UriKind.Relative));
            else None.IsChecked = false;
        }

        private void Topic_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedSubject != null)
                Frame1.NavigationService.Navigate(new Uri("TopicPage.xaml", UriKind.Relative));
            else Topic.IsChecked = false;
        }

        private void Question1_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedTopic != null)
            {
                Frame1.NavigationService.Navigate(new Uri("Question1Page.xaml", UriKind.Relative));
                UpdateListBoxQuestions(EditorClass.edcl.GetListQuestions1);
            }
            else Question1.IsChecked = false;
        }

        private void Question2_Checked(object sender, RoutedEventArgs e)
        {
            if (EditorClass.edcl.SelectedTopic != null)
            {
                Frame1.NavigationService.Navigate(new Uri("Question2Page.xaml", UriKind.Relative));
                UpdateListBoxQuestions(EditorClass.edcl.GetListQuestions2);
            }
            else Question2.IsChecked = false;
        }

        private void Theory_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateListBoxQuestions<T>(List<T> list)
        {
            ListBoxQuestions.IsEnabled = true;
            ListBoxTheory.IsEnabled = false;
            ListBoxQuestions.ItemsSource = null;
            ListBoxQuestions.ItemsSource = list;
        }

        public void RadioButtonUpdate()
        {
            foreach (Control c in StackPanelRButtons.Children)
            {
                RadioButton rb = c as RadioButton;
                if (rb.IsChecked.Value)
                {
                    rb.IsChecked = false;
                    rb.IsChecked = true;
                }
            }
        }

        private string GetCheckedRadioButtonName()
        {
            foreach (Control c in StackPanelRButtons.Children)
            {
                RadioButton rb = c as RadioButton;
                if (rb.IsChecked.Value)
                {
                    return rb.Content.ToString();
                }
            }
            return null;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditorClass.edcl.AddButtonClick();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            EditorClass.edcl.DeleteButtonClick(GetCheckedRadioButtonName());
        }

        private void ListBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxQuestions.SelectedItem is QuestionModel1)
                EditorClass.edcl.SelectedQuestion1 = ListBoxQuestions.SelectedItem as QuestionModel1;
            else EditorClass.edcl.SelectedQuestion2 = ListBoxQuestions.SelectedItem as QuestionModel2;
        }

        private void ListBoxTheory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
