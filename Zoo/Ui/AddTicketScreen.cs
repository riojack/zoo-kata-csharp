using System.IO;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class AddTicketScreen : IScreen
    {
        public string Name { get; } = "Add Ticket";

        public TextWriter Out { get; set; }

        public Task Activated()
        {
            throw new System.NotImplementedException();
        }
    }
}