using System;
using System.Collections.Generic;

namespace DependencyInjector
{
    public class SimpleContainer
    {
        private IDictionary<Type, object> InstanceMap { get; } = new Dictionary<Type, object>();

        public T FindByType<T>() where T : class
        {
            InstanceMap.TryGetValue(typeof(T), out var instance);

            return instance as T;
        }

        public void Configure<T>() where T : class
        {
            if (!InstanceMap.ContainsKey(typeof(T)))
            {
                var instancesToStore = ConfigureNewInstances<T>();

                foreach (var innerInstance in instancesToStore)
                {
                    InstanceMap.Add(innerInstance.GetType(), innerInstance);
                }
            }
        }

        private IList<object> ConfigureNewInstances<T>() where T : class
        {
            var instancesToStore = new List<object>();
            var instance = Activator.CreateInstance<T>();
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
                    innerInstance = Activator.CreateInstance(propertyInfo.PropertyType);
                    instancesToStore.Add(innerInstance);
                }

                propertyInfo.SetMethod.Invoke(instance, new[] {innerInstance});
            }

            instancesToStore.Add(instance);

            return instancesToStore;
        }
    }
}