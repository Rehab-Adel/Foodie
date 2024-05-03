using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Models
{
	public class Menu
	{
        public int Id { get; set; }

        public int Resturant_Id { get; set; }

		[ForeignKey("Resturant_Id ")]
		public virtual Restaurant Restaurant { get; set; }

		public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
	}
}
