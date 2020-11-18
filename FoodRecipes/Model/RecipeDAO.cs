using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes
{
    public class RecipeDAO
    {
        //public static int ITEMS_PER_PAGE = 10;
        //public static int TOTAL_PAGE;
        //public static int PAGE;

        private static string PATH = @"Data\Recipes.json";
        public static void createJson()
        {
            try
            {
                // Check if file already exists.
                if (File.Exists(PATH))
                {
                    return;
                }

                // Create a new file
                FileStream fs = File.Create(PATH);

            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.ToString());
            }
        }

        public static List<Recipe> getAllRecipesFromJson()
        {
            List<Recipe> recipes = null;

            RelativeToAbsoluteConverter converter = new RelativeToAbsoluteConverter();
            String absolutePath = (String)converter.Convert(PATH, null, null, null);

            try
            {
                using (StreamReader r = new StreamReader(absolutePath))
                {
                    string jsonData = r.ReadToEnd();
                    recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData);
                }

                //string jsonData = System.IO.File.ReadAllText(PATH);
            }
            catch (Exception ex)
            {
                recipes = null;
                Debug.WriteLine(ex.ToString());
            }

            return recipes;
        }

        public static string AddRecipe(Recipe recipe)
        {
            try
            {
                List<Recipe> recipes = getAllRecipesFromJson();
                if (recipes == null)
                {
                    recipes = new List<Recipe>();
                }
                recipes.Add(recipe);

                string json = JsonConvert.SerializeObject(recipes, Formatting.Indented);
                System.IO.File.WriteAllText(PATH, json, Encoding.UTF8);

                return json;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }
        public static List<Recipe> SearchRecipe(string searchName)
        {

            List<Recipe> recipes = RecipeDAO.getAllRecipesFromJson();
            var query = from c in recipes
                        where c.Name.ToLower().Contains(searchName)
                        select c;
            return query.ToList();

        }

        public static List<Recipe> getNextPageItems(int pageIndex, int itemPerPage)
        {
            return null;
        }
        
        public static List<Recipe> getPreviousPageItems(int pageIndex, int itemPerPage)
        {
            return null;
        }

        public static List<Recipe> getPageItems(int page)
        {
            return null;
        }

    }
}
