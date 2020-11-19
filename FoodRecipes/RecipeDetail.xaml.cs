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
using System.Windows.Navigation;
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
            if (this.SelectedRecipe.VideoLink != "") { 
                YTlink.NavigateUri = new Uri(this.SelectedRecipe.VideoLink, UriKind.Absolute);
            }
            else
            {
                YTlink.Click += nullclickhandler;
            }

            foreach (String s in SelectedRecipe.Ingredients)
            {
                IngredientTextBlock.Text += s + "\n";
            }
            StepDescriptionTextBlock.Text = $"Bước {currentStep + 1}: " + SelectedRecipe.Step[currentStep].Description;
            try
            {
                if (SelectedRecipe.Step[currentStep].Images != "") { 
                RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                var image = new BitmapImage(new Uri(absolutePath));
                StepImage.Source = image;
                }
                else
                {
                    StepImage.Source = null;
                }    

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }

        private void nullclickhandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chưa có video hướng dẫn", "Sorry!");
            
        }

        private void PreStep_Button_Click(object sender, RoutedEventArgs e)
        {
            if(currentStep > 0)
            {
                currentStep --;
                StepDescriptionTextBlock.Text = $"Bước {currentStep + 1}: " + SelectedRecipe.Step[currentStep].Description;
               // StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;

                try
                {
                    //RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                    //String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                    //var image = new BitmapImage(new Uri(absolutePath));
                    //StepImage.Source = image;
                    if (SelectedRecipe.Step[currentStep].Images != "")
                    {
                        RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                        String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                        var image = new BitmapImage(new Uri(absolutePath));
                        StepImage.Source = image;
                    }
                    else
                    {
                        StepImage.Source = null;
                    }
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
                //StepDescriptionTextBlock.Text = SelectedRecipe.Step[currentStep].Description;
                StepDescriptionTextBlock.Text = $"Bước {currentStep + 1}: " + SelectedRecipe.Step[currentStep].Description;


                try
                {
                    //RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                    //String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                    //var image = new BitmapImage(new Uri(absolutePath));
                    //StepImage.Source = image;
                    if (SelectedRecipe.Step[currentStep].Images != "")
                    {
                        RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
                        String absolutePath = (String)converter.Convert(SelectedRecipe.Step[currentStep].Images, null, null, null);
                        var image = new BitmapImage(new Uri(absolutePath));
                        StepImage.Source = image;
                    }
                    else
                    {
                        StepImage.Source = null;
                    }
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

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
