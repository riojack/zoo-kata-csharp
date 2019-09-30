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
        private AddTicketScreen Screen { get; set; }

        public AddTicketScreenTest()
        {
            MockConsoleWrapper = new Mock<ConsoleWrapper>();

            Screen = new AddTicketScreen {ConsoleWrapper = MockConsoleWrapper.Object};
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
    }
}