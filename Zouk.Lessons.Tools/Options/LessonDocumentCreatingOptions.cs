using System.IO;
using CommandLine;

namespace Zouk.Lessons.Tools.Options
{
	[Verb("create-lesson-document", HelpText = "Создает документ с необходимыми для занятия движениями")]
	public class LessonDocumentCreatingOptions
	{
		[Option("movements-file", Required = true, HelpText = "Xlsx файл с фигурами для связок")]
		public FileInfo MovementsFile { get; set; }

		[Option("warmup-movements-file", Required = true, HelpText = "Xlsx файл с фигурами для разминки")]
		public FileInfo WarmupMovementsFile { get; set; }

		[Option("lesson-number", Required = true, HelpText = "Порядковый номер занятия")]
		public string LessonNumber { get; set; }

		[Option("lesson-filename", Required = true, HelpText = "Имя выходного файла с фигурами для занятия")]
		public string LessonFilename { get; set; }
	}
}