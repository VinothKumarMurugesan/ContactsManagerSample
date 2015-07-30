
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Xen.Common.Services.Caching
{
    /// <summary>
    /// The base cache manager which extends the Entlib caching manager
    /// </summary>
    public class XenCacheManager
    {
        ICacheManager _cacheManager = null;

        public XenCacheManager()
        {
            _cacheManager = CacheFactory.GetCacheManager();
        }

        public XenCacheManager(string cacheName)
        {
            _cacheManager = CacheFactory.GetCacheManager(cacheName);
        }

        public void Add(string key, object value)
        {
            _cacheManager.Add(key, value);
        }

        public object GetValue(string key)
        {
            return _cacheManager.GetData(key);
        }

        public void Remove(string key)
        {
            _cacheManager.Remove(key);
        }

        public void Flush()
        {
            _cacheManager.Flush();            
        }

    }
}
