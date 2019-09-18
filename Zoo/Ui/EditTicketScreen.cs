using System.IO;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class EditTicketScreen : IScreen
    {
        public string Name { get; } = "Edit Ticket";

        public TextWriter Out { get; set; }

        public Task Activated()
        {
            throw new System.NotImplementedException();
        }
    }
}