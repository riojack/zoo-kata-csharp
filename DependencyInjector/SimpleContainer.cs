using System;
using System.Collections.Generic;
using System.Reflection;

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

        public void Configure<T>()
        {
            var instance = Activator.CreateInstance<T>();
            var members = instance.GetType().GetProperties();

            foreach (var member in members)
            {
                var propertyInfo = instance.GetType().GetProperty(member.Name);

                var innerInstance = Activator.CreateInstance(propertyInfo.PropertyType);
                propertyInfo.SetMethod.Invoke(instance, new[] {innerInstance});
            }

            InstanceMap.Add(instance.GetType(), instance);
        }
    }
}