using System.Collections.Generic;
using System.IO;
using Moq;
using Xunit;
using Zoo.Ui;

namespace ZooTest.Ui
{
    public class ScreenManagerTest
    {
        private const string ScreenName = "Test Screen 1";
        private Mock<IScreen> MockScreen { get; set; }

        private Mock<TextWriter> MockTextWriter { get; set; }

        private Mock<TextReader> MockTextReader { get; set; }

        private ScreenManager ScreenManager { get; set; }

        public ScreenManagerTest()
        {
            MockScreen = new Mock<IScreen>();
            MockScreen.Setup(x => x.Name).Returns(ScreenName);

            MockTextWriter = new Mock<TextWriter>();
            MockTextReader = new Mock<TextReader>();

            ScreenManager = new ScreenManager
            {
                Screens = new List<IScreen> {MockScreen.Object},
                Out = MockTextWriter.Object,
                In = MockTextReader.Object
            };
        }

        [Fact]
        public void ShouldRenderAllScreenNamesToOutput()
        {
            ScreenManager.StartInputOutputLoop();

            MockTextWriter.Verify(x => x.WriteLine(ScreenName), Times.Once);
        }
    }
}