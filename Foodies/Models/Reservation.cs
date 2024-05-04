using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Models
{
    public class Reservation
    {
        [Key]
        public int Reservation_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Meal_Id { get; set; }
        public string PaymentType { get; set; }
        public int Quantity { get; set; }
        public bool Delivery { get; set; }

        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Meal_Id")]
        public virtual Meal Meal { get; set; }
    }
}
