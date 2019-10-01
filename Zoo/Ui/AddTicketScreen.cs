using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Service;
using Zoo.Ui.Utilities;
using Zoo.Ui.ViewModels;

namespace Zoo.Ui
{
    public class AddTicketScreen : IScreen
    {
        private IDictionary<string, Action<NewTicketViewModel, string>> InputLines { get; } =
            new Dictionary<string, Action<NewTicketViewModel, string>>
            {
                {
                    "Guest's Name: ", (model, value) => { model.GuestName = value; }
                },
                {
                    "Guest's Phone: ", (model, value) => { model.GuestPhone = value; }
                },
                {
                    "Guest's Mailing Address: ", (model, value) => { model.GuestMailingAddress = value; }
                },
                {
                    "Date Attending: ", (model, value) => { model.DateAttending = value; }
                },
                {
                    "Card Number: ", (model, value) => { model.CardNumber = value; }
                },
                {
                    "Card Expiration: ", (model, value) => { model.CardExpirationDate = value; }
                },
                {
                    "CVV: ", (model, value) => { model.CardVerificationValue = value; }
                }
            };

        public string Name { get; } = "Add Ticket";

        public ConsoleWrapper ConsoleWrapper { get; set; }

        public ZooService Service { get; set; }

        public async Task Activated()
        {
            foreach (var inputLine in InputLines)
            {
                var withRightAlignment = $"{inputLine.Key,30}";
                await ConsoleWrapper.WriteLineAsync(withRightAlignment);
            }

            var newTicket = new NewTicketViewModel();
            for (var lineNumber = 0; lineNumber < InputLines.Count; lineNumber++)
            {
                ConsoleWrapper.SetCursorPosition(32, lineNumber);
                var value = await ConsoleWrapper.ReadLineAsync();

                var field = InputLines.ElementAt(lineNumber);
                var setFunc = field.Value;

                setFunc(newTicket, value);
            }

            Service.SaveNewTicket(newTicket);
        }
    }
}