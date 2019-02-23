using System.Collections.Generic;
using Zouk.Lessons.Tools.Core.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Printers
{
	public class LessonPrinter
	{
		public void Print(List<string> warmup, List<string> movements, IWriter writer)
		{
			PrintBlock(warmupSourceMovementsHeader, warmup, writer);
			PrintBlock(warmupMovementsSequenceHeader, warmup, writer);

			PrintBlock(sourceMovementsHeader, movements, writer);
			PrintBlock(movementsSequenceHeader, new List<string>(), writer);
		}

		private void PrintBlock(string blockHeader, List<string> movements, IWriter writer)
		{
			writer.WriteLine(blockDelimiter);
			writer.WriteLine(blockHeader);
			writer.WriteLine(blockDelimiter);
			writer.WriteLine();
			foreach(var name in movements)
			{
				writer.WriteLine(name);
			}

			writer.WriteLine();
		}

		private const string blockDelimiter = "-------------------------------------------------------------------------------------------------------------------------------------------------------------------";
		private const string warmupSourceMovementsHeader = "Разминка. Исходные фигуры";
		private const string warmupMovementsSequenceHeader = "Разминка. Последовательность";
		private const string sourceMovementsHeader = "Фигуры";
		private const string movementsSequenceHeader = "Связки";
	}
}