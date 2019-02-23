using System.IO;
using Zouk.Lessons.Tools.Core.Common;

namespace Zouk.Lessons.Tools.Core.Printers.Writers
{
	public class FileWriter : IWriter
	{
		public FileWriter(string filepath)
		{
			writer = new StreamWriter(filepath, false, Constants.Encoding);
		}

		public void Dispose()
		{
			writer.Dispose();
		}

		public void Write(string text = null)
		{
			writer.Write(text);
		}

		public void WriteLine(string text = null)
		{
			writer.WriteLine(text);
		}

		private readonly StreamWriter writer;
	}
}