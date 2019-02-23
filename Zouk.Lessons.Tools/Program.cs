using CommandLine;
using Excel.Document.Reader.Xlsx;
using Zouk.Lessons.Tools.Core.Extractors;
using Zouk.Lessons.Tools.Core.Printers;
using Zouk.Lessons.Tools.Core.Printers.Writers;
using Zouk.Lessons.Tools.Core.Readers;
using Zouk.Lessons.Tools.Core.Validators;
using Zouk.Lessons.Tools.Options;

namespace Zouk.Lessons.Tools
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Parser.Default.ParseArguments<LessonDocumentCreatingOptions, LessonDocumentValidationOptions>(args)
				.WithParsed<LessonDocumentCreatingOptions>(CreateLesson)
				.WithParsed<LessonDocumentValidationOptions>(ValidateLesson);
		}

		private static void CreateLesson(LessonDocumentCreatingOptions options)
		{
			var excelDocumentReader = new XlsxDocumentReader();
			var lessonExtractor = new LessonMovementsExtractor();

			var movementsDocument = excelDocumentReader.Read(options.MovementsFile.FullName);
			var warmupMovementsDocument = excelDocumentReader.Read(options.WarmupMovementsFile.FullName);

			var movements = lessonExtractor.Extract(movementsDocument, options.LessonNumber);
			var warmupMovements = lessonExtractor.Extract(warmupMovementsDocument, options.LessonNumber);

			using(var writer = new FileWriter(options.LessonFilename))
			{
				new LessonPrinter().Print(warmupMovements, movements, writer);
			}
		}

		private static void ValidateLesson(LessonDocumentValidationOptions options)
		{
			var excelDocumentReader = new XlsxDocumentReader();
			var lessonExtractor = new LessonMovementsExtractor();

			var movementsDocument = excelDocumentReader.Read(options.MovementsFile.FullName);
			var warmupMovementsDocument = excelDocumentReader.Read(options.WarmupMovementsFile.FullName);

			var movements = lessonExtractor.Extract(movementsDocument, options.LessonNumber);
			var warmupMovements = lessonExtractor.Extract(warmupMovementsDocument, options.LessonNumber);

			var lesson = new LessonDocumentReader().Read(options.LessonFile.FullName);
			var validationResult = new LessonValidator().Validate(warmupMovements, movements, lesson);

			using(var writer = new FileWriter(options.ValidationResultFilename))
			{
				new ValidationResultPrinter().Print(validationResult, writer);
			}
		}
	}
}