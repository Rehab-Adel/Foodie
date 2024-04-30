using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Models
{
	public class Meal
	{
        public int Id { get; set; }

        public string MealName { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int Menu_Id { get; set; }

        public int Reservation_Id { get; set; } //order

        [ForeignKey("Menu_Id")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("Reservation_Id")]
        public virtual Reservation Reservation { get; set; }
    }
}
