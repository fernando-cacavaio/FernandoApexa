using FernandoApexa.Application.Interfaces;

namespace FernandoApexa.Application.Cache
{
    public class MRUCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly int capacity;
        private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> cache;
        private readonly LinkedList<KeyValuePair<TKey, TValue>> mruList;

        public MRUCache(int capacity = 5)
        {
            this.capacity = capacity;
            this.cache = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(capacity);
            this.mruList = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        public TValue Get(TKey key)
        {
            if (cache.ContainsKey(key))
            {
                var node = cache[key];
                mruList.Remove(node);
                mruList.AddFirst(node);
                return node.Value.Value;
            }
            return default(TValue);
        }

        public void Put(TKey key, TValue value)
        {
            if (cache.ContainsKey(key))
            {
                var node = cache[key];
                mruList.Remove(node);
                node.Value = new KeyValuePair<TKey, TValue>(key, value);
                mruList.AddFirst(node);
            }
            else
            {
                if (cache.Count == capacity)
                {
                    var lastNode = mruList.Last;
                    mruList.RemoveLast();
                    cache.Remove(lastNode.Value.Key);
                }
                var newNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
                mruList.AddFirst(newNode);
                cache.Add(key, newNode);
            }
        }

        public void Delete(TKey key)
        {
            if (cache.ContainsKey(key))
            {
                var node = cache[key];
                mruList.Remove(node);
                cache.Remove(key);
            }
        }
    }
}