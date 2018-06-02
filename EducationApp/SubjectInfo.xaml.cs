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
    /// Interaction logic for SubjectInfo.xaml
    /// </summary>
    public partial class SubjectInfo : Page
    {
        public SubjectInfo()
        {
            InitializeComponent();
        }

        private void ButtonStartTest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestQuestion1.xaml"), UriKind.Relative);
        }
    }
}
