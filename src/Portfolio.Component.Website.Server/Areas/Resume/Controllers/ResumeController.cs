using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Resume.Models;
using Portfolio.Services.Content;

namespace Portfolio.Component.Website.Server.Areas.Resume.Controllers;

[ApiController]
[Area("Resume")]
[Route("/resume")]
[ApiExplorerSettings(GroupName = "Resume")]
public class ResumeController : Controller
{
	private readonly IContentService contentService;

	public ResumeController(IContentService contentService)
	{
		this.contentService = contentService;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Index([FromQuery] string? company, [FromQuery] string? position)
	{
		return View(new ResumeViewModel(company ?? string.Empty, position ?? string.Empty));
	}
}
