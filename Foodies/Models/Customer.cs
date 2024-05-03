using System.ComponentModel.DataAnnotations;

namespace Foodies.Models
{
	public class Customer
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

		public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }=new List<Reservation>();
    }
}
