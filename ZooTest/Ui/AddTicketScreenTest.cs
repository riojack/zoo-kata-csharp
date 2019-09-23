using Moq;
using Xunit;
using Zoo.Ui;
using Zoo.Ui.Utilities;

namespace ZooTest.Ui
{
    public class AddTicketScreenTest
    {
        private string[] ExpectedLines { get; } =
        {
            "Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: "
        };

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
            var screen = new AddTicketScreen {ConsoleWrapper = MockConsoleWrapper.Object};

            await screen.Activated();

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
            var screen = new AddTicketScreen {ConsoleWrapper = MockConsoleWrapper.Object};

            await screen.Activated();

            for (var lineNumber = 0; lineNumber < ExpectedLines.Length; lineNumber++)
            {
                MockConsoleWrapper.Verify(x => x.SetCursorPosition(32, lineNumber));
                MockConsoleWrapper.Verify(x => x.ReadLineAsync());
            }
        }
    }
}