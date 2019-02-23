using System.Collections.Generic;

namespace Zouk.Lessons.Tools.Core.Common
{
	public class Lesson
	{
		public List<string> WarmupSourceMovements { get; set; } = new List<string>();
		public List<string> WarmupMovementsSequence { get; set; } = new List<string>();
		public List<string> SourceMovements { get; set; } = new List<string>();
		public List<string> MovementsSequence { get; set; } = new List<string>();
	}
}