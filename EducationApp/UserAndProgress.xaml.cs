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

namespace EducationApp
{
    /// <summary>
    /// Interaction logic for UserAndProgress.xaml
    /// </summary>
    public partial class UserAndProgress : Page
    {
        IRepository _repo = Factory.Instance.GetRepository();
        public UserAndProgress()
        {
            InitializeComponent();
            textBlock_Subject1.Text = _repo.GetSubject(1);
            textBlock_Subject2.Text = _repo.GetSubject(2);
            textBlock_Subject3.Text = _repo.GetSubject(3);
        }

        private void HyperLink1_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(1);
        }

        private void HyperLink2_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(2);
        }

        private void HyperLink3_Click(object sender, RoutedEventArgs e)
        {
            _repo.SelectSubject(3);
        }
    }
}
