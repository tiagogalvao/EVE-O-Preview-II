﻿namespace Build
{
	static class Configuration
	{
		public const string SolutionName = @"./src/EVE-O-Preview.sln";

		public const string BinFolder = @"./bin/net8.0-windows";
		public const string ToolsFolder = @"./tools";
		public const string PublishFolder = @"./publish";
		public const string BuildConfiguration = @"Release";

        public const string BuildToolPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe"; // Set to NULL to let Cake to try to use the default MSBuild instance
    }
}
