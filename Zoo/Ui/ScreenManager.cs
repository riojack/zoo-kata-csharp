using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Ui.Utilities;

namespace Zoo.Ui
{
    public class ScreenManager
    {
        public ICollection<IScreen> Screens { get; set; }

        public ConsoleWrapper ConsoleWrapper { get; set; }

        public async Task StartInputOutputLoop()
        {
            var errorToRender = String.Empty;
            while (errorToRender != ScreenCommands.Quit)
            {
                errorToRender = await Foo(errorToRender);
            }
        }

        private async Task<string> Foo(string errorToRender)
        {
            var screenNames = Screens.Select((x, index) => $"{index + 1}. {x.Name}");
            ConsoleWrapper.ClearScreen();

            foreach (var screenName in screenNames)
            {
                await ConsoleWrapper.WriteLineAsync(screenName);
            }

            await ConsoleWrapper.WriteLineAsync("99. Quit");

            if (!String.IsNullOrEmpty(errorToRender))
            {
                await ConsoleWrapper.WriteLineAsync(String.Empty);
                await ConsoleWrapper.WriteLineAsync(errorToRender);
            }

            var selection = await ConsoleWrapper.ReadLineAsync();

            if (selection == "99")
            {
                ConsoleWrapper.ClearScreen();
                return ScreenCommands.Quit;
            }

            var selectionAsNumber = int.Parse(selection) - 1;

            if (selectionAsNumber >= Screens.Count)
            {
                return "Selection out of range.  Please select an option.";
            }

            var elementAt = Screens.ElementAt(selectionAsNumber);

            await elementAt.Activated();

            return String.Empty;
        }
    }

    public static class ScreenCommands
    {
        public const string Quit = "QUIT";
    }
}