using System;
using Moq;
using Xunit;
using Zoo.Service;
using Zoo.Ui;
using Zoo.Ui.Utilities;
using Zoo.Ui.ViewModels;

namespace ZooTest.Ui
{
    public class AddTicketScreenTest
    {
        private string[] ExpectedLines { get; } =
        {
            "Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: ",
            "Card Expiration: ", "CVV: "
        };

        private Mock<ConsoleWrapper> MockConsoleWrapper { get; set; }

        private Mock<ZooService> MockZooService { get; set; }
        private AddTicketScreen Screen { get; set; }

        public AddTicketScreenTest()
        {
            MockConsoleWrapper = new Mock<ConsoleWrapper>();
            MockZooService = new Mock<ZooService>();

            Screen = new AddTicketScreen
            {
                ConsoleWrapper = MockConsoleWrapper.Object,
                Service = MockZooService.Object
            };
        }

        [Fact]
        public void ShouldBeNamed()
        {
            Assert.Equal("Add Ticket", Screen.Name);
        }

        [Fact]
        public async void ShouldRenderExpectedInputLinesWithCorrectRightAlignmentOnActivation()
        {
            await Screen.Activated();

            foreach (var expectedLine in ExpectedLines)
            {
                var withRightAlignment = $"{expectedLine,30}";
                MockConsoleWrapper.Verify(x => x.WriteLineAsync(withRightAlignment), Times.Once);
            }
        }

        [Fact]
        public async void ShouldWaitForInputAtEachLineRendered()
        {
            MockConsoleWrapper.Setup(x => x.ReadLineAsync()).ReturnsAsync("abc");

            await Screen.Activated();

            for (var lineNumber = 0; lineNumber < ExpectedLines.Length; lineNumber++)
            {
                MockConsoleWrapper.Verify(x => x.SetCursorPosition(32, lineNumber));
                MockConsoleWrapper.Verify(x => x.ReadLineAsync());
            }
        }

        [Fact]
        public async void ShouldSaveNewTicket()
        {
            var guestName = Guid.NewGuid().ToString();
            var expectedNewTicketViewModel = new NewTicketViewModel
            {
                GuestName = guestName,
                GuestPhone = "555-555-5555",
                GuestMailingAddress = "12345 Somewhere USA",
                DateAttending = "12/12/2100",
                CardNumber = "1234 5678 9100 0000",
                CardExpirationDate = "01/01/2120",
                CardVerificationValue = "123"
            };

            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync(guestName)
                .ReturnsAsync("555-555-5555")
                .ReturnsAsync("12345 Somewhere USA")
                .ReturnsAsync("12/12/2100")
                .ReturnsAsync("1234 5678 9100 0000")
                .ReturnsAsync("01/01/2120")
                .ReturnsAsync("123");

            NewTicketViewModel capturedTicket = null;
            MockZooService.Setup(x => x.SaveNewTicket(It.IsAny<NewTicketViewModel>()))
                .Callback<NewTicketViewModel>(ticketVm => capturedTicket = ticketVm);

            await Screen.Activated();

            Assert.Equal(expectedNewTicketViewModel, capturedTicket);
        }
    }
}