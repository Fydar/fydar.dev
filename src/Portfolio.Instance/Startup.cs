using Amazon.S3;
using Amazon.SimpleEmail;
using LettuceEncrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Instance.Services.ContactService;
using Portfolio.Instance.Services.ContentService;
using Portfolio.Instance.Services.PageMetaProvider;
using Portfolio.Instance.Services.ViewRenderer;
using Portfolio.Instance.Utility;
using Portfolio.Services.EmailTickets;
using RPGCore.Packages;
using System.IO;

namespace Portfolio.Instance
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }
		public IExplorer Explorer { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
			Explorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.Path).Result;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddResponseCompression();

			services.AddHealthChecks();
			services.AddControllers();
			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			});

			services.AddSingleton(Explorer);
			services.AddSingleton<IContentService, LocalContentService>();
			services.AddSingleton<IPageMetaTransformer, ProjectPageMetaTransformer>();
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

			app.UseHttpsRedirection();
			app.UseResponseCompression();

			// Exception handling
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseStatusCodePagesWithReExecute("/error/{0}");
			}

			app.UseStaticFiles();
			app.UseStaticContentImages(Explorer);

			app.UseMiddleware<RequestLoggingMiddleware>();

			app.UseMvc();
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Portfolio}/{action=Index}");
			});
		}
	}
}
