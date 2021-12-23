using Microsoft.Extensions.DependencyInjection;
using Portfolio.Component.Api.Server.Controllers;
using System.Reflection;

namespace Portfolio.Component.Api.Server
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddPortfolioApiControllers(this IServiceCollection collection)
		{
			var sampleAssembly = Assembly.GetAssembly(typeof(ProfileController));

			collection
				.AddControllers(options =>
				{
				})
				.AddApplicationPart(sampleAssembly);

			return collection;
		}
	}
}
