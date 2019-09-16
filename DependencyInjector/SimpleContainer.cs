using System;
using System.Collections.Generic;

namespace DependencyInjector
{
    public class SimpleContainer
    {
        private readonly IDictionary<Type, object> instanceMap = new Dictionary<Type, object>();
        
        public void Store(object instanceToStore)
        {
            instanceMap.Add(instanceToStore.GetType(), instanceToStore);
        }

        public T FindByType<T>() where T : class
        {
            instanceMap.TryGetValue(typeof(T), out var instance);
            
            return instance as T;
        }
    }
}