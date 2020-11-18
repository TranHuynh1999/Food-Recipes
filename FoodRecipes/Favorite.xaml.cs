﻿using System;
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

namespace FoodRecipes
{
    /// <summary>
    /// Interaction logic for Favorite.xaml
    /// </summary>
    public partial class Favorite : UserControl
    {

        private int itemPerPage = 10;
        private int currentPage = 0;
        private int totalItem;
        private int totalPage;

        public Favorite()
        {
            InitializeComponent();
            showList();
        }

        List<Recipe> favoriteRecipes;

        private void showList()
        {
            favoriteRecipes = RecipeDAO.getFavoriteRecipesFromJson();
            if(favoriteRecipes != null)
            {
            totalItem = favoriteRecipes.Count();
            int floor = totalItem / itemPerPage;
            totalPage = (totalItem % itemPerPage == 0) ? floor : (floor + 1);

                FavoriteListView.ItemsSource = getNextPageItems();
            favoritePagingInfo.Text = $"{currentPage}/{totalPage}";
            }


            //MessageBox.Show(totalItem.ToString());
            
        }

        int startIndex = 0;
        private List<Recipe> getNextPageItems()
        {
            List<Recipe> recipesOfPage = null;
            if (favoriteRecipes != null && currentPage < totalPage)
            {
                startIndex = currentPage * itemPerPage;

                if (startIndex < totalItem)
                {
                    int count = (totalItem - startIndex) >= itemPerPage ? itemPerPage : (totalItem - startIndex);
                    recipesOfPage = favoriteRecipes.GetRange(startIndex, count);

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
                FavoriteListView.ItemsSource = temp;
                favoritePagingInfo.Text = $"{currentPage}/{totalPage}";
            }
        }

        private List<Recipe> getPreviousPageItems()
        {
            List<Recipe> recipesOfPage = null;
            if (favoriteRecipes != null && currentPage > 1)
            {
                startIndex -= itemPerPage;
                recipesOfPage = favoriteRecipes.GetRange(startIndex, itemPerPage);

                currentPage--;

            }

            return recipesOfPage;
        }

        private void PreviousPageClick(object sender, RoutedEventArgs e)
        {
            List<Recipe> temp = getPreviousPageItems();
            if (temp != null)
            {
                FavoriteListView.ItemsSource = temp;
                favoritePagingInfo.Text = $"{currentPage}/{totalPage}";
            }
        }
    }
}
