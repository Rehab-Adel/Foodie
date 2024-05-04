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
            // rahoba work
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var customer=_DBContext.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            // rahoba work
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult DeleteCustomer(int id)
        {
            // rahoba work
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
        public IActionResult AddRestaurant(Restaurant restaurant )
        {
            // rahoba work
            return RedirectToAction("AllResturants");
        }

        [HttpGet]
        public IActionResult UpdateRestaurant(int id)
        {
            var resturant = _DBContext.Restaurants.FirstOrDefault(a => a.Id == id);
            return View(resturant);
        }
        [HttpPost]
        public IActionResult UpdateRestaurant(Restaurant restaurant)
        {
            // rahoba work
            return RedirectToAction("AllResturants");
        }
        [HttpPost]

        public IActionResult DeleteRestaurant(int id)
        {
            // rahoba work
            return RedirectToAction("AllResturants");
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
            // rahoba work
            return RedirectToAction("EditMenue", new {id=meal.Menu_Id});
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
            // rahoba work
            return RedirectToAction("EditMenue", new { id = meal.Menu_Id });
        }
        [HttpPost]

        public IActionResult DeleteMeal(int id)
        {
            var meal = _DBContext.Meals.FirstOrDefault(m => m.Id == id);
            // rahoba work
            return RedirectToAction("EditMenue", new { id = meal.Menu_Id });
        }
    }
}
