using System.Collections.Generic;
using Xunit;
using Zoo.Ui.AddTicket;
using Zoo.Ui.ViewModels;

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

        [Fact]
        public void ShouldMapValuesStoredByDisplayNameIntoNewTicketViewModel()
        {
            var inputMap = new Dictionary<string, string>
            {
                {
                    "Guest's Name: ", "ABC"
                },
                {
                    "Guest's Phone: ", "123-456-7890"
                },
                {
                    "Guest's Mailing Address: ", "DEF"
                },
                {
                    "Date Attending: ", "12/12/1212"
                },
                {
                    "Card Number: ", "GHI"
                },
                {
                    "Card Expiration: ", "12/12"
                },
                {
                    "CVV: ", "JKLMNOPQRS"
                }
            };

            var actualModel = Converter.ConvertInputToModel(inputMap);

            var expectedModel = new NewTicketViewModel
            {
                GuestName = "ABC",
                GuestPhone = "123-456-7890",
                GuestMailingAddress = "DEF",
                DateAttending = "12/12/1212",
                CardNumber = "GHI",
                CardExpirationDate = "12/12",
                CardVerificationValue = "JKLMNOPQRS"
            };

            Assert.Equal(expectedModel, actualModel);
        }

        [Fact]
        public void ShouldPartiallyBuildNewTicketViewModel()
        {
            var inputMap = new Dictionary<string, string>
            {
                {
                    "Guest's Name: ", "ABC"
                }
            };

            var actualModel = Converter.ConvertInputToModel(inputMap);

            var expectedModel = new NewTicketViewModel
            {
                GuestName = "ABC"
            };

            Assert.Equal(expectedModel, actualModel);
        }

        [Fact]
        public void ShouldStillMapEvenWithIrrelevantDisplayNamesInTheInputMap()
        {
            var inputMap = new Dictionary<string, string>
            {
                {
                    "Guest's Name: ", "ABC"
                },
                {
                    "Shirt Size: ", "XL"
                }
            };

            var actualModel = Converter.ConvertInputToModel(inputMap);

            var expectedModel = new NewTicketViewModel
            {
                GuestName = "ABC"
            };

            Assert.Equal(expectedModel, actualModel);
        }
    }
}