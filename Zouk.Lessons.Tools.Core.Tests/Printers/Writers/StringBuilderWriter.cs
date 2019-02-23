using System.Text;
using Zouk.Lessons.Tools.Core.Printers.Writers;

namespace Zouk.Lessons.Tools.Core.Tests.Printers.Writers
{
	public class StringBuilderWriter : IWriter
	{
		public StringBuilderWriter(StringBuilder stringBuilder)
		{
			this.stringBuilder = stringBuilder;
		}

		public void Dispose()
		{
		}

		public void Write(string text = null)
		{
			stringBuilder.Append(text);
		}

		public void WriteLine(string text = null)
		{
			stringBuilder.AppendLine(text);
		}

		private readonly StringBuilder stringBuilder;
	}
}