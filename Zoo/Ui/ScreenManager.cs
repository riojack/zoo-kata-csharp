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
            while (true)
            {
                ConsoleWrapper.ClearScreen();
                await RenderMenu();
                await RenderError(errorToRender);

                var selection = await ConsoleWrapper.ReadLineAsync();

                if (ShouldQuit(selection))
                {
                    ConsoleWrapper.ClearScreen();
                    break;
                }

                errorToRender = SelectionValidation(selection);

                if (!string.IsNullOrEmpty(errorToRender))
                {
                    continue;
                }

                await RenderSelectedScreen(selection);
            }
        }

        private async Task RenderMenu()
        {
            var screenNames = Screens.Select((x, index) => $"{index + 1}. {x.Name}");

            foreach (var screenName in screenNames)
            {
                await ConsoleWrapper.WriteLineAsync(screenName);
            }

            await ConsoleWrapper.WriteLineAsync("99. Quit");
        }

        private async Task RenderError(string errorToRender)
        {
            if (!String.IsNullOrEmpty(errorToRender))
            {
                await ConsoleWrapper.WriteLineAsync(String.Empty);
                await ConsoleWrapper.WriteLineAsync(errorToRender);
            }
        }

        private bool ShouldQuit(string selection)
        {
            return selection == "99";
        }

        private string SelectionValidation(string selection)
        {
            if (!int.TryParse(selection, out var selectionAsNumber))
            {
                return "Selection is invalid.  Please select an option.";
            }

            if (selectionAsNumber <= 0 || selectionAsNumber >= Screens.Count)
            {
                return "Selection out of range.  Please select an option.";
            }

            return "";
        }

        private async Task RenderSelectedScreen(string selection)
        {
            ConsoleWrapper.ClearScreen();
            int.TryParse(selection, out var selectionAsNumber);

            var indexOfSelection = selectionAsNumber - 1;
            var screenToActivate = Screens.ElementAt(indexOfSelection);

            await screenToActivate.Activated();
        }
    }
}