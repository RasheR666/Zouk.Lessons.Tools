using System.Collections.Generic;
using System.Linq;
using Zouk.Lessons.Tools.Core.Common;

namespace Zouk.Lessons.Tools.Core.Validators
{
	public class LessonValidator
	{
		public ValidationResult Validate(List<string> warmup, List<string> movements, Lesson lesson)
		{
			var validationElements = new List<ValidationResultElement>
			{
				Compare(warmup, lesson.WarmupSourceMovements, "warmup.xlsx.txt.source"),
				Compare(warmup, lesson.WarmupMovementsSequence, "warmup.xlsx.txt.sequence"),
				Compare(movements, lesson.SourceMovements, "movements.xlsx.txt.source"),
				Compare(movements, lesson.MovementsSequence, "movements.xlsx.txt.sequence")
			};
			return new ValidationResult {Elements = validationElements};
		}

		private ValidationResultElement Compare(List<string> source, List<string> current, string name)
		{
			var sourceSet = new HashSet<string>(source);
			var currentSet = new HashSet<string>(current);

			return new ValidationResultElement
			{
				Name = name,
				MissingMovements = sourceSet.Where(x => !currentSet.Contains(x)).ToList(),
				ExtraMovements = currentSet.Where(x => !sourceSet.Contains(x)).ToList()
			};
		}
	}
}