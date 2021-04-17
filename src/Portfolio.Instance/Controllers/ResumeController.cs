using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Services.ContentService;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Controllers
{
	public class ResumeController : Controller
	{
		private readonly IContentService contentService;

		public ResumeController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[Route("/resume")]
		public IActionResult Index([FromQuery] string company, [FromQuery] string position)
		{
			return View(new ResumeViewModel(company, position));
		}
	}
}
