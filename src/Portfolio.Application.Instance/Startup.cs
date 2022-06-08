using Amazon.S3;
using Amazon.SimpleEmail;
using LettuceEncrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Portfolio.Application.Instance.Utility;
using Portfolio.Component.Api.Server;
using Portfolio.Component.Api.Server.Controllers;
using Portfolio.Component.Website.Server;
using Portfolio.Component.Website.Server.Areas.Blog.Controllers;
using Portfolio.Component.Website.Server.Areas.Portfolio.Models;
using Portfolio.Component.Website.Server.Services.ContactService;
using Portfolio.Component.Website.Server.Services.PageMetaProvider;
using Portfolio.Component.Website.Server.Services.ViewToStringRenderer;
using Portfolio.Services.Content;
using Portfolio.Services.EmailTickets;
using RPGCore.Packages;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;

namespace Portfolio.Application.Instance;

public class Startup
{
	public IConfiguration Configuration { get; }
	public IWebHostEnvironment Environment { get; }

	public Startup(
		IConfiguration configuration,
		IWebHostEnvironment environment)
	{
		Configuration = configuration;
		Environment = environment;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("api-v1", new OpenApiInfo
			{
				Title = "Portfolio API",
				Version = "v1",
				Description = "An application-facing API."
			});

			options.SwaggerDoc("site-v1", new OpenApiInfo
			{
				Title = "Web Interface",
				Version = "v1",
				Description = "A user-facing web site for my portfolio."
			});

			options.DocInclusionPredicate((docName, apiDesc) =>
			{
				if (docName == "site-v1" && apiDesc.TryGetMethodInfo(out var methodInfo))
				{
					return methodInfo.DeclaringType?.Assembly == typeof(BlogController).Assembly;
				}
				if (docName == "api-v1" && apiDesc.TryGetMethodInfo(out methodInfo))
				{
					return methodInfo.DeclaringType?.Assembly == typeof(ProfileController).Assembly;
				}
				return false;
			});

			options.OrderActionsBy(apiDescription => apiDescription.RelativePath);

			options.TagActionsBy(apiDescription =>
			{
				return new string[] {
					apiDescription.GroupName ?? "default"
				};
			});

			// Set the comments path for the Swagger JSON and UI.
			string xmlFile = $"{typeof(BlogController).Assembly.GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath);

			// Set the comments path for the Swagger JSON and UI.
			xmlFile = $"{typeof(ProfileController).Assembly.GetName().Name}.xml";
			xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath);
		});

		services.AddResponseCompression(options =>
		{
			options.EnableForHttps = true;

			options.Providers.Add<GzipCompressionProvider>();
			options.MimeTypes = ResponseCompressionDefaults.MimeTypes
				.Concat(new[] { "image/svg+xml" });
		});

		services.AddHealthChecks();
		services.AddMvc(options =>
		{
		});

		IExplorer explorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.ContentPath).Result;
		services.AddSingleton(explorer);

		services.AddSingleton<IContentService, LocalContentService>();
		services.AddSingleton<IPageMetadataTransformer<ProjectViewModel>, ProjectViewModelPageMetadataTransformer>();
		services.AddSingleton(new EmailReaderServiceConfiguration()
		{
			Bucket = "anthonymarmont.com-inbound-email"
		});
		services.AddSingleton<EmailReaderService>();

		services.AddScoped<IContactSubmitSink, SaveTicketSubmitSink>();
		services.AddScoped<IContactSubmitSink, ContactNotificationSubmitSink>();

		services.AddScoped<IViewToStringRenderer, RazorViewToStringRenderer>();

		services.AddAWSService<IAmazonSimpleEmailService>();
		services.AddAWSService<IAmazonS3>();

		services.AddPortfolioSiteControllers();

		if (!Environment.IsDevelopment())
		{
			string domainName = Configuration.GetValue<string>("DOMAINNAME");

			services.AddLettuceEncrypt(options =>
			{
				options.AcceptTermsOfService = true;
				options.DomainNames = new string[] { domainName };
				options.EmailAddress = "dev.anthonymarmont@gmail.com";
			})
				.PersistDataToDirectory(new DirectoryInfo("lettuce"), null);
		}
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			options.RoutePrefix = "swagger";
			options.SwaggerEndpoint("/swagger/api-v1/swagger.json", "api-v1");
			options.SwaggerEndpoint("/swagger/site-v1/swagger.json", "site-v1");
		});

		app.UseHealthChecks("/api/health");

		app.UsePortfolioSite("");

		app.UsePortfolioApi("/api");
	}
}
