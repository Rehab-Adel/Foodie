using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Menu_Id { get; set; } 

        [ForeignKey("Menu_Id")]
        public virtual Menu Menu { get; set; }
    }
}

