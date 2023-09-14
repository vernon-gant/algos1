using System;

namespace CacheCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var cache = new NativeCache<string>(8);
            cache.Put("kok","ses");
            cache.Put("kek","ses");
            cache.Put("kak","ses");
            cache.Put("kbk","ses");
            cache.Put("ktk","ses");
            cache.Put("sfregerdgrg","ses");
            cache.Put("ergerg","ses");
            cache.Put("ergergerg","ses");
            cache.Put("cvbbf","ses");
            cache.Put("lihkuihjkh","ses");
            cache.Put("uyuijnnn","ses");
            cache.Get("ktk");
            cache.Get("ktk");
            cache.Get("ktk");
            cache.Get("ktk");
            cache.Get("ktk");
            cache.Get("kak");
            cache.Get("kak");
            cache.Get("kbk");
            cache.Get("kbk");
            cache.Get("kbk");
            cache.Get("sfregerdgrg");
            cache.Get("ergerg");
            cache.Get("ergergerg");
            cache.Get("cvbbf");
            cache.Get("lihkuihjkh");
            cache.Get("uyuijnnn");
            cache.Put("kokozavr","test");
        }

    }
}