﻿using Newtonsoft.Json;
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
    class RecipeDAO
    {
        //public static int ITEMS_PER_PAGE = 10;
        //public static int TOTAL_PAGE;
        //public static int PAGE;

        private static string PATH = @"C:\Users\kptan\Desktop\Food-Recipes-main\FoodRecipes\Data\Recipes.json";
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

            try
            {
                string jsonData = System.IO.File.ReadAllText(PATH);
                recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData);
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
            //List<Recipe> tempRecipes = null;
            //for(int i=0;i<recipes.Count;i++)
            //{
            //    Recipe temp = recipes[i];
            //    if((String.Compare(searchName,temp.Name, true) == 0))
            //    {
            //        tempRecipes.Add(temp);
            //    }
            //    else
            //    {
            //        //Do nothing
            //    }

            //}
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
