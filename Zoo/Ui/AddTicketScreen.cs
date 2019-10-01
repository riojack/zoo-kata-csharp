using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Service;
using Zoo.Ui.AddTicket;
using Zoo.Ui.Utilities;

namespace Zoo.Ui
{
    public class AddTicketScreen : IScreen
    {
        public string Name { get; } = "Add Ticket";

        public ConsoleWrapper ConsoleWrapper { get; set; }

        public ZooService Service { get; set; }

        public TicketFieldConverter Converter { get; set; }

        public async Task Activated()
        {
            foreach (var fieldDisplayName in Converter.FieldDisplayNames)
            {
                var withRightAlignment = $"{fieldDisplayName,30}";
                await ConsoleWrapper.WriteLineAsync(withRightAlignment);
            }

            var displayNamesWithValues = new Dictionary<string, string>();
            for (var lineNumber = 0; lineNumber < Converter.FieldDisplayNames.Count(); lineNumber++)
            {
                ConsoleWrapper.SetCursorPosition(32, lineNumber);
                var value = await ConsoleWrapper.ReadLineAsync();
                var field = Converter.FieldDisplayNames.ElementAt(lineNumber);

                displayNamesWithValues.Add(field, value);
            }

            var newTicket = Converter.ConvertInputToModel(displayNamesWithValues);

            Service.SaveNewTicket(newTicket);
        }
    }
}