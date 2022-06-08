using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Portfolio.Models;
using Portfolio.Services.Content;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Controllers;

[ApiController]
[Area("Portfolio")]
[Route("/")]
[Route("/portfolio")]
[ApiExplorerSettings(GroupName = "Portfolio")]
public class PortfolioController : Controller
{
	private readonly IContentService contentService;

	public PortfolioController(IContentService contentService)
	{
		this.contentService = contentService;
	}

	/// <summary>
	/// The website page for a portfolio.
	/// </summary>
	/// <returns>A view representing the page.</returns>
	/// <response code="200">The portfolio page.</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Index()
	{
		return View("Index", new PortfolioIndexViewModel(contentService.Projects));
	}
}
