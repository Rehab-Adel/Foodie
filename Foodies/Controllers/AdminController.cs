using Microsoft.AspNetCore.Mvc;

namespace Foodies.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
