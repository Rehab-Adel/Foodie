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
    }
}
