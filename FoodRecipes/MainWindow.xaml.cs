using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FoodRecipes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
 

    public partial class MainWindow : Window
    {

     
        public MainWindow()
            {
                InitializeComponent();
            //GridPrincipal.Children.Clear();
            //GridPrincipal.Children.Add(new Home());
        }
           

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewMenu_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            switch(index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    boolSearch = 0;
                    GridPrincipal.Children.Add(new Home());
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Favorite());
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new NewRecipe());
                    break;
                default:
                    break;
            }    
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContenSlide.OnApplyTemplate();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            MainData.Margin = new Thickness(250, 0, 0, 0);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MainData.Margin = new Thickness(0, 0, 0, 0);
        }
        public static String dataSearch;
        public static int boolSearch = 0;
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            dataSearch = Search.Text;
            GridPrincipal.Children.Clear();
            Home home = new Home();
            GridPrincipal.Children.Add(home);
            Search.Text = null;
            boolSearch = 1;
        }
    }
    }

