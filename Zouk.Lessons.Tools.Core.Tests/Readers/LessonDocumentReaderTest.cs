using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Newtonsoft.Json;
using NUnit.Framework;
using Testing.Tools.Directory;
using Zouk.Lessons.Tools.Core.Readers;

namespace Zouk.Lessons.Tools.Core.Tests.Readers
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	[UseApprovalSubdirectory(@"TestData\ApprovalFiles")]
	public class LessonDocumentReaderTest
	{
		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void Read(FileInfo lessonFile)
		{
			using(ApprovalResults.ForScenario(lessonFile.Name))
			{
				var reader = new LessonDocumentReader();
				var lesson = reader.Read(lessonFile.FullName);
				var json = JsonConvert.SerializeObject(lesson, Formatting.Indented);
				Approvals.VerifyJson(json);
			}
		}

		private static IEnumerable<TestCaseData> TestCases => InputDataDirectory.GetFiles().Select(file => new TestCaseData(file).SetName(file.Name));

		private static DirectoryInfo InputDataDirectory
		{
			get
			{
				return TestingProjectDirectoryProvider.ZoukLessonsToolsCoreTestsDirectory
					.GetOrCreateSubDirectory("Readers")
					.GetOrCreateSubDirectory("TestData")
					.GetOrCreateSubDirectory("InputData");
			}
		}
	}
}