using System;
using System.Collections.Generic;

namespace DependencyInjector
{
    public class SimpleContainer
    {
        private readonly IDictionary<Type, object> instanceMap = new Dictionary<Type, object>();

        public T FindByType<T>() where T : class
        {
            instanceMap.TryGetValue(typeof(T), out var instance);

            return instance as T;
        }

        public void Configure<T>()
        {
            var instance = Activator.CreateInstance<T>();

            instanceMap.Add(instance.GetType(), instance);
        }
    }
}