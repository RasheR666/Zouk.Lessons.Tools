using System;
using System.IO;
using System.Linq;
using Zouk.Lessons.Tools.Core.Common;

namespace Zouk.Lessons.Tools.Core.Readers
{
	public class LessonDocumentReader
	{
		public Lesson Read(string filepath)
		{
			var lesson = new Lesson();
			var isHeaderBegun = false;
			var headerName = string.Empty;
			foreach(var line in File.ReadAllLines(filepath, Constants.Encoding).Where(x => !string.IsNullOrWhiteSpace(x)))
			{
				if(line == Constants.blockDelimiter)
				{
					isHeaderBegun = !isHeaderBegun;
					continue;
				}

				if(isHeaderBegun)
				{
					headerName = line;
					continue;
				}

				switch(headerName)
				{
					case Constants.warmupSourceMovementsHeader:
						lesson.WarmupSourceMovements.Add(line);
						break;
					case Constants.warmupMovementsSequenceHeader:
						lesson.WarmupMovementsSequence.Add(line);
						break;
					case Constants.sourceMovementsHeader:
						lesson.SourceMovements.Add(line);
						break;
					case Constants.movementsSequenceHeader:
						lesson.MovementsSequence.Add(line);
						break;
					default:
						throw new ApplicationException($"Unknown header name: '{headerName}'");
				}
			}

			return lesson;
		}
	}
}