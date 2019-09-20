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

        [Fact]
        public void ShouldReturnNullIfTicketIsNotFound()
        {
            var persistedTicket = Dao.Find("i-dont-exist");

            Assert.Null(persistedTicket);
        }

        [Fact]
        public void ShouldFindReturnACopyOfTheTicket()
        {
            var ticket = new Ticket
            {
                AttendanceDate = "07/20/2019"
            };

            Dao.Save(ticket);

            var persistedTicket = Dao.Find(ticket.Id);

            Assert.NotSame(ticket, persistedTicket);
        }

        [Fact]
        public void ShouldFindAndReturnACopyOfTheTicketForEachFindOfTheSameTicketId()
        {
            var ticket = new Ticket
            {
                AttendanceDate = "07/20/2019"
            };

            Dao.Save(ticket);

            var ticketFromFirstFindCall = Dao.Find(ticket.Id);
            var ticketFromSecondFindCall = Dao.Find(ticket.Id);

            Assert.NotSame(ticketFromFirstFindCall, ticketFromSecondFindCall);
        }

        [Fact]
        public void ShouldRemoveATicketById()
        {
            var ticket = new Ticket
            {
                AttendanceDate = "07/20/2019"
            };

            Dao.Save(ticket);

            Dao.Remove(ticket.Id);

            var deletedTicket = Dao.Find(ticket.Id);

            Assert.Null(deletedTicket);
        }
    }
}