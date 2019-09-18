using System;
using Xunit;

namespace ZooIntegrationTest
{
    public class ConsoleAppIntegrationTest: IDisposable
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

        [Fact(DisplayName = "Should have a menu with 4 options: List Tickets, Add Ticket, Edit Ticket, Remove Ticket")]
        public async void ShouldHaveAMenuWithOptions()
        {
            var listTicketsOption = await Runner.ReadLineAsync();
            var addTicketOption = await Runner.ReadLineAsync();
            var editTicketOption = await Runner.ReadLineAsync();
            var removeTicketOption = await Runner.ReadLineAsync();
            
            Assert.Equal("1. List Tickets", listTicketsOption);
            Assert.Equal("2. Add Ticket", addTicketOption);
            Assert.Equal("3. Edit Ticket", editTicketOption);
            Assert.Equal("4. Remove Ticket", removeTicketOption);
        }
    }
}