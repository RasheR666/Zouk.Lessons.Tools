using System.Collections.Generic;
using System.Text;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Zouk.Lessons.Tools.Core.Common;
using Zouk.Lessons.Tools.Core.Printers;
using Zouk.Lessons.Tools.Core.Tests.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Tests.Printers
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	[UseApprovalSubdirectory(@"TestData\ApprovalFiles")]
	public class ValidationResultPrinterTest
	{
		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void Print(ValidationResult validationResult, string scenarioName)
		{
			using(ApprovalResults.ForScenario(scenarioName))
			{
				var printer = new ValidationResultPrinter();
				var sb = new StringBuilder();
				printer.Print(validationResult, new StringBuilderWriter(sb));
				Approvals.Verify(sb.ToString());
			}
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				TestCaseData CreateTestCaseData(ValidationResult validationResult, string scenarioName)
				{
					return new TestCaseData(validationResult, scenarioName).SetName(scenarioName);
				}

				string GetCountName(List<string> list)
				{
					if(list.Count == 0) return "empty";
					if(list.Count == 1) return "one";
					if(list.Count == 3) return "few";
					return "unknown";
				}

				yield return CreateTestCaseData(new ValidationResult(), "no validation elements");

				var missingCases = new[]
				{
					new List<string>(),
					new List<string>() {"miss movement"},
					new List<string>() {"miss movement", "miss cool movement", "miss crazy movement"},
				};

				var extraCases = new[]
				{
					new List<string>(),
					new List<string>() {"extra movement"},
					new List<string>() {"extra movement", "extra cool movement", "extra crazy movement"},
				};

				foreach(var missingMovements in missingCases)
				{
					foreach(var extraMovements in extraCases)
					{
						var element = new ValidationResultElement
						{
							Name = $"{GetCountName(missingMovements)} missing movements and {GetCountName(extraMovements)} extra movements",
							MissingMovements = missingMovements,
							ExtraMovements = extraMovements
						};
						yield return CreateTestCaseData(
							new ValidationResult {Elements = new List<ValidationResultElement> {element}},
							$"validation result with one validation element where is {GetCountName(missingMovements)} missing movements and {GetCountName(extraMovements)} extra movements"
						);
					}
				}

				var testCase = new ValidationResult {Elements = new List<ValidationResultElement>()};
				foreach(var missingMovements in missingCases)
				{
					foreach(var extraMovements in extraCases)
					{
						var element = new ValidationResultElement
						{
							Name = $"{GetCountName(missingMovements)} missing movements and {GetCountName(extraMovements)} extra movements",
							MissingMovements = missingMovements,
							ExtraMovements = extraMovements
						};
						testCase.Elements.Add(element);
					}
				}

				yield return CreateTestCaseData(testCase, "validation result with few validation elements");
			}
		}
	}
}