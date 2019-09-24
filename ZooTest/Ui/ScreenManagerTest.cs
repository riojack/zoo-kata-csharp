using System.Collections.Generic;
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

        private Mock<ConsoleWrapper> MockConsoleWrapper { get; set; }

        private ScreenManager ScreenManager { get; set; }

        public ScreenManagerTest()
        {
            var firstMockScreen = new Mock<IScreen>();
            firstMockScreen.Setup(x => x.Name).Returns(ScreenNameOne);

            SecondMockScreen = new Mock<IScreen>();
            SecondMockScreen.Setup(x => x.Name).Returns(ScreenNameTwo);

            var thirdMockScreen = new Mock<IScreen>();
            thirdMockScreen.Setup(x => x.Name).Returns(ScreenNameThree);

            MockConsoleWrapper = new Mock<ConsoleWrapper>();
            MockConsoleWrapper.Setup(x => x.ReadLineAsync()).ReturnsAsync("2");

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
        public async void ShouldRenderAllScreenNamesToOutputWithPrecedingNumber()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"1. {ScreenNameOne}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"2. {ScreenNameTwo}"), Times.Once);
            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"3. {ScreenNameThree}"), Times.Once);
        }

        [Fact]
        public async void ShouldRenderAnOptionForQuitting()
        {
            await ScreenManager.StartInputOutputLoop();

            MockConsoleWrapper.Verify(x => x.WriteLineAsync($"99. Quit"), Times.Once);
        }

        [Fact]
        public async void ShouldCallActivateOnCorrectScreenWhenUserEntersNumber()
        {
            await ScreenManager.StartInputOutputLoop();

            SecondMockScreen.Verify(x => x.Activated());
        }
    }
}