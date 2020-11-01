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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private int itemPerPage = 10;
        private int currentPage = 0;
        private int totalItem;
        private int totalPage;
        public Home()
        {
            InitializeComponent();
          
        }

        //BindingList<Food> _listFood = new BindingList<Food>();

        List<Recipe> recipes;
        private void BindingFood(object sender, RoutedEventArgs e)
        {
            if (MainWindow.boolSearch == 0)
            {
                recipes = RecipeDAO.getAllRecipesFromJson();
                if (recipes != null)
                {
                    totalItem = recipes.Count();

                    int floor = totalItem / itemPerPage;
                    totalPage = (totalItem % itemPerPage == 0) ? floor : (floor + 1);

                //MessageBox.Show(totalItem.ToString());

                    DataListview.ItemsSource = getNextPageItems();
                }
            }
            else
            {
                recipes = RecipeDAO.SearchRecipe(MainWindow.dataSearch);
                totalItem = recipes.Count();

                int floor = totalItem / itemPerPage;
                totalPage = (totalItem % itemPerPage == 0) ? floor : (floor + 1);

                //MessageBox.Show(totalItem.ToString());

                if (recipes != null)
                {
                    DataListview.ItemsSource = getNextPageItems();
                }
            }
            
 
        }


        int startIndex = 0;
        private List<Recipe> getNextPageItems()
        {
            List<Recipe> recipesOfPage = null;
            if (recipes != null && currentPage < totalPage)
            {
                startIndex = currentPage * itemPerPage;

                if (startIndex < totalItem)
                {
                    int count = (totalItem - startIndex) >= itemPerPage ? itemPerPage : (totalItem - startIndex);
                    recipesOfPage = recipes.GetRange(startIndex, count);

                    currentPage++;
                }


            }

            return recipesOfPage;
        }

        private void NextPageClick(object sender, RoutedEventArgs e)
        {
            List<Recipe> temp = getNextPageItems();
            if (temp != null)
            {
                DataListview.ItemsSource = temp;
            }
        }

        private List<Recipe> getPreviousPageItems()
        {
            List<Recipe> recipesOfPage = null;
            if (recipes != null && currentPage > 1)
            {
                startIndex -= itemPerPage;
                recipesOfPage = recipes.GetRange(startIndex, itemPerPage);

                currentPage--;

            }

            return recipesOfPage;
        }

        private void PreviousPageClick(object sender, RoutedEventArgs e)
        {
            List<Recipe> temp = getPreviousPageItems();
            if (temp != null)
            {
                DataListview.ItemsSource = temp;
            }
        }
        //public void resultsSearch()
        //{
        //    recipes = RecipeDAO.SearchRecipe(MainWindow.dataSearch);

        //    if (recipes != null)
        //    {
        //        DataListview.ItemsSource = getNextPageItems();
        //    }
        
           

        //}

        }
    }
