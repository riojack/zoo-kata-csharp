using System;
using System.Collections.Generic;
using System.Linq;
using Zoo.Ui.ViewModels;

namespace Zoo.Ui.AddTicket
{
    public class TicketFieldConverter
    {
        private readonly IDictionary<string, Action<NewTicketViewModel, string>> _conversionMap =
            new Dictionary<string, Action<NewTicketViewModel, string>>
            {
                {
                    "Guest's Name: ",
                    (model, value) => { model.GuestName = value; }
                },
                {
                    "Guest's Phone: ",
                    (model, value) => { model.GuestPhone = value; }
                },
                {
                    "Guest's Mailing Address: ",
                    (model, value) => { model.GuestMailingAddress = value; }
                },
                {
                    "Date Attending: ",
                    (model, value) => { model.DateAttending = value; }
                },
                {
                    "Card Number: ",
                    (model, value) => { model.CardNumber = value; }
                },
                {
                    "Card Expiration: ",
                    (model, value) => { model.CardExpirationDate = value; }
                },
                {
                    "CVV: ",
                    (model, value) => { model.CardVerificationValue = value; }
                }
            };

        public virtual IEnumerable<string> FieldDisplayNames => _conversionMap.Keys;

        public virtual NewTicketViewModel ConvertInputToModel(IDictionary<string, string> displayNamesWithValues)
        {
            var model = new NewTicketViewModel();

            var cleanDisplayNamesWithValues = displayNamesWithValues
                .Where(x => FieldDisplayNames.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var (displayName, value) in cleanDisplayNamesWithValues)
            {
                var mutatorFunc = _conversionMap[displayName];
                mutatorFunc(model, value);
            }

            return model;
        }
    }
}