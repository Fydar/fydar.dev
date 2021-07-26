using System;
using System.Text.Json.Serialization;

namespace Portfolio.Api.Models
{
	/// <summary>
	/// A model representing a user.
	/// </summary>
	public class ProfileModel
	{
		/// <summary>
		/// The name of the user.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Pronouns for the user.
		/// </summary>
		[JsonPropertyName("pronouns")]
		public string Pronouns { get; set; } = string.Empty;

		/// <summary>
		/// A collection of social links for this user.
		/// </summary>
		[JsonPropertyName("links")]
		public ProfileLinkModel[] Links { get; set; } = Array.Empty<ProfileLinkModel>();
	}
}
