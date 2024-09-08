using Cake.Frosting;

namespace Build.Tasks
{
	[IsDependentOn(typeof(Zip))]
	public class Default : FrostingTask<Context>
	{
	}
}