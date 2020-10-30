using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Controllers
{
	public class ErrorController : Controller
	{
		public ErrorController()
		{
		}

		[Route("error/404")]
		public IActionResult Error404()
		{
			return View(new ErrorViewModel()
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
