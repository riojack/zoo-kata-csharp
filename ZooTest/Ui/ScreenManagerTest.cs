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
        public async void ShouldRenderAnOptionForQuitting()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.WriteLineAsync("99. Quit"), Times.Once);
        }

        [Fact]
        public async void ShouldClearScreenAndRenderMenu()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.ClearScreen(), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Once);
        }

        [Fact]
        public async void ShouldCallActivateOnCorrectScreenWhenUserEntersNumber()
        {
            MockConsoleWrapper.Setup(x => x.ReadLineAsync()).ReturnsAsync("2");
            SecondMockScreen.Setup(x => x.Activated()).Returns(Task.CompletedTask);

            await ScreenManager.StartInputOutputLoop();

            SecondMockScreen.Verify(x => x.Activated());
        }
    }
}