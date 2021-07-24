using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Services.ContentService;

namespace Portfolio.Site.Areas.Blog.Controllers
{
	[ApiController]
	[Area("Blog")]
	[Route("/blog/{identifier}")]
	[ApiExplorerSettings(GroupName = "Blog")]
	public class BlogPostController : Controller
	{
		private readonly IContentService contentService;

		public BlogPostController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		/// <summary>
		/// The website page for a blog post.
		/// </summary>
		/// <returns>A view representing the blog post page.</returns>
		/// <response code="200">A blog post page.</response>
		/// <response code="404">When no blog post with the identifier could be found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index(string identifier)
		{
			var blogPost = contentService.GetBlogPost(identifier);
			if (blogPost != null)
			{
				return View("BlogPost", blogPost);
			}

			return NotFound();
		}
	}
}
