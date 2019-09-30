using System.Threading.Tasks;
using Zoo.Service;
using Zoo.Ui.Utilities;
using Zoo.Ui.ViewModels;

namespace Zoo.Ui
{
    public class AddTicketScreen : IScreen
    {
        private string[] InputLines { get; } =
        {
            "Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: ",
            "Card Expiration: ", "CVV: "
        };

        public string Name { get; } = "Add Ticket";

        public ConsoleWrapper ConsoleWrapper { get; set; }

        public ZooService Service { get; set; }

        public async Task Activated()
        {
            foreach (var inputLine in InputLines)
            {
                var withRightAlignment = $"{inputLine,30}";
                await ConsoleWrapper.WriteLineAsync(withRightAlignment);
            }

            for (var lineNumber = 0; lineNumber < InputLines.Length; lineNumber++)
            {
                ConsoleWrapper.SetCursorPosition(32, lineNumber);
                await ConsoleWrapper.ReadLineAsync();
            }

            Service.SaveNewTicket(new NewTicketViewModel
            {
                GuestName = "Mr. Guest Name",
                GuestPhone = "555-555-5555",
                GuestMailingAddress = "12345 Somewhere USA",
                DateAttending = "12/12/2100",
                CardNumber = "1234 5678 9100 0000",
                CardExpirationDate = "01/01/2120",
                CardVerificationValue = "123"
            });

        }
    }
}