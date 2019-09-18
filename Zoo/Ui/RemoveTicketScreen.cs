using System.IO;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class RemoveTicketScreen : IScreen
    {
        public string Name { get; } = "Remove Ticket";

        public TextWriter Out { get; set; }

        public Task Activated()
        {
            throw new System.NotImplementedException();
        }
    }
}