using System;
using System.Collections.Generic;
using Zoo.Domain;

namespace Zoo.Persistence
{
    public class TicketingDao
    {
        private IDictionary<string, Ticket> Tickets { get; set; } = new Dictionary<string, Ticket>();

        public void Save(Ticket ticket)
        {
            var id = Guid.NewGuid().ToString();

            ticket.Id = id;

            Tickets.Add(ticket.Id, ticket);
        }

        public Ticket Find(string ticketId)
        {
            Tickets.TryGetValue(ticketId, out var ticket);

            return ticket?.Clone() as Ticket;
        }

        public void Remove(string ticketId)
        {
        }
    }
}