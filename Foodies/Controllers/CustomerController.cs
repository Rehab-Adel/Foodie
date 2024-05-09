﻿using Foodies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Foodies.Controllers
{
	public class CustomerController : Controller
	{
        private readonly ApplicationDBContext _DBContext;
        public CustomerController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public IActionResult Index(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve user ID claim
            var userNameClaim = User.FindFirstValue(ClaimTypes.Name); // Retrieve user name claim
            ViewBag.UserName = userNameClaim;
            var user = _DBContext.Customers.FirstOrDefault(x => x.Id == id);
            //ViewData["UserName"] = user.Username;
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var locationGroups = _DBContext.Restaurants
                .GroupBy(r => r.Location)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.LocationGroups = locationGroups; // Pass locationGroups to ViewBag

            return View(user);
        }

        public IActionResult SeeMenue(int id)
        {
            var restaurant = _DBContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
              
                return NotFound();
            }

            var menu = _DBContext.Menus.FirstOrDefault(x => x.Resturant_Id == restaurant.Id);
            if (menu == null)
            {
               
                return NotFound("Menu not found for this restaurant");
            }

       
            var menuItems = _DBContext.Meals.Where(m => m.Menu_Id == menu.Id).ToList();

        
            return View(menuItems);
        }

        public IActionResult MakeOrder(int id)
        {
            var meal = _DBContext.Meals.FirstOrDefault(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            TempData["MealID"] = meal.Id;
            return View(meal);
        }
        // Make sure to import your Reservation model
        public IActionResult ConfirmOrder(int id, Reservation reservation, bool Delivery)
        {
            var mealId = TempData["MealID"] != null ? (int)TempData["MealID"] : 0;

           
            var meal = _DBContext.Meals.FirstOrDefault(x => x.Id == mealId);
            reservation.Meal = meal;
            if (meal == null)
            {
                return NotFound(); // Handle case where meal is not found
            }
            reservation.Delivery= Delivery;
            reservation.Customer_Id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           reservation.Meal_Id = mealId;
            ModelState.Remove("Customer");
            ModelState.Remove("Meal");

            if (ModelState.IsValid)
            {
                try
                {
                 
                    var newReservation = new Reservation
                    {
                        Customer_Id = reservation.Customer_Id,
                      Meal_Id = reservation.Meal_Id,
                    PaymentType = reservation.PaymentType,
                        Quantity = reservation.Quantity,
                        Delivery = reservation.Delivery
                    };

                    _DBContext.Reservations.Add(newReservation);
                    _DBContext.SaveChanges();

                    return RedirectToAction("SeeMenue", new { id = reservation.Meal.Menu.Resturant_Id });
                }
                catch (Exception ex)
                {
                    
                    return View("Error"); // Display a generic error view
                }
            }

  
            return RedirectToAction("SeeMenue", new {id=meal.Menu.Resturant_Id});
        }

        public IActionResult CancelOrder(int id)
        {
            return RedirectToAction("SeeMenue", new { id = id });
        }
    }
}
