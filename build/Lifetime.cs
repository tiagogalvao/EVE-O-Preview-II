using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Net;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
	public class Lifetime : FrostingLifetime<Context>
	{
		private void DeleteDirectory(Context context, string directoryName)
		{
			if (!context.DirectoryExists(directoryName))
			{
				return;
			}

			context.DeleteDirectory(directoryName, new DeleteDirectorySettings { Force = true, Recursive = true });
		}


		public override void Setup(Context context, ISetupContext info)
		{
			context.Information("Setting things up...");

			context.Information("Delete bin and publish folders");
			DeleteDirectory(context, Configuration.BinFolder);
			DeleteDirectory(context, Configuration.PublishFolder);
		}

		public override void Teardown(Context context, ITeardownContext info)
		{
			context.Information("Tearing things down...");
			//this.DeleteDirectory(context, ToolsDirectoryName);
		}
	}
}