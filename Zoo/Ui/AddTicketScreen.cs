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
            foreach (var inputLine in Converter.FieldDisplayText)
            {
                var withRightAlignment = $"{inputLine,30}";
                await ConsoleWrapper.WriteLineAsync(withRightAlignment);
            }

            var fieldValues = new Dictionary<string, string>();
            for (var lineNumber = 0; lineNumber < Converter.FieldDisplayText.Count(); lineNumber++)
            {
                ConsoleWrapper.SetCursorPosition(32, lineNumber);
                var value = await ConsoleWrapper.ReadLineAsync();
                var field = Converter.FieldDisplayText.ElementAt(lineNumber);

                fieldValues.Add(field, value);
            }

            var newTicket = Converter.ConvertInputToModel(fieldValues);

            Service.SaveNewTicket(newTicket);
        }
    }
}