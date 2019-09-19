using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class ScreenManager
    {
        public ICollection<IScreen> Screens { get; set; }

        public TextWriter Out { get; set; }
        public TextReader In { get; set; }

        public async Task StartInputOutputLoop()
        {
            var screenNames = Screens.Select((x, index) => $"{index + 1}. {x.Name}");

            foreach (var screenName in screenNames)
            {
                Out.WriteLine(screenName);
            }

            var selection = await In.ReadLineAsync();
            var selectionAsNumber = int.Parse(selection) - 1;

            var elementAt = Screens.ElementAt(selectionAsNumber);

            await elementAt.Activated();
        }
    }
}