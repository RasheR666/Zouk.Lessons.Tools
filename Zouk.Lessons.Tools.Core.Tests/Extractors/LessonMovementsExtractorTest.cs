using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Excel.Document.Reader.Xlsx;
using Newtonsoft.Json;
using NUnit.Framework;
using Testing.Tools.Directory;
using Zouk.Lessons.Tools.Core.Extractors;

namespace Zouk.Lessons.Tools.Core.Tests.Extractors
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	[UseApprovalSubdirectory(@"TestData\ApprovalFiles")]
	public class LessonMovementsExtractorTest
	{
		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void ExtractFromMovements(string lessonNumber)
		{
			using(ApprovalResults.ForScenario(lessonNumber))
			{
				var xlsxFile = TestDataDirectory.GetOrCreateSubDirectory("InputData")
					.GetFiles()
					.First(x => x.Name == "Movements.xlsx");
				var document = new XlsxDocumentReader().Read(xlsxFile.FullName);
				var movements = new LessonMovementsExtractor().Extract(document, lessonNumber);
				var json = JsonConvert.SerializeObject(movements, Formatting.Indented);
				Approvals.VerifyJson(json);
			}
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				var lessonNumbers = new[] {"5", "13", "19", "27", "35", "51"};
				return lessonNumbers.Select(x => new TestCaseData(x).SetName(x));
			}
		}

		private static DirectoryInfo TestDataDirectory
		{
			get
			{
				return TestingProjectDirectoryProvider.ZoukLessonsToolsCoreTestsDirectory
					.GetOrCreateSubDirectory("Extractors")
					.GetOrCreateSubDirectory("TestData");
			}
		}
	}
}
