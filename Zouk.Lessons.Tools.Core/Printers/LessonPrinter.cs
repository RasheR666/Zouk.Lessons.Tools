using System.Collections.Generic;
using Zouk.Lessons.Tools.Core.Common;
using Zouk.Lessons.Tools.Core.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Printers
{
	public class LessonPrinter
	{
		public void Print(List<string> warmup, List<string> movements, IWriter writer)
		{
			PrintBlock(Constants.warmupSourceMovementsHeader, warmup, writer);
			PrintBlock(Constants.warmupMovementsSequenceHeader, warmup, writer);

			PrintBlock(Constants.sourceMovementsHeader, movements, writer);
			PrintBlock(Constants.movementsSequenceHeader, new List<string>(), writer);
		}

		private void PrintBlock(string blockHeader, List<string> movements, IWriter writer)
		{
			writer.WriteLine(Constants.blockDelimiter);
			writer.WriteLine(blockHeader);
			writer.WriteLine(Constants.blockDelimiter);
			writer.WriteLine();
			foreach(var name in movements)
			{
				writer.WriteLine(name);
			}

			writer.WriteLine();
		}
	}
}