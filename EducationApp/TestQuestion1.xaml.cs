﻿using App.Classes;
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

namespace EducationApp
{
    /// <summary>
    /// Interaction logic for TestQuestion1.xaml
    /// </summary>
    public partial class TestQuestion1 : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();

        public TestQuestion1()
        {
            InitializeComponent();
            var question = _repo.ReturnQuestionModel1();
            textBlock_Question.Text = question.Question;
            textBlock_Answer1.Text = question.Answer1;
            textBlock_Answer2.Text = question.Answer2;
            textBlock_Answer3.Text = question.Answer3;
            textBlock_Answer4.Text = question.Answer4;
            textBlock_Subject1.Text = _repo.GetSubject(1);
            textBlock_Subject2.Text = _repo.GetSubject(2);
            textBlock_Subject3.Text = _repo.GetSubject(3);
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink1_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(1);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink2_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(2);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void Hyperlink3_Click(object sender, RoutedEventArgs e)
        {
            _repo.interruptTest();
            _repo.SelectSubject(3);
            listBox_Themes.ItemsSource = null;
            listBox_Themes.ItemsSource = _repo.ReturnSubjectTopics();
        }

        private void ButtonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            bool? isChecked1 = RadioButton1.IsChecked;
            bool? isChecked2 = RadioButton2.IsChecked;
            bool? isChecked3 = RadioButton3.IsChecked;
            bool? isChecked4 = RadioButton4.IsChecked;
            string value = " ";
            if ((bool)isChecked1)
            {
                value = textBlock_Answer1.Text;
            }
            else
            {
                if ((bool)isChecked2)
                {
                    value = textBlock_Answer2.Text;
                }
                else
                {
                    if ((bool)isChecked3)
                    {
                        value = textBlock_Answer3.Text;
                    }
                    else
                    {
                        if ((bool)isChecked4)
                        {
                            value = textBlock_Answer4.Text;
                        }
                    }
                }
            }
            _repo.CheckAnswer(value, 1);
            if (_repo.GetQuestionType() == 1)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                var question = _repo.ReturnQuestionModel1();
                textBlock_Question.Text = question.Question;
                textBlock_Answer1.Text = question.Answer1;
                textBlock_Answer2.Text = question.Answer2;
                textBlock_Answer3.Text = question.Answer3;
                textBlock_Answer4.Text = question.Answer4;
            }
            else
            {
                if (_repo.GetQuestionType() == 2)
                {
                    this.NavigationService.Navigate(new System.Uri("TestQuestion2.xaml", UriKind.Relative));
                }
            }
            if (_repo.GetQuestionType() == 0)
            {
                _repo.GetTestResult();
                this.NavigationService.Navigate(new System.Uri("UserAndProgress.xaml", UriKind.Relative));
            }
        }
    }
}
