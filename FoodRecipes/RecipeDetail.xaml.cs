using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FoodRecipes
{
    /// <summary>
    /// Interaction logic for RecipeDetail.xaml
    /// </summary>
    public partial class RecipeDetail : Window
    {
        public Recipe SelectedRecipe { get; set; }
        public RecipeDetail(Recipe recipe)
        {
            SelectedRecipe = recipe;
            InitializeComponent();
            ShowRecipeDetail();
        }

        int currentStep = 0;
        bool isFavorite = false;
        
        private void ShowRecipeDetail()
        {
            isFavorite = this.SelectedRecipe.Favorite;
            favoriteIcon.Foreground = (isFavorite) ? Brushes.Red : Brushes.Gray;

            RecipeNameTextBlock.Text = this.SelectedRecipe.Name + "\n";

            foreach(String s in SelectedRecipe.Ingredients)
            {
                IngredientTextBlock.Text += s + "\n";
            }
            StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;
            try
            {
                RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                var image = new BitmapImage(new Uri(absolutePath));
                StepImage.Source = image;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }

        private void PreStep_Button_Click(object sender, RoutedEventArgs e)
        {
            if(currentStep > 0)
            {
                currentStep --;
                StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;

                try
                {
                    RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                    String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                    var image = new BitmapImage(new Uri(absolutePath));
                    StepImage.Source = image;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        private void NextStep_Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentStep < SelectedRecipe.Step.Count - 1)
            {
                currentStep ++;
                StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;

                try
                {
                    RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                    String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                    var image = new BitmapImage(new Uri(absolutePath));
                    StepImage.Source = image;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        public delegate void MyDeligateType(bool isFavorite);
        public event MyDeligateType Handler;

        private void onFavoriteButtonClick(object sender, RoutedEventArgs e)
        {
            isFavorite = !isFavorite;
            favoriteIcon.Foreground = (isFavorite) ? Brushes.Red : Brushes.Gray;
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Handler != null)
            {
                Handler(isFavorite);
            }
        }


    }
}
