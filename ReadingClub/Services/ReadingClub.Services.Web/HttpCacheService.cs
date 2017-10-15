using System;
using System.Web;
using System.Web.Caching;

using Bytes2you.Validation;

using ReadingClub.Services.Web.Contracts;

namespace ReadingClub.Services.Web
{
    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();

        public T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds)
        {
            Guard.WhenArgument(itemName, nameof(itemName)).IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(durationInSeconds, nameof(durationInSeconds)).IsLessThanOrEqual(0).Throw();

            if (HttpRuntime.Cache[itemName] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[itemName] == null)
                    {
                        var data = getDataFunc();
                        HttpRuntime.Cache.Insert(
                            itemName,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[itemName];
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }
    }
}
