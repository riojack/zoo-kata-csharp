using System.Collections.Generic;
using Xunit;
using Zoo.Injector;

namespace ZooTest.Injector
{
    public class SimpleInjectorTests
    {
        [Fact]
        public void ShouldCreateAnInstanceAndRetrieveByType()
        {
            var container = new SimpleInjector();

            container.Configure<House>();

            var actualInstance = container.FindByType<House>();

            Assert.NotNull(actualInstance);
        }

        [Fact]
        public void ShouldConfigureInstanceProperties()
        {
            var container = new SimpleInjector();

            container.Configure<House>();

            var actualInstance = container.FindByType<House>();

            Assert.NotNull(actualInstance.Room);
        }

        [Fact]
        public void ShouldStoreInstancesCreatedForProperties()
        {
            var container = new SimpleInjector();

            container.Configure<House>();

            var actualInstance = container.FindByType<Room>();

            Assert.NotNull(actualInstance);
        }

        [Fact]
        public void ShouldCreateOneInstanceAndShareItWithOtherConfiguredObjects()
        {
            var container = new SimpleInjector();
            container.Configure<House>();
            container.Configure<Office>();

            Room houseRoom = container.FindByType<House>().Room;
            Room officeRoom = container.FindByType<Office>().ConferenceRoom;

            Assert.Same(houseRoom, officeRoom);
        }

        [Fact]
        public void ShouldDoNothingIfMoreThanOneAttemptIsMadeToCreateAnInstance()
        {
            var container = new SimpleInjector();
            container.Configure<House>();

            var house = container.FindByType<House>();

            container.Configure<House>();

            var secondHouse = container.FindByType<House>();

            Assert.Same(house, secondHouse);
        }

        [Fact]
        public void ShouldRecursivelyConfigureProperties()
        {
            var container = new SimpleInjector();
            container.Configure<House>();

            Window window = container.FindByType<House>().Room.Window;

            Assert.NotNull(window);
        }

        [Fact]
        public void ShouldReturnNullIfNothingIsFoundForTheGivenType()
        {
            var container = new SimpleInjector();
            var shouldNotExist = container.FindByType<House>();

            Assert.Null(shouldNotExist);
        }

        [Fact]
        public void ShouldStoreAPreBakedObjectWithoutConfiguringItsDependencies()
        {
            var container = new SimpleInjector();
            var house = new House();
            
            container.Store(house);

            var houseFromContainer = container.FindByType<House>();
            
            Assert.NotNull(houseFromContainer);
            Assert.Null(house.Room);
        }

        [Fact]
        public void ShouldStoreAndLoadCollectionTypes()
        {
            var container = new SimpleInjector();
            var houses = new List<House> {new House(), new House(), new House()};
            
            container.Store(houses);

            var housesFromStore = container.FindByType<List<House>>();
            
            Assert.Same(houses, housesFromStore);
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