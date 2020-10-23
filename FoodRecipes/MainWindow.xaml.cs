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
            }
            class Food
            {
                public string Name { get; set; }
                public string Avatar { get; set; }

            }
            class FoodDAO
            {
                public static BindingList<Food> GetAllFood()
                {
                    var result = new BindingList<Food>()
                {
                    new Food() { Name = "Bach Tuoc Ngam sa", Avatar = "/Images/Bachtuocngamsa.jpg"},
                    new Food() { Name = "Ca nuc kho nuoc dua", Avatar = "/Images/Canuckhonuocdua.jpg" },
                    new Food() { Name = "Chan gio ham", Avatar = "/Images/Changioham.jpg" },
                    new Food() { Name = "Dau hu sot nam huong", Avatar = "/Images/Dauhusotnamhuong.jpg" },
                    new Food() { Name = "Ga chien han quoc", Avatar = "/Images/Gachienhanquoc.jpg" },
                    new Food() { Name = "Muc xao sot cay", Avatar = "/Images/Mucxaosotcay.jpg" },
                    new Food() { Name = "Nui sot tom", Avatar = "/Images/Nuisottom.jpg" },
                    new Food() { Name = "Thit ga hap nam huong", Avatar = "/Images/Thitgahapnamhuong.jpg" },
                    new Food() { Name = "Tom rang muoi tieu", Avatar = "/Images/Tomrangmuoitieu.jpg" }
                };
                    return result;
                }
            }
            BindingList<Food> _listFood = new BindingList<Food>();

            private void BindingFood(object sender, RoutedEventArgs e)
            {
                _listFood = FoodDAO.GetAllFood();
                DataListview.ItemsSource = _listFood;
            }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewMenu_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContenSlide.OnApplyTemplate();
            GridCorsor.Margin = new Thickness(0,(100+(60*index)),0,0);
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            MainData.Margin = new Thickness(250, 0, 0, 0);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MainData.Margin = new Thickness(0, 0, 0, 0);
        }
    }
    }

