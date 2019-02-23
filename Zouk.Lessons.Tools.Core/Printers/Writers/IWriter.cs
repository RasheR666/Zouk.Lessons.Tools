using System;

namespace Zouk.Lessons.Tools.Core.Printers.Writers
{
	public interface IWriter : IDisposable
	{
		void Write(string text = null);

		void WriteLine(string text = null);
	}
}