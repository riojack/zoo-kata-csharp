using Xunit;
using Zoo.Domain;
using Zoo.Persistence;

namespace ZooTest.Persistence
{
    public class TicketingDaoTest
    {
        private TicketingDao Dao { get; set; }

        public TicketingDaoTest()
        {
            Dao = new TicketingDao();
        }

        [Fact]
        public void ShouldSetTicketIdAfterSavingTicket()
        {
            var ticket = new Ticket
            {
                AttendanceDate = "07/01/2019"
            };

            Dao.Save(ticket);

            Assert.NotNull(ticket.Id);
        }

        [Fact]
        public void ShouldFindSavedTickets()
        {
            var ticket = new Ticket
            {
                AttendanceDate = "07/23/2019"
            };

            Dao.Save(ticket);

            var persistedTicket = Dao.Find(ticket.Id);

            Assert.Equal(ticket, persistedTicket);
        }
    }
}