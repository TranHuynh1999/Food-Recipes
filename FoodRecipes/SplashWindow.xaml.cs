using System;
using System.Collections.Generic;
using System.Configuration;
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

  
        private readonly String PATH = @"Data\DataInformationInteresting.txt";
        private void readData()
        {
            RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
            String absolutePath = (String)converter.Convert(PATH, null, null, null);

            try
            {
                string[] lines = System.IO.File.ReadAllLines(absolutePath);
                int result = _rng.Next(lines.Length);
                interestingInformation.Text = lines[result];
            }
            catch (Exception) { }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
            timer.Stop();
        }
    }
}
