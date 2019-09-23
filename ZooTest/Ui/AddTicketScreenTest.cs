using System;
using Moq;
using Xunit;
using Zoo.Ui;
using Zoo.Ui.Utilities;

namespace ZooTest.Ui
{
    public class AddTicketScreenTest
    {
        private Mock<ConsoleWrapper> MockConsoleWrapper { get; set; }

        public AddTicketScreenTest()
        {
            MockConsoleWrapper = new Mock<ConsoleWrapper>();
        }

        [Fact]
        public void ShouldBeNamed()
        {
            var screen = new AddTicketScreen();

            Assert.Equal("Add Ticket", screen.Name);
        }

        [Fact]
        public async void ShouldRenderExpectedInputLinesWithCorrectRightAlignmentOnActivation()
        {
            var expectedLines = new[]
            {
                "Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: "
            };

            var screen = new AddTicketScreen {ConsoleWrapper = MockConsoleWrapper.Object};

            await screen.Activated();

            foreach (var expectedLine in expectedLines)
            {
                var withRightAlignment = $"{expectedLine,30}";
                MockConsoleWrapper.Verify(x => x.WriteLineAsync(withRightAlignment), Times.Once);
            }
        }
    }
}