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
	}
}
