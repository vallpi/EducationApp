using App.Classes;
using App.Classes.Interface;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EditorClass edcl = EditorClass.GetEditorClass();

        public MainWindow()
        {
            InitializeComponent();
            ComboBoxSubjects.ItemsSource = edcl.GetSubjectList();
        }

        private void ComboBoxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Subject)ComboBoxSubjects.SelectedItem;
            edcl.SelectedSubject = selected;
            edcl.SelectedTopic = null;
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = selected.Topics;
        }

        private void ListBoxTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Topic)ListBoxTopics.SelectedItem;
            edcl.SelectedTopic = selected;
            RadioButtonUpdate();
        }

        private void None_Checked(object sender, RoutedEventArgs e)
        {
            if (edcl.SelectedSubject != null)
                Frame1.NavigationService.Navigate(new Uri("NonePage.xaml", UriKind.Relative));
            else None.IsChecked = false;
        }

        private void Topic_Checked(object sender, RoutedEventArgs e)
        {
            if (edcl.SelectedSubject != null)
            {
                Frame1.NavigationService.Navigate(new Uri("TopicPage.xaml", UriKind.Relative));
            }
            else Topic.IsChecked = false;
        }

        private void Question1_Checked(object sender, RoutedEventArgs e)
        {
            if (edcl.SelectedTopic != null)
            {
                Frame1.NavigationService.Navigate(new Uri("Question1Page.xaml", UriKind.Relative));
                UpdateListBoxQuestions(edcl.GetListQuestions1);
            }
            else Question1.IsChecked = false;
        }

        private void Question2_Checked(object sender, RoutedEventArgs e)
        {
            if (edcl.SelectedTopic != null)
            {
                Frame1.NavigationService.Navigate(new Uri("Question2Page.xaml", UriKind.Relative));
                UpdateListBoxQuestions(edcl.GetListQuestions2);
            }
            else Question2.IsChecked = false;
        }

        private void Theory_Checked(object sender, RoutedEventArgs e)
        {
            if (edcl.SelectedTopic != null)
            {
                Frame1.NavigationService.Navigate(new Uri("TheoryPage.xaml", UriKind.Relative));
                UpdateListBoxTheory(edcl.GetListTheory);
            }
            else Theory.IsChecked = false;
        }

        private void UpdateListBoxQuestions<T>(List<T> list)
        {
            ListBoxTheory.Visibility = Visibility.Hidden;
            ListBoxQuestions.Visibility = Visibility.Visible;
            ListBoxQuestions.ItemsSource = null;
            ListBoxQuestions.ItemsSource = list;
        }

        private void UpdateListBoxTheory(List<Theory> list)
        {
            ListBoxTheory.Visibility = Visibility.Visible;
            ListBoxQuestions.Visibility = Visibility.Hidden;
            ListBoxTheory.ItemsSource = null;
            ListBoxTheory.ItemsSource = list;
        }

        public void UpdateListBoxTopics()
        {
            ListBoxTopics.ItemsSource = null;
            ListBoxTopics.ItemsSource = edcl.GetListTopics();
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
            edcl.AddButtonClick();
            RadioButtonUpdate();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            edcl.DeleteButtonClick(GetCheckedRadioButtonName());
            RadioButtonUpdate();
        }

        private void ListBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxQuestions.SelectedItem is QuestionModel1)
            {
                edcl.SelectedQuestion1 = ListBoxQuestions.SelectedItem as QuestionModel1;
                edcl.SelectedQuestion2 = null;
                edcl.SelectedTheory = null;
            }
            else
            {
                edcl.SelectedQuestion2 = ListBoxQuestions.SelectedItem as QuestionModel2;
                edcl.SelectedQuestion1 = null;
                edcl.SelectedTheory = null;
            }
        }

        private void ListBoxTheory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            edcl.SelectedTheory = ListBoxTheory.SelectedItem as Theory;
            edcl.SelectedQuestion1 = null;
            edcl.SelectedQuestion2 = null;
        }
    }
}
