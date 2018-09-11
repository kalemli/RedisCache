using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
    public class CacheUtil
    {
        private static string Host = "localhost";
        private static int Port = 6379;

        public static void Save(string key, object value)
        {
            using (var client = new RedisClient(Host, Port))
            {
                client.Set(key, value);
            }
        }

        public static T Get<T>(string key)
        {
            using (var client = new RedisClient(Host, Port))
            {
                return client.Get<T>(key);
            }
        }
    }
}
