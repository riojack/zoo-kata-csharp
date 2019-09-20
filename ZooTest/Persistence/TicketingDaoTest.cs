using Xunit;
using Zoo.Domain;
using Zoo.Persistence;

namespace ZooTest.Persistence
{
    public class TicketingDaoTest
    {
        [Fact]
        public void ShouldSetTicketIdAfterSavingTicket()
        {
            var dao = new TicketingDao();
            var ticket = new Ticket
            {
                AttendanceDate = "07/01/2019"
            };

            dao.Save(ticket);

            Assert.NotNull(ticket.Id);
        }
    }
}