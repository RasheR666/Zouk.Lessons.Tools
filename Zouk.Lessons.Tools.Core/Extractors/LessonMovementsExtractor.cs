using System.Collections.Generic;
using Excel.Document.Reader.Common;

namespace Zouk.Lessons.Tools.Core.Extractors
{
	public class LessonMovementsExtractor
	{
		public List<string> Extract(ExcelDocument document, string lessonNumber)
		{
			var cells = document.Tables[0].Cells;
			var movementNameColumnIndex = 0;
			var lessonColumnIndex = 0;
			for(var index = 0; index < cells.GetLength(1); index++)
			{
				if(cells[0, index].Value == lessonNumber)
				{
					lessonColumnIndex = index;
					break;
				}
			}

			if(lessonColumnIndex == 0)
				return new List<string>();

			var movementNames = new List<string>(16);

			for(var rowIndex = 3; rowIndex < cells.GetLength(0); rowIndex++)
			{
				if(!string.IsNullOrWhiteSpace(cells[rowIndex, lessonColumnIndex].Value))
					movementNames.Add(cells[rowIndex, movementNameColumnIndex].Value);
			}

			return movementNames;
		}
	}
}