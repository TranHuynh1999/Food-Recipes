using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodRecipes
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        Random _rng = new Random();
        public SplashWindow()
        {
            InitializeComponent();
            try
            {
                string imagePath = @"\Images\Background.jpg";
                RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                String absolutePath = (String)converter.Convert(imagePath, null, null, null);
                var image = new BitmapImage(new Uri(absolutePath));
                BackgroundImage.ImageSource = image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void neverShowAgain_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        System.Timers.Timer timer;

        private void window_Loaded(object sender, RoutedEventArgs e)
        {

            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();
                this.Close();
            }
            else
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;
                timer.Start();
                readData();
            }
        }
        int cout = 0;
        int target = 5;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            cout++;
            if (cout == target)
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    var screen = new MainWindow();
                    screen.Show();
                    this.Close();
                });
            }
        }

  
        private void readData()
        {
            //Uri path = new Uri(@"Data.txt", UriKind.RelativeOrAbsolute);

            //string[] lines = System.IO.File.ReadAllLines(path.ToString());
            //int result = _rng.Next(lines.Length);

            //interestingInformation.Text = lines[result];
            try
            {
                string stringPath = @"Data\DataInformationInteresting.txt";

                string[] lines = System.IO.File.ReadAllLines(stringPath);
                int result = _rng.Next(lines.Length);
                interestingInformation.Text = lines[result];
            }
            catch (Exception) { }
        }

        //public static List<string> docFile(string path)
        //{
        //    List<string> result = new List<string>();
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader(path))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                result.Add(line);
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    { }
        //    return result;
        //}


        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
            timer.Stop();
        }
    }
}
