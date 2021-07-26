using System.Text.Json.Serialization;

namespace Portfolio.Api.Models
{
	/// <summary>
	/// A model representing a link to an external site.
	/// </summary>
	public class ProfileLinkModel
	{
		/// <summary>
		/// The site that this social link represents.
		/// </summary>
		/// <remarks>
		/// Common examples include:
		/// <list type="bullet">
		/// <item><c>github</c></item>
		/// <item><c>itch-io</c></item>
		/// <item><c>twitter</c></item>
		/// <item><c>youtube</c></item>
		/// <item><c>linkedin</c></item>
		/// <item><c>website</c></item>
		/// </list>
		/// </remarks>
		[JsonPropertyName("site")]
		public string Site { get; set; } = string.Empty;

		/// <summary>
		/// How the link should be displayed as text.
		/// </summary>
		[JsonPropertyName("display")]
		public string Display { get; set; } = string.Empty;

		/// <summary>
		/// The url of the website that this link directs to.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;
	}
}
