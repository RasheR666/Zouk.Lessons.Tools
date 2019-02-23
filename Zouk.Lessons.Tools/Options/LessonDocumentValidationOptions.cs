using System.IO;
using CommandLine;

namespace Zouk.Lessons.Tools.Options
{
	[Verb("validate-lesson-document", HelpText = "Проверяет документ с необходимыми для занятия движениями")]
	public class LessonDocumentValidationOptions
	{
		[Option("movements-file", Required = true, HelpText = "Xlsx файл с фигурами для связок")]
		public FileInfo MovementsFile { get; set; }

		[Option("warmup-movements-file", Required = true, HelpText = "Xlsx файл с фигурами для разминки")]
		public FileInfo WarmupMovementsFile { get; set; }

		[Option("lesson-number", Required = true, HelpText = "Порядковый номер занятия")]
		public string LessonNumber { get; set; }

		[Option("lesson-file", Required = true, HelpText = "Файл с фигурами для занятия")]
		public FileInfo LessonFile { get; set; }

		[Option("validation-result-file", Required = true, HelpText = "Файл с результатами проверки")]
		public string ValidationResultFilename { get; set; }
	}
}