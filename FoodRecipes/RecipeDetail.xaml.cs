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
        
        private void ShowRecipeDetail()
        {
            RecipeNameTextBlock.Text = this.SelectedRecipe.Name + "\n";

            foreach(String s in SelectedRecipe.Ingredients)
            {
                IngredientTextBlock.Text += s + "\n";
            }
            StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;
            try
            {
                var image = new BitmapImage(new Uri(SelectedRecipe.Step[currentStep].Images));
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
                    var image = new BitmapImage(new Uri(SelectedRecipe.Step[currentStep].Images));
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
                    var image = new BitmapImage(new Uri(SelectedRecipe.Step[currentStep].Images));
                    StepImage.Source = image;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
