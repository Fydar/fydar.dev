using System;
using System.IO;

namespace Portfolio.Instance.Utility
{
	public static class ContentDirectory
	{
		public static string Path { get; }

		static ContentDirectory()
		{
			string buildStepOutput = "../Portfolio.Pipeline.BuildStep/bin/Debug/net5.0/output/content";

			if (Directory.Exists("content"))
			{
				Path = "content";
			}
			else if (Directory.Exists(buildStepOutput))
			{
				Path = buildStepOutput;
			}
			else
			{
				throw new InvalidOperationException("Failed to locate package data to host");
			}
		}
	}
}
