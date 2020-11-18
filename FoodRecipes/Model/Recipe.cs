using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public List<Step> Step { get; set; }
        public string[] Ingredients { get; set; }
        public string VideoLink { get; set; }
        public bool Favorite { get; set; }

       
    }
}
