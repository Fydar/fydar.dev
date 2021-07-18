using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Error.Models;

namespace Portfolio.Site.Areas.Error.Controllers
{
	[Area("Error")]
	public class ErrorController : Controller
	{
		public ErrorController()
		{
		}

		[Route("error/404")]
		public IActionResult Error404()
		{
			return View("Error404", new ErrorViewModel()
			{

			});
		}

		[Route("error/{code:int}")]
		public IActionResult Error(int code)
		{
			return View("ServerError", new ErrorViewModel()
			{

			});
		}
	}
}
