using System.Collections.Generic;

namespace Zouk.Lessons.Tools.Core.Common
{
	public class ValidationResult
	{
		public List<ValidationResultElement> Elements { get; set; } = new List<ValidationResultElement>();
	}

	public class ValidationResultElement
	{
		public string Name { get; set; }
		public List<string> MissingMovements { get; set; } = new List<string>();
		public List<string> ExtraMovements { get; set; } = new List<string>();
	}
}