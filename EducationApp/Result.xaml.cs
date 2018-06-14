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
using System.Windows.Shapes;

namespace EducationApp
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        IRepository _repo = Factory.Instance.GetRepository();
        MediaElement sound;

        public Result()
        {
            int result = _repo.ShowResult();
            _repo.GetTestResult();
            InitializeComponent();
            sound = new MediaElement();

            if (result < 5)
            {
                textBlock_Result.Text = "Результат: " + result + " / 10";
                image_Result.Source = new BitmapImage(new Uri(@"Resources/Affleck_meme.jpg", UriKind.Relative));
                mediaElement_music.Source = new Uri(@"Resources/Sad_Soundtrack.mp3", UriKind.Relative);
            }
            else if (result < 8)
            {
                textBlock_Result.Text = "Результат: " + result + " / 10";
                image_Result.Source = new BitmapImage(new Uri(@"Resources/Deadpool_meme.jpg", UriKind.Relative));
                mediaElement_music.Source = new Uri(@"Resources/Maximum_Effort_Deadpool.mp3", UriKind.Relative);
            }
            else
            {
                textBlock_Result.Text = "Результат: " + result + " / 10";
                image_Result.Source = new BitmapImage(new Uri(@"Resources/Pewdiepie_meme.jpg", UriKind.Relative));
                mediaElement_music.Source = new Uri(@"Resources/Vary_Naice_Pewdiepie.mp3", UriKind.Relative);
            }
        }
    }
}