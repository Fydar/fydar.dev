using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Portfolio.Instance.Utility;
using Serilog;
using Serilog.Events;
using System;

namespace Portfolio.Instance
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.Console(new LogFormatter())
				// .WriteTo.Async(a => a.File(new LogFormatter(), "log.txt", rollingInterval: RollingInterval.Day))
				.CreateLogger();

			try
			{
				Log.Information("Starting web host");
				BuildWebHost(args)
					.Run();
				return 0;
			}
			catch (Exception exception)
			{
				Log.Fatal(exception, "Host terminated unexpectedly");
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseConfiguration(new ConfigurationBuilder()
					.AddCommandLine(args)
					.Build())
				.UseStartup<Startup>()
				.UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
				.UseSerilog()
				.Build();
		}
	}
}
