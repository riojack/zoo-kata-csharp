using DependencyInjector;
using Xunit;

namespace DependencyInjectorTest
{
    public class SimpleContainerTests
    {
        [Fact]
        public void ShouldCreateAnInstanceAndRetrieveByType()
        {
            var container = new SimpleContainer();

            container.Configure<House>();

            var actualInstance = container.FindByType<House>();

            Assert.NotNull(actualInstance);
        }

        [Fact]
        public void ShouldConfigureInstanceProperties()
        {
            var container = new SimpleContainer();

            container.Configure<House>();

            var actualInstance = container.FindByType<House>();
            
            Assert.NotNull(actualInstance.Room);
        }
    }

    class House
    {
        public Room Room { get; set; }
    }

    class Room
    {
        public Window Window { get; set; }
    }

    class Window
    {
    }
}