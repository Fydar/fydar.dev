using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Portfolio.Models;
using Portfolio.Services.Content;

namespace Portfolio.Component.Website.Server.Areas.Blog.Controllers;

[ApiController]
[Area("Blog")]
[Route("/blog")]
[ApiExplorerSettings(GroupName = "Blog")]
public class BlogController : Controller
{
	private readonly IContentService contentService;

	public BlogController(IContentService contentService)
	{
		this.contentService = contentService;
	}

	/// <summary>
	/// The website page for blog posts.
	/// </summary>
	/// <returns>A view representing the page.</returns>
	/// <response code="200">The blog posts page.</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Index()
	{
		return View("Index", new PortfolioIndexViewModel(contentService.Projects));
	}
}
