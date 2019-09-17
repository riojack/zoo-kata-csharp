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

        [Fact]
        public void ShouldStoreInstancesCreatedForProperties()
        {
            var container = new SimpleContainer();

            container.Configure<House>();

            var actualInstance = container.FindByType<Room>();

            Assert.NotNull(actualInstance);
        }

        [Fact]
        public void ShouldCreateOneInstanceAndShareItWithOtherConfiguredObjects()
        {
            var container = new SimpleContainer();
            container.Configure<House>();
            container.Configure<Office>();

            Room houseRoom = container.FindByType<House>().Room;
            Room officeRoom = container.FindByType<Office>().ConferenceRoom;

            Assert.Same(houseRoom, officeRoom);
        }

        [Fact]
        public void ShouldDoNothingIfMoreThanOneAttemptIsMadeToCreateAnInstance()
        {
            var container = new SimpleContainer();
            container.Configure<House>();

            var house = container.FindByType<House>();

            container.Configure<House>();

            var secondHouse = container.FindByType<House>();

            Assert.Same(house, secondHouse);
        }
        
        [Fact]
        public void ShouldRecursivelyConfigureProperties()
        {
            var container = new SimpleContainer();
            container.Configure<House>();

            Window window = container.FindByType<House>().Room.Window;

            Assert.NotNull(window);
        }
    }

    class House
    {
        public Room Room { get; set; }
    }

    class Office
    {
        public Room ConferenceRoom { get; set; }
    }

    class Room
    {
        public Window Window { get; set; }
    }

    class Window
    {
    }
}