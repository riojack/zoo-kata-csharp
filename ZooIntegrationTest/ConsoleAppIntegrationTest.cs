using System;
using System.Threading;
using Xunit;

namespace ZooIntegrationTest
{
    public class ConsoleAppIntegrationTest : IDisposable
    {
        private ZooConsoleRunner Runner { get; }

        public ConsoleAppIntegrationTest()
        {
            Runner = new ZooConsoleRunner();
            Runner.StartZooConsoleApp();
        }

        public void Dispose()
        {
            Runner.KillZooConsoleApp();
        }

        [Fact(DisplayName =
            "Should have a menu with 5 options: List Tickets, Add Ticket, Edit Ticket, Remove Ticket, and Quit", Timeout = 5000)]
        public async void ShouldHaveAMenuWithOptions()
        {
            var listTicketsOption = await Runner.ReadLineAsync();
            var addTicketOption = await Runner.ReadLineAsync();
            var editTicketOption = await Runner.ReadLineAsync();
            var removeTicketOption = await Runner.ReadLineAsync();
            var quitOption = await Runner.ReadLineAsync();

            Assert.Equal("1. List Tickets", listTicketsOption);
            Assert.Equal("2. Add Ticket", addTicketOption);
            Assert.Equal("3. Edit Ticket", editTicketOption);
            Assert.Equal("4. Remove Ticket", removeTicketOption);
            Assert.Equal("99. Quit", quitOption);

            await Runner.WriteLineAsync("99");
        }

        [Fact(DisplayName = "Should have fields for entering a new ticket after selecting \"Add Ticket\" menu option")]
        public async void ShouldAddTickets()
        {
            await Runner.ReadLineAsync();
            await Runner.ReadLineAsync();
            await Runner.ReadLineAsync();
            await Runner.ReadLineAsync();
            await Runner.ReadLineAsync();

            await Runner.WriteLineAsync("2");

            var guestsNameField = await Runner.ReadLineAsync();
            Assert.Contains("Guest's Name: ", guestsNameField);
        }
    }
}