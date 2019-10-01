using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using Zoo.Service;
using Zoo.Ui;
using Zoo.Ui.AddTicket;
using Zoo.Ui.Utilities;

namespace ZooTest.Ui
{
    public class AddTicketScreenTest
    {
        private Mock<ConsoleWrapper> MockConsoleWrapper { get; }

        private Mock<ZooService> MockZooService { get; }

        private Mock<TicketFieldConverter> MockConverter { get; }

        private AddTicketScreen Screen { get; }

        private IEnumerable<string> DisplayNames { get; } = new[] {"Field 1", "Field 2", "Field 3"};

        public AddTicketScreenTest()
        {
            MockConsoleWrapper = new Mock<ConsoleWrapper>();
            MockZooService = new Mock<ZooService>();
            MockConverter = new Mock<TicketFieldConverter>();

            MockConverter.SetupGet(x => x.FieldDisplayNames).Returns(DisplayNames);

            Screen = new AddTicketScreen
            {
                ConsoleWrapper = MockConsoleWrapper.Object,
                Service = MockZooService.Object,
                Converter = MockConverter.Object
            };
        }

        [Fact]
        public void ShouldBeNamed()
        {
            Assert.Equal("Add Ticket", Screen.Name);
        }

        [Fact]
        public async void ShouldWaitForInputAtEachLineRendered()
        {
            MockConsoleWrapper.Setup(x => x.ReadLineAsync()).ReturnsAsync("abc");

            await Screen.Activated();

            for (var lineNumber = 0; lineNumber < DisplayNames.Count(); lineNumber++)
            {
                var topPosition = lineNumber;
                MockConsoleWrapper.Verify(x => x.SetCursorPosition(32, topPosition));
                MockConsoleWrapper.Verify(x => x.ReadLineAsync());
            }
        }

        [Fact]
        public async void ShouldSaveNewTicket()
        {
            var guestName = Guid.NewGuid().ToString();
            var guestPhone = "555-555-5555";
            var guestAddress = "12345 Somewhere USA";

            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync(guestName)
                .ReturnsAsync(guestPhone)
                .ReturnsAsync(guestAddress);

            IDictionary<string, string> actualConvertInputToModelArgument = null;
            MockConverter.Setup(x => x.ConvertInputToModel(It.IsAny<IDictionary<string, string>>()))
                .Callback<IDictionary<string, string>>(x => actualConvertInputToModelArgument = x);

            await Screen.Activated();

            var expectedConvertInputToModelArgument = new Dictionary<string, string>
            {
                {"Field 1", guestName},
                {"Field 2", guestPhone},
                {"Field 3", guestAddress}
            };

            Assert.Equal(expectedConvertInputToModelArgument, actualConvertInputToModelArgument);
        }
    }
}