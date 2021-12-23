using Microsoft.Extensions.DependencyInjection;
using Portfolio.Api.Controllers;
using System.Reflection;

namespace Portfolio.Api
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
