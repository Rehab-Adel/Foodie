using Foodies.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Foodies.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _DBContext;

        public AccountController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public IActionResult Index()
        {
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
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var customer = _DBContext.Customers.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (customer != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()), // ID claim
                        new Claim(ClaimTypes.Name, customer.Username) // Name claim
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(claimsPrincipal).Wait();
                    return RedirectToAction("Index", "Customer", new {id=customer.Id});
                }
                else
                {
                    var admin = _DBContext.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
                    if (admin != null)
                    {
                        var claims = new[]
                         {
                            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()), // ID claim
                         
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        HttpContext.SignInAsync(claimsPrincipal).Wait(); // Sign in user

                        return RedirectToAction("Index", "Admin");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid email or Password");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!_DBContext.Admins.Any())
                {
                    ModelState.AddModelError(string.Empty, "You can't register until an admin is registered");
                    return View(customer);
                }

                var existingUser = _DBContext.Customers.FirstOrDefault(u => u.Email == customer.Email);
                if (existingUser == null)
                {
                    _DBContext.Customers.Add(customer);
                    _DBContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "This email already exists");
            }
            _DBContext.SaveChanges();
            return View(customer);
        }

        public async Task< ActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
