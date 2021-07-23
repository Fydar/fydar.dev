using Amazon.S3;
using Amazon.SimpleEmail;
using LettuceEncrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Instance.Utility;
using Portfolio.Services.EmailTickets;
using Portfolio.Site;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContactService;
using Portfolio.Site.Services.ContentService;
using Portfolio.Site.Services.PageMetaProvider;
using Portfolio.Site.Services.ViewToStringRenderer;
using RPGCore.Packages;
using System.IO;

namespace Portfolio.Instance
{
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
			services.AddResponseCompression();

			services.AddHealthChecks();
			services.AddMvc(options =>
			{
			});

			IExplorer explorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.Path).Result;
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
			app.UseHealthChecks("/api/health");

			app.UsePortfolioSite("");
		}
	}
}
