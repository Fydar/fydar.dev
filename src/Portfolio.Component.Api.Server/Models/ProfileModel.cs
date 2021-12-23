using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio.Component.Api.Server.Models
{
	/// <summary>
	/// A model representing a user.
	/// </summary>
	public class ProfileModel
	{
		/// <summary>
		/// The name of the user.
		/// </summary>
		[Required]
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Pronouns for the user.
		/// </summary>
		[Required]
		[JsonPropertyName("pronouns")]
		public string Pronouns { get; set; } = string.Empty;

		/// <summary>
		/// A collection of social links for this user.
		/// </summary>
		[Required]
		[JsonPropertyName("links")]
		public ProfileLinkModel[] Links { get; set; } = Array.Empty<ProfileLinkModel>();
	}
}
