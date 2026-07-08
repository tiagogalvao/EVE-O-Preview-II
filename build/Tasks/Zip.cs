using System.Linq;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks
{
	[IsDependentOn(typeof(Build))]
	public class Zip : FrostingTask<Context>
	{
		public override void Run(Context context)
		{
			if (!context.DirectoryExists(Configuration.PublishFolder))
			{
				context.CreateDirectory(Configuration.PublishFolder);
			}

			context.Zip(Configuration.BinFolder, Configuration.PublishFolder + "/EVE-O Preview.zip",
				context.GetFiles(Configuration.BinFolder + "/*")
					.Where(f => f.GetExtension() != ".pdb"));
		}
	}
}