using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers
{
	/// <summary>
	/// A controller for the /api/profile endpoint that provides the consumer with profile details.
	/// </summary>
	[ApiController]
	[Area("Profile")]
	[Route("/api/profile")]
	[ApiExplorerSettings(GroupName = "Profile")]
	public class ProfileController : Controller
	{
		/// <summary>
		/// Retrieves information about the user profile.
		/// </summary>
		/// <returns>A model representing the user profile.</returns>
		/// <response code="200">A model representing the user profile.</response>
		[HttpGet]
		[ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
		public IActionResult Index()
		{
			var profile = new ProfileModel()
			{
				Name = "Anthony Marmont",
				Pronouns = "They/Them",
				Links = new ProfileLinkModel[]
				{
					new ProfileLinkModel()
					{
						Display = "Fydar",
						Site = "github",
						Url = "https://github.com/Fydar"
					},
					new ProfileLinkModel()
					{
						Display = "@Fydarus",
						Site = "twitter",
						Url = "https://twitter.com/Fydarus"
					},
					new ProfileLinkModel()
					{
						Display = "YouTube",
						Site = "youtube",
						Url = "https://www.youtube.com/channel/UCEEI1m2TCso1OGWEHvHS20g"
					},
					new ProfileLinkModel()
					{
						Display = "Fydar",
						Site = "itch-io",
						Url = "https://fydar.itch.io/"
					},
					new ProfileLinkModel()
					{
						Display = "anthonymarmont",
						Site = "linkedin",
						Url = "https://www.linkedin.com/in/anthonymarmont/"
					}
				}
			};

			return Ok(profile);
		}
	}
}
