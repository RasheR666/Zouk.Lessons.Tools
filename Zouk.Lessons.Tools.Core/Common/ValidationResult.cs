using System.Collections.Generic;

namespace Zouk.Lessons.Tools.Core.Common
{
	public class ValidationResult
	{
		public List<ValidationResultElement> Elements { get; set; }
	}

	public class ValidationResultElement
	{
		public string Name { get; set; }
		public List<string> MissingMovements { get; set; }
		public List<string> ExtraMovements { get; set; }
	}
}