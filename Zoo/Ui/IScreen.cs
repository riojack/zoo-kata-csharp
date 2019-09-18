using System.Threading.Tasks;

namespace Zoo.Ui
{
    public interface IScreen
    {
        string Name { get; }

        Task Activated();
    }
}