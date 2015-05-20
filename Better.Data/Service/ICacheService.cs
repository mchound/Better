using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace Better.Data.Service
{
    public interface ICacheService
    {
        void Add(object item, SeasonKey key);
        T Get<T>(SeasonKey key) where T : class;
    }

    public class DefaultCacheService : ICacheService
    {
        public void Add(object item, SeasonKey key)
        {
            MemoryCache.Default.Add(new CacheItem(key.ToString(), item), null);
        }

        public T Get<T>(SeasonKey key) where T : class
        {
            if(!MemoryCache.Default.Contains(key.ToString())) return default(T);

            return MemoryCache.Default.Get(key.ToString()) as T;
        }
    }

}
