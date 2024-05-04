using Foodies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Foodies.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _DBContext;
        public AdminController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public IActionResult Index()
        {
            var customers = _DBContext.Customers.ToList();


            ViewBag.Customers = customers;
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var admin = _DBContext.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
                if (admin != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                _DBContext.Customers.Add(customer);
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var customer=_DBContext.Customers.FirstOrDefault(a => a.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            if(ModelState.IsValid && customer != null)
            {
                _DBContext.Entry(customer).State = EntityState.Modified;
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            var customerToDelete = _DBContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customerToDelete != null)
            {
                _DBContext.Customers.Remove(customerToDelete);
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //end crud customer 


        [HttpGet]
        public IActionResult AllResturants()
        {
            var resturants = _DBContext.Restaurants.ToList();
            ViewBag.Resturants = resturants;
            return View();
        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Restaurants.Add(restaurant);
                _DBContext.SaveChanges();

                //menu 
                var newMenu = new Menu { Resturant_Id = restaurant.Id };
                _DBContext.Menus.Add(newMenu);
                _DBContext.SaveChanges();
                return RedirectToAction("AllResturants");
            }
            return View(restaurant);
        }

        [HttpGet]
        public IActionResult UpdateRestaurant(int id)
        {
            var resturant = _DBContext.Restaurants.FirstOrDefault(a => a.Id == id);
            if(resturant == null)
            {
                return NotFound();
            }
            return View(resturant);
        }
        [HttpPost]
        public IActionResult UpdateRestaurant(Restaurant restaurant)
        {
            if(ModelState.IsValid)
            {
                _DBContext.Entry(restaurant).State = EntityState.Modified;
                _DBContext.SaveChanges();
                return RedirectToAction("AllResturants");
            }
            return View(restaurant);
        }
        [HttpPost]

        public IActionResult DeleteRestaurant(int id)
        {
            var restaurant = _DBContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                _DBContext.Restaurants.Remove(restaurant);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("AllRestaurants");
        }
        //end resturant

        [HttpGet]

        public IActionResult EditMenue(int id)
        {
            var menuId = _DBContext.Menus.FirstOrDefault(m => m.Resturant_Id == id)?.Id;
            if (menuId != null)
            {
                var menuItems = _DBContext.Meals.Where(m => m.Menu_Id == menuId).ToList();
                return View(menuItems);
            }
            return NotFound(); 
        }

        [HttpGet]
        public IActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMeal(Meal meal)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Meals.Add(meal);
                _DBContext.SaveChanges();
                return RedirectToAction("EditMenue", new { id = meal.Menu_Id });
            }
            return View(meal);
        }




        [HttpGet]
        public IActionResult UpdateMeal(int id)
        {
            var meal = _DBContext.Meals.FirstOrDefault(a => a.Id == id);
            return View(meal);
        }
        [HttpPost]
        public IActionResult UpdateMeal(Meal meal)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Entry(meal).State = EntityState.Modified;
                _DBContext.SaveChanges();
                return RedirectToAction("EditMenu", new { id = meal.Menu_Id });
            }
            return View(meal);
        }
        [HttpPost]

        public IActionResult DeleteMeal(int id)
        {
            var meal = _DBContext.Meals.FirstOrDefault(m => m.Id == id);
            if (meal != null)
            {
                _DBContext.Meals.Remove(meal);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("EditMenu", new { id = meal.Menu_Id });
        }
        
    }
}
