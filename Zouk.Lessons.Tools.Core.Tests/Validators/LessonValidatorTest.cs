using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Newtonsoft.Json;
using NUnit.Framework;
using Zouk.Lessons.Tools.Core.Common;
using Zouk.Lessons.Tools.Core.Validators;

namespace Zouk.Lessons.Tools.Core.Tests.Validators
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	[UseApprovalSubdirectory(@"TestData\ApprovalFiles")]
	public class LessonValidatorTest
	{
		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void Validate(List<string> warmup, List<string> movements, Lesson lesson, string scenarioName)
		{
			using(ApprovalResults.ForScenario(scenarioName))
			{
				var validator = new LessonValidator();
				var validationResult = validator.Validate(warmup, movements, lesson);
				var json = JsonConvert.SerializeObject(validationResult, Formatting.Indented);
				Approvals.VerifyJson(json);
			}
		}

		private static TestCaseData CreateTestCaseData(List<string> warmup, List<string> movements, Lesson lesson, string scenarioName)
		{
			return new TestCaseData(warmup, movements, lesson, scenarioName).SetName(scenarioName);
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				var warmupSourceMovements = new List<string> {"warmup.source.movement", "warmup.source.cool.movement", "warmup.source.crazy.movement"};
				var warmupMovementsSequence = new List<string> {"warmup.sequence.movement", "warmup.sequence.cool.movement", "warmup.sequence.crazy.movement"};
				var sourceMovements = new List<string> {"source.movement", "source.cool.movement", "source.crazy.movement"};
				var movementsSequence = new List<string> {"sequence.movement", "sequence.cool.movement", "sequence.crazy.movement"};

				var warmup = new List<string> {"warmup.source.movement", "warmup.sequence.cool.movement", "warmup.source.crazy.movement"};
				var movements = new List<string> {"source.movement", "source.cool.movement", "sequence.crazy.movement"};
				var empty = new List<string>();

				yield return CreateTestCaseData(
					warmup,
					empty,
					new Lesson {WarmupSourceMovements = warmupSourceMovements},
					"WarmupSourceMovements only"
				);

				yield return CreateTestCaseData(
					warmup,
					empty,
					new Lesson {WarmupMovementsSequence = warmupMovementsSequence},
					"WarmupMovementsSequence only"
				);

				yield return CreateTestCaseData(
					empty,
					movements,
					new Lesson {SourceMovements = sourceMovements},
					"SourceMovements only"
				);

				yield return CreateTestCaseData(
					empty,
					movements,
					new Lesson {MovementsSequence = movementsSequence},
					"MovementsSequence only"
				);

				yield return CreateTestCaseData(
					warmup,
					movements,
					new Lesson
					{
						WarmupSourceMovements = warmupSourceMovements,
						WarmupMovementsSequence = warmupMovementsSequence,
						SourceMovements = sourceMovements,
						MovementsSequence = movementsSequence
					},
					"Each list has error"
				);

				yield return CreateTestCaseData(
					warmup,
					movements,
					new Lesson
					{
						WarmupSourceMovements = warmup,
						WarmupMovementsSequence = warmup,
						SourceMovements = movements,
						MovementsSequence = movements
					},
					"Each list has not error"
				);

				yield return CreateTestCaseData(
					empty,
					empty,
					new Lesson(),
					"Each list is empty"
				);

				yield return CreateTestCaseData(
					warmup,
					movements,
					new Lesson
					{
						WarmupSourceMovements = warmupSourceMovements,
						WarmupMovementsSequence = warmup,
						SourceMovements = sourceMovements,
						MovementsSequence = movements
					},
					"Sources has errors"
				);

				yield return CreateTestCaseData(
					warmup,
					movements,
					new Lesson
					{
						WarmupSourceMovements = warmup,
						WarmupMovementsSequence = warmupMovementsSequence,
						SourceMovements = movements,
						MovementsSequence = movementsSequence
					},
					"Sequences has errors"
				);
			}
		}
	}
}