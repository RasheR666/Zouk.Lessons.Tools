using System.Collections.Generic;
using Zouk.Lessons.Tools.Core.Common;
using Zouk.Lessons.Tools.Core.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Printers
{
	public class ValidationResultPrinter
	{
		public void Print(ValidationResult validationResult, IWriter writer)
		{
			foreach(var validationResultElement in validationResult.Elements)
			{
				Print(validationResultElement, writer);
			}
		}

		private void Print(ValidationResultElement validationResultElement, IWriter writer)
		{
			writer.WriteLine(Constants.blockDelimiter);
			writer.WriteLine(validationResultElement.Name);
			writer.WriteLine();
			Print("Missing movements", validationResultElement.MissingMovements, writer);
			Print("Extra movements", validationResultElement.ExtraMovements, writer);
		}

		private void Print(string name, List<string> movements, IWriter writer)
		{
			writer.WriteLine(name);
			foreach(var missingMovement in movements)
			{
				writer.WriteLine(missingMovement);
			}

			writer.WriteLine();
		}
	}
}