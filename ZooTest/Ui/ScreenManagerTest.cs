using System.Collections.Generic;
using System.IO;
using Moq;
using Xunit;
using Zoo.Ui;

namespace ZooTest.Ui
{
    public class ScreenManagerTest
    {
        private const string ScreenNameOne = "First Test Screen Name";
        private const string ScreenNameTwo = "Second Test Screen Name";
        private const string ScreenNameThree = "Third Test Screen Name";

        private Mock<IScreen> SecondMockScreen { get; }

        private Mock<TextWriter> MockTextWriter { get; set; }

        private Mock<TextReader> MockTextReader { get; set; }

        private ScreenManager ScreenManager { get; set; }

        public ScreenManagerTest()
        {
            var firstMockScreen = new Mock<IScreen>();
            firstMockScreen.Setup(x => x.Name).Returns(ScreenNameOne);

            SecondMockScreen = new Mock<IScreen>();
            SecondMockScreen.Setup(x => x.Name).Returns(ScreenNameTwo);

            var thirdMockScreen = new Mock<IScreen>();
            thirdMockScreen.Setup(x => x.Name).Returns(ScreenNameThree);

            MockTextWriter = new Mock<TextWriter>();
            MockTextReader = new Mock<TextReader>();
            MockTextReader.Setup(x => x.ReadLineAsync()).ReturnsAsync("2");

            ScreenManager = new ScreenManager
            {
                Screens = new List<IScreen>
                {
                    firstMockScreen.Object,
                    SecondMockScreen.Object,
                    thirdMockScreen.Object
                },
                Out = MockTextWriter.Object,
                In = MockTextReader.Object
            };
        }

        [Fact]
        public async void ShouldRenderAllScreenNamesToOutputWithPrecedingNumber()
        {
            await ScreenManager.StartInputOutputLoop();

            MockTextWriter.Verify(x => x.WriteLine($"1. {ScreenNameOne}"), Times.Once);
            MockTextWriter.Verify(x => x.WriteLine($"2. {ScreenNameTwo}"), Times.Once);
            MockTextWriter.Verify(x => x.WriteLine($"3. {ScreenNameThree}"), Times.Once);
        }

        [Fact]
        public async void ShouldCallActivateOnCorrectScreenWhenUserEntersNumber()
        {
            await ScreenManager.StartInputOutputLoop();

            SecondMockScreen.Verify(x => x.Activated());
        }
    }
}