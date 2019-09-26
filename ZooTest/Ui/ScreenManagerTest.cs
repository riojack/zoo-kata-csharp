using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zoo.Ui;
using Zoo.Ui.Utilities;

namespace ZooTest.Ui
{
    public class ScreenManagerTest
    {
        private const string ScreenNameOne = "First Test Screen Name";
        private const string ScreenNameTwo = "Second Test Screen Name";
        private const string ScreenNameThree = "Third Test Screen Name";

        private Mock<IScreen> SecondMockScreen { get; }

        private Mock<ConsoleWrapper> MockConsoleWrapper { get; }

        private ScreenManager ScreenManager { get; }

        public ScreenManagerTest()
        {
            var firstMockScreen = new Mock<IScreen>();
            firstMockScreen.Setup(x => x.Name).Returns(ScreenNameOne);

            SecondMockScreen = new Mock<IScreen>();
            SecondMockScreen.Setup(x => x.Name).Returns(ScreenNameTwo);

            var thirdMockScreen = new Mock<IScreen>();
            thirdMockScreen.Setup(x => x.Name).Returns(ScreenNameThree);

            MockConsoleWrapper = new Mock<ConsoleWrapper>();
            MockConsoleWrapper.Setup(x => x.ReadLineAsync()).ReturnsAsync("99");
            MockConsoleWrapper.Setup(x => x.WriteLineAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            ScreenManager = new ScreenManager
            {
                Screens = new List<IScreen>
                {
                    firstMockScreen.Object,
                    SecondMockScreen.Object,
                    thirdMockScreen.Object
                },
                ConsoleWrapper = MockConsoleWrapper.Object
            };
        }

        [Fact]
        public async void ShouldClearScreenAndRenderMenu()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeastOnce);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Once);
        }

        [Fact]
        public async void ShouldRenderAnOptionForQuitting()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.WriteLineAsync("99. Quit"), Times.Once);
        }

        [Fact]
        public async void ShouldClearScreenBeforeQuitting()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeastOnce);
        }

        [Fact]
        public async void ShouldCallActivateOnCorrectScreenWhenUserEntersNumber()
        {
            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync("2")
                .ReturnsAsync("99");
            SecondMockScreen.Setup(x => x.Activated()).Returns(Task.CompletedTask);

            await ScreenManager.StartInputOutputLoop();

            SecondMockScreen.Verify(x => x.Activated());
        }

        [Fact]
        public async void ShouldRenderErrorMessageAndRenderTheMenuAgainIfNumberEnteredIsTooLarge()
        {
            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync("98324")
                .ReturnsAsync("99");

            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeast(3));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync("Selection out of range.  Please select an option."));
        }

        [Fact]
        public async void ShouldRenderErrorMessageAndRenderTheMenuAgainIfNumberEnteredIsZero()
        {
            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync("0")
                .ReturnsAsync("99");

            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeast(3));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync("Selection out of range.  Please select an option."));
        }

        [Fact]
        public async void ShouldRenderErrorMessageAndRenderTheMenuAgainIfNumberEnteredIsLessThanZero()
        {
            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync("-1213")
                .ReturnsAsync("99");

            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeast(3));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync("Selection out of range.  Please select an option."));
        }

        [Fact]
        public async void ShouldRenderErrorMessageAndRenderTheMenuAgainIfNumberEnteredIsNotANumber()
        {
            MockConsoleWrapper.SetupSequence(x => x.ReadLineAsync())
                .ReturnsAsync("12AlphaBravo")
                .ReturnsAsync("99");

            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.AtLeast(3));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Exactly(2));
            MockConsoleWrapper.Verify(x => x.WriteLineAsync("Selection is invalid.  Please select an option."));
        }
    }
}