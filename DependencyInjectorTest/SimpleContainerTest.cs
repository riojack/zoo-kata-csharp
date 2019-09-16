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

            container.Configure<FooBarBaz>();

            var actualInstance = container.FindByType<FooBarBaz>();

            Assert.NotNull(actualInstance);
        }
    }

    class FooBarBaz
    {
    }
}