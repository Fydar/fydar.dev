using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Portfolio.Component.Website.Server;

public static class IServiceCollectionExtensions
{
	public static IServiceCollection AddPortfolioSiteControllers(this IServiceCollection collection)
	{
		var sampleAssembly = Assembly.GetAssembly(typeof(IServiceCollectionExtensions));

		collection
			.AddControllers(options =>
			{
			})
			.AddViewOptions(options =>
			{
			})
			.AddApplicationPart(sampleAssembly)
			.AddRazorOptions(options =>
			{
				options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{0}.cshtml");
				options.AreaViewLocationFormats.Add("/{0}.cshtml");
			});

		return collection;
	}
}
