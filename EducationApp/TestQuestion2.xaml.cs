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
    /// Interaction logic for TestQuestion2.xaml
    /// </summary>
    public partial class TestQuestion2 : Page
    {
        public TestQuestion2()
        {
            InitializeComponent();
        }

        private void ButtonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestQuestion1.xaml"), UriKind.Relative);
        }
    }
}
