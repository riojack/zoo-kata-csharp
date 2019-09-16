using DependencyInjector;
using Xunit;

namespace DependencyInjectorTest {
    public class SimpleContainerTests {
        [Fact]
        public void ShouldStoreStoreAndRetrieveObjectsByType()
        {
            var container = new SimpleContainer();
            var instanceToStore = new FooBarBaz();

            container.Store(instanceToStore);

            var actualInstance = container.FindByType<FooBarBaz>();
            
            Assert.Same(instanceToStore, actualInstance);
        }
    }

    class FooBarBaz {
        
    }
}