using System;
using System.IO;

namespace Portfolio.Instance
{
	public static class ContentDirectory
	{
		public static string Path { get; }

		static ContentDirectory()
		{
			if (Directory.Exists("content"))
			{
				Path = "content";
			}
			else if (Directory.Exists("bin/Debug/netcoreapp3.1/content"))
			{
				Path = "bin/Debug/netcoreapp3.1/content";
			}
			else if (Directory.Exists("bin/Release/netcoreapp3.1/content"))
			{
				Path = "bin/Release/netcoreapp3.1/content";
			}
			else
			{
				throw new InvalidOperationException("Failed to locate package data to host");
			}
		}
	}
}
