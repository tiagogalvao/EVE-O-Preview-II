﻿using Cake.Common.Diagnostics;
using Cake.Common.Tools.MSBuild;
using Cake.Frosting;

namespace Build.Tasks
{
	[IsDependentOn(typeof(Restore))]
	public class Build : FrostingTask<Context>
	{
		public override void Run(Context context)
		{
			context.Information("Build started...");
			context.MSBuild(Configuration.SolutionName, settings =>
			{
				settings.Configuration = Configuration.BuildConfiguration;
				settings.ToolVersion = MSBuildToolVersion.Default;

				if (!string.IsNullOrEmpty(Configuration.BuildToolPath))
				{
					settings.ToolPath = Configuration.BuildToolPath;
				}
			});
		}
	}
}
