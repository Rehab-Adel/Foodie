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

    }
}
