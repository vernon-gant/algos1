

using System;

namespace HashTableCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var dict = new Dictionary<int, string>(capacity:10);
            dict.Add(1234234324,"zero");
            string result;
            dict.TryGetValue(0, out result);
            dict.Add(1,"one");
            dict.Add(2,"two");
            dict.Add(12,"collision");
        }

    }
}