using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Frosting;

namespace Build.Tasks
{
	public class Restore : FrostingTask<Context>
	{
		public override void Run(Context context)
		{
			context.Information("Restore started...");
			context.DotNetRestore(Configuration.SolutionName);
		}
	}
}
