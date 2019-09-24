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
            var screenNames = Screens.Select((x, index) => $"{index + 1}. {x.Name}");

            foreach (var screenName in screenNames)
            {
                await ConsoleWrapper.WriteLineAsync(screenName);
            }

            await ConsoleWrapper.WriteLineAsync("99. Quit");

            var selection = await ConsoleWrapper.ReadLineAsync();

            if (selection == "99")
            {
                return;
            }

            var selectionAsNumber = int.Parse(selection) - 1;

            var elementAt = Screens.ElementAt(selectionAsNumber);

            await elementAt.Activated();
        }
    }
}