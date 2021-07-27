using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Controllers
{
	[ApiController]
	[Area("Error")]
	[Route("api/error/404")]
	[ApiExplorerSettings(GroupName = "Error")]
	public class Error404Controller : Controller
	{
		public Error404Controller()
		{
		}

		/// <summary>
		/// The website page for 404 errors.
		/// </summary>
		/// <returns>A view representing the page.</returns>
		/// <response code="200">The 404 error page.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index()
		{
			return StatusCode(404);
		}
	}
}
