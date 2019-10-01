using Xunit;
using Zoo.Ui.AddTicket;

namespace ZooTest.Ui.AddTicket
{
    public class TicketFieldConverterTest
    {
        private string[] ExpectedLines { get; } =
        {
            "Guest's Name: ", "Guest's Phone: ", "Guest's Mailing Address: ", "Date Attending: ", "Card Number: ",
            "Card Expiration: ", "CVV: "
        };

        private TicketFieldConverter Converter { get; }

        public TicketFieldConverterTest()
        {
            Converter = new TicketFieldConverter();
        }

        [Fact]
        public void ShouldProvideExpectedInputLinesWithCorrectRightAlignmentOnActivation()
        {
            Assert.Equal(ExpectedLines, Converter.FieldDisplayNames);
        }
    }
}