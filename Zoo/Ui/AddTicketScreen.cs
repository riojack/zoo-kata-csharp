using System;
using System.Threading.Tasks;
using Zoo.Ui.Utilities;

namespace Zoo.Ui
{
    public class AddTicketScreen : IScreen
    {
        private string[] InputLines { get; } = {"Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: "};

        public string Name { get; } = "Add Ticket";

        public ConsoleWrapper ConsoleWrapper { get; set; }

        public async Task Activated()
        {
            foreach (var inputLine in InputLines)
            {
                await ConsoleWrapper.WriteLineAsync(inputLine);
            }
        }
    }
}