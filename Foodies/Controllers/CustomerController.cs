using Microsoft.AspNetCore.Mvc;

namespace Foodies.Controllers
{
	public class CustomerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
