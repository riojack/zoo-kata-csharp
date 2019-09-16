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

        public void Configure<T>()
        {
            var instance = Activator.CreateInstance<T>();

            InstanceMap.Add(instance.GetType(), instance);
        }
    }
}