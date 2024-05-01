﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Models
{
	public class Reservation
	{
		[Key]
        public int Order_Id { get; set; }

        public string PaymentType { get; set; }

        public bool Delivery { get; set; }

		public int Resturant_Id { get; set; }
		public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

		[ForeignKey("Resturant_Id")]
		public virtual Restaurant Restaurant { get; set; }

		public virtual List<Meal> Meals { get; set; }=new List<Meal>();
    }
}