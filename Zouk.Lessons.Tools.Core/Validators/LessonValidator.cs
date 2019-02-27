using System.Collections.Generic;
using System.Linq;
using Zouk.Lessons.Tools.Core.Common;

namespace Zouk.Lessons.Tools.Core.Validators
{
	public class LessonValidator
	{
		public ValidationResult Validate(List<string> warmup, List<string> movements, Lesson lesson)
		{
			return new ValidationResult
			{
				Elements = new List<ValidationResultElement>
				{
					Compare(warmup, lesson.WarmupSourceMovements, "warmup.xlsx.txt.source"),
					Compare(warmup, lesson.WarmupMovementsSequence, "warmup.xlsx.txt.sequence"),
					Compare(movements, lesson.SourceMovements, "movements.xlsx.txt.source"),
					Compare(movements, lesson.MovementsSequence, "movements.xlsx.txt.sequence")
				}
			};
		}

		private ValidationResultElement Compare(List<string> source, List<string> current, string name)
		{
			var sourceSet = CreateHashSet(source);
			var currentSet = CreateHashSet(current);

			return new ValidationResultElement
			{
				Name = name,
				MissingMovements = sourceSet.Where(x => !currentSet.Contains(x)).ToList(),
				ExtraMovements = currentSet.Where(x => !sourceSet.Contains(x)).ToList()
			};
		}

		private HashSet<string> CreateHashSet(List<string> list)
		{
			var enumerable = list
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Select(x => x.Trim());
			return new HashSet<string>(enumerable);
		}
	}
}