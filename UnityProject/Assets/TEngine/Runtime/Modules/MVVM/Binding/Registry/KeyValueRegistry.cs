﻿

using System.Collections.Generic;


namespace Framework.Binding.Registry
{
    
    public class KeyValueRegistry<K,V> : IKeyValueRegistry<K,V>
    {
        private readonly Dictionary<K, V> lookups = new Dictionary<K, V>();

        public virtual V Find(K key)
        {
            V toReturn;
            this.lookups.TryGetValue(key, out toReturn);
            return toReturn;
        }

        public virtual V Find(K key,V defaultValue)
        {
            V toReturn;
            if (this.lookups.TryGetValue(key, out toReturn))
                return toReturn;

            return defaultValue;
        }

        public virtual void Register(K key, V value)
        {
            if (this.lookups.ContainsKey(key))
            {
                TEngine.Log.Warning("The Key({0}) already exists", key);
            }
            this.lookups[key] = value;
        }
    }
}
