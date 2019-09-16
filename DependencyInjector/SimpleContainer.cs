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
            var instance = Activator.CreateInstance<T>();
            var members = instance.GetType().GetProperties();

            IList<object> instancesForInstance = new List<object>();

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
                    instancesForInstance.Add(innerInstance);
                }

                propertyInfo.SetMethod.Invoke(instance, new[] {innerInstance});
            }

            foreach (var innerInstance in instancesForInstance)
            {
                InstanceMap.Add(innerInstance.GetType(), innerInstance);
            }

            InstanceMap.Add(instance.GetType(), instance);
        }
    }
}