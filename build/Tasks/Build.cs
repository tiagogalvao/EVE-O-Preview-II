using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Frosting;

namespace Build.Tasks
{
	[IsDependentOn(typeof(Restore))]
	public class Build : FrostingTask<Context>
	{
		public override void Run(Context context)
		{
			context.Information("Build started...");
			context.DotNetBuild(Configuration.SolutionName, new DotNetBuildSettings
			{
				Configuration = Configuration.BuildConfiguration,
				NoRestore = true,
			});
		}
	}
}
