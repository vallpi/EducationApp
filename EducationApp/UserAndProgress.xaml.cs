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
            foreach (Subject s in _repo.GetSubjectList())
                AddSubject(s.Name);
        }


        private void AddSubject(string name)
        {
            TextBlock text = new TextBlock()
            {
                Text = name,
                FontSize = 18,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF8DC")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness { Left = 100, Right = 100 },
            };
            var list = ListSubj.Items;
            list.Add(text);
        }

        private void ListSubj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SubjectInfo.xaml", UriKind.Relative));
            TextBlock text = (TextBlock)ListSubj.SelectedItem;
            int id = _repo.GetSubjectList().Where(n => n.Name == text.Text).FirstOrDefault().Id;
            _repo.SelectSubject(id);

        }

    }
}
