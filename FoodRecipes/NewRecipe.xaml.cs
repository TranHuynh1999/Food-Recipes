using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace FoodRecipes
{
    /// <summary>
    /// Interaction logic for NewRecipe.xaml
    /// </summary>
    public partial class NewRecipe : UserControl
    {
        public NewRecipe()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.Name = Title.Text;
            recipe.Avatar = "/Images/" + System.IO.Path.GetFileName(filePathNewAvatar);
            recipe.Step = steps;


            recipe.Ingredients = new string[]
            {
                  Ingredents.Text,
            };
            recipe.VideoLink = Youtube.Text;
            recipe.Favorite = false;
            bool result = RecipeDAO.AddRecipe(recipe);
            if (result == true)
            {
                MessageBox.Show("Add Recipe Success", "Notice", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Add Recipe Failed", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        string filePathNewAvatar;
        private void ButtonAddAvatarNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();

            if (screen.ShowDialog() == true)
            {
                filePathNewAvatar = screen.FileName;
                AvatarNewRecipe.Source = new BitmapImage(new Uri(filePathNewAvatar));
            }

            try
            {
                string newDir = AppDomain.CurrentDomain.BaseDirectory + "/Images/";
                string curFile = System.IO.Path.GetFileName(filePathNewAvatar);
                string newPathToFile = System.IO.Path.Combine(newDir, curFile);
                File.Copy(filePathNewAvatar, newPathToFile);
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }
        string filePathNewStepAvatar;
        private void ButtonAddImageStep_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();

            if (screen.ShowDialog() == true)
            {
                filePathNewStepAvatar = screen.FileName;
                newImageStep.Source = new BitmapImage(new Uri(filePathNewStepAvatar));
            }

            try
            {
                string newDir = AppDomain.CurrentDomain.BaseDirectory + "/Images/";
                string curFile = System.IO.Path.GetFileName(filePathNewStepAvatar);
                string newPathToFile = System.IO.Path.Combine(newDir, curFile);
                File.Copy(filePathNewStepAvatar, newPathToFile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        List<Step> steps = new List<Step>();
        private void ButtonAddStep_Click(object sender, RoutedEventArgs e)
        {
            Step step = new Step();
            step.Images = "/Images/" + System.IO.Path.GetFileName(filePathNewStepAvatar);
            step.Description = StepDescription.Text;
            if (step != null && step.Images != null && step.Description != null)
            {
                steps.Add(step);
                MessageBox.Show("Add Step Success", "Notice", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Add Step Failed", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
