using System.IO;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class ListTicketsScreen : IScreen
    {
        public string Name { get; } = "List Tickets";

        public TextWriter Out { get; set; }

        public Task Activated()
        {
            throw new System.NotImplementedException();
        }
    }
}