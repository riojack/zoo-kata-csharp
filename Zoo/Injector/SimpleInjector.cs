using System;
using System.Collections.Generic;

namespace Zoo.Injector
{
    public class SimpleInjector
    {
        private IDictionary<Type, object> InstanceMap { get; } = new Dictionary<Type, object>();

        public T FindByType<T>() where T : class
        {
            InstanceMap.TryGetValue(typeof(T), out var instance);

            return instance as T;
        }

        public void Configure<T>() where T : class
        {
            var typeToInstantiate = typeof(T);
            
            if (!InstanceMap.ContainsKey(typeToInstantiate))
            {
                var instancesToStore = new List<object>();
                ConfigureNewInstance(typeToInstantiate, instancesToStore);

                foreach (var innerInstance in instancesToStore)
                {
                    InstanceMap.Add(innerInstance.GetType(), innerInstance);
                }
            }
        }

        private object ConfigureNewInstance(Type typeToInstantiate, ICollection<object> newInstancesAccumulator)
        {
            var instance = Activator.CreateInstance(typeToInstantiate);
            var members = instance.GetType().GetProperties();

            foreach (var member in members)
            {
                var propertyInfo = instance.GetType().GetProperty(member.Name);
                var propertyType = propertyInfo.PropertyType;

                object innerInstance;

                if (InstanceMap.ContainsKey(propertyType))
                {
                    InstanceMap.TryGetValue(propertyType, out innerInstance);
                }
                else
                {
                    innerInstance = ConfigureNewInstance(propertyType, newInstancesAccumulator);
                }

                propertyInfo.SetMethod.Invoke(instance, new[] {innerInstance});
            }

            newInstancesAccumulator.Add(instance);

            return instance;
        }
    }
}