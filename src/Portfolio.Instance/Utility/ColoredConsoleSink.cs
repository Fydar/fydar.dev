using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;

namespace Portfolio.Instance.Utility
{
	public class ColoredConsoleSink : ILogEventSink
	{
		private readonly JsonValueFormatter valueFormatter;

		/// <summary>
		/// Construct a <see cref="CompactJsonFormatter"/>, optionally supplying a formatter for
		/// <see cref="LogEventPropertyValue"/>s on the event.
		/// </summary>
		/// <param name="valueFormatter">A value formatter, or null.</param>
		public ColoredConsoleSink(JsonValueFormatter valueFormatter = null)
		{
			this.valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
		}

		public void Emit(LogEvent logEvent)
		{
			if (logEvent == null)
			{
				throw new ArgumentNullException(nameof(logEvent));
			}

			var output = Console.Out;

			lock (output)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				output.Write("[");
				output.Write(logEvent.Timestamp.ToString("HH:mm:ss"));
				output.Write("] ");

				output.Write("[");
				Console.ForegroundColor = LogLevelToColor(logEvent.Level);
				output.Write(AbbreviateLogLevel(logEvent.Level));
				Console.ForegroundColor = ConsoleColor.DarkGray;
				output.Write("] ");

				Console.ForegroundColor = ConsoleColor.Gray;
				string message = logEvent.MessageTemplate.Render(logEvent.Properties);
				output.Write(message);

				if (logEvent.Exception != null)
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					output.Write("\n");
					output.Write(logEvent.Exception.ToString());
				}

				foreach (var property in logEvent.Properties)
				{
					string name = property.Key;
					if (name.Length > 0 && name[0] == '@')
					{
						// Escape first '@' by doubling
						name = '@' + name;
					}

					Console.ForegroundColor = ConsoleColor.DarkGray;
					output.Write("\n - ");
					Console.ForegroundColor = ConsoleColor.Cyan;
					output.Write(name);
					Console.ForegroundColor = ConsoleColor.DarkGray;
					output.Write(": ");

					if (property.Value is ScalarValue scalarValue)
					{
						if (scalarValue.Value is int
							|| scalarValue.Value is uint
							|| scalarValue.Value is byte
							|| scalarValue.Value is sbyte
							|| scalarValue.Value is short
							|| scalarValue.Value is ushort
							|| scalarValue.Value is long
							|| scalarValue.Value is ulong
							|| scalarValue.Value is double
							|| scalarValue.Value is decimal
							|| scalarValue.Value is float)
						{
							Console.ForegroundColor = ConsoleColor.Blue;
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Yellow;
						}
					}
					else if (property.Value is null)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Gray;
					}

					valueFormatter.Format(property.Value, output);
				}
				Console.ResetColor();
				output.Write("\n\n");
			}
		}

		private static ConsoleColor LogLevelToColor(LogEventLevel logLevel)
		{
			switch (logLevel)
			{
				default:
				case LogEventLevel.Verbose:
					return ConsoleColor.DarkGray;

				case LogEventLevel.Debug:
					return ConsoleColor.Gray;

				case LogEventLevel.Information:
					return ConsoleColor.White;

				case LogEventLevel.Warning:
					return ConsoleColor.Yellow;

				case LogEventLevel.Error:
					return ConsoleColor.DarkRed;

				case LogEventLevel.Fatal:
					return ConsoleColor.Red;
			}
		}

		private static string AbbreviateLogLevel(LogEventLevel logLevel)
		{
			switch (logLevel)
			{
				case LogEventLevel.Verbose:
					return "VRB";

				case LogEventLevel.Debug:
					return "DBG";

				case LogEventLevel.Information:
					return "INF";

				case LogEventLevel.Warning:
					return "WRN";

				case LogEventLevel.Error:
					return "ERR";

				case LogEventLevel.Fatal:
					return "FTL";

				default:
					return logLevel.ToString();
			}
		}
	}
}
