using System.IO;
using Testing.Tools.Directory;

namespace Zouk.Lessons.Tools.Core.Tests
{
	public static class TestingProjectDirectoryProvider
	{
		public static DirectoryInfo SolutionDirectory { get; } = SolutionDirectoryProvider.Get("Zouk.Lessons.Tools.sln");
		public static DirectoryInfo ZoukLessonsToolsCoreTestsDirectory { get; } = SolutionDirectory.GetOrCreateSubDirectory("Zouk.Lessons.Tools.Core.Tests");
	}
}