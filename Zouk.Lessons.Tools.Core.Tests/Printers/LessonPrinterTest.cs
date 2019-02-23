using System.Collections.Generic;
using System.Text;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Zouk.Lessons.Tools.Core.Printers;
using Zouk.Lessons.Tools.Core.Tests.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Tests.Printers
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	[UseApprovalSubdirectory(@"TestData\ApprovalFiles")]
	public class LessonPrinterTest
	{
		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void Print(List<string> warmupMovements, List<string> movements, string scenarioName)
		{
			using(ApprovalResults.ForScenario(scenarioName))
			{
				var printer = new LessonPrinter();
				var sb = new StringBuilder();
				printer.Print(warmupMovements, movements, new StringBuilderWriter(sb));
				Approvals.Verify(sb.ToString());
			}
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				TestCaseData CreateTestCaseData(List<string> warmupMovements, List<string> movements, string scenarioName)
				{
					return new TestCaseData(warmupMovements, movements, scenarioName).SetName(scenarioName);
				}

				string GetCountName(List<string> list)
				{
					if(list.Count == 0) return "empty";
					if(list.Count == 1) return "one";
					if(list.Count == 3) return "few";
					return "unknown";
				}

				var warmupCases = new[]
				{
					new List<string>(),
					new List<string> {"warming up"},
					new List<string> {"warming up", "big warming up", "good warming up"}
				};

				var movementCases = new[]
				{
					new List<string>(),
					new List<string> {"movements"},
					new List<string> {"movement", "cool movement", "crazy movement"}
				};

				foreach(var warmupCase in warmupCases)
				{
					foreach(var movementCase in movementCases)
					{
						yield return CreateTestCaseData(warmupCase, movementCase, $"{GetCountName(warmupCase)} warmup and {GetCountName(movementCase)} movements");
					}
				}
			}
		}
	}
}