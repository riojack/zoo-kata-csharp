using Xunit;
using Zoo.Ui;

namespace ZooTest.Ui
{
    public class AddTicketScreenTest
    {
        [Fact]
        public void ShouldBeNamed()
        {
            var screen = new AddTicketScreen();

            Assert.Equal("Add Ticket", screen.Name);
        }
    }
}