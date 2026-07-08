namespace Build
{
	static class Configuration
	{
		public const string SolutionName = @"./src/EVE-O-Preview.sln";

		public const string BinFolder = @"./bin/net10.0-windows";
		public const string PublishFolder = @"./publish";
		public const string BuildConfiguration = @"Release";
        public const string BuildToolPath = null; // Set to NULL to let Cake to try to use the default MSBuild instance
    }
}
