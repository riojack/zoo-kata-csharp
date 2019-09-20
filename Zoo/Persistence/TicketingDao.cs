using System;
using Zoo.Domain;

namespace Zoo.Persistence
{
    public class TicketingDao
    {
        public void Save(Ticket ticket)
        {
            var id = Guid.NewGuid().ToString();

            ticket.Id = id;
        }

        public void Find(string ticketId)
        {
        }

        public void Remove(string ticketId)
        {
        }
    }
}