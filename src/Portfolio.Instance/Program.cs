using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
				var host = BuildWebHost(args);
				host.Start();

				var addresses = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
				Log.Information($"Web host started listening on {string.Join(", ", addresses)}");

				host.WaitForShutdown();

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
