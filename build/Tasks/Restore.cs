﻿using Cake.Common.Diagnostics;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Restore;
using Cake.Frosting;

namespace Build.Tasks
{
	[IsDependentOn(typeof(Documentation))]
	public class Restore : FrostingTask<Context>
	{
		public override void Run(Context context)
		{
			context.Information("Restore started...");
			context.NuGetRestore(Configuration.SolutionName, new NuGetRestoreSettings { NoCache = true});
		}
	}
}
