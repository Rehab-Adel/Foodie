using Microsoft.AspNetCore.Mvc;

namespace Foodies.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
    }
}
