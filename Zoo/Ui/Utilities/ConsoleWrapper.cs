using System;
using System.Threading.Tasks;

namespace Zoo.Ui.Utilities
{
    public class ConsoleWrapper
    {
        public virtual async Task WriteLineAsync(string line) => await Console.Out.WriteLineAsync(line);

        public virtual async Task<string> ReadLineAsync() => await Console.In.ReadLineAsync();

        public virtual void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public virtual void ClearScreen() => Console.Clear();
    }
}