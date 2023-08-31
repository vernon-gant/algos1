using System;
using AlgorithmsDataStructures;

namespace OrderedListCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var orderedList = new OrderedList<int>(true);
            orderedList.Add(1);
            orderedList.Add(21);
            orderedList.Add(1213);
            orderedList.Add(23);
            orderedList.Add(23);
            orderedList.Add(23);
            orderedList.Add(200);
            
            orderedList.Delete(23);
            orderedList.Delete(23);
            orderedList.Delete(23);
            orderedList.Delete(23);
            
            Console.WriteLine(orderedList.Count());

            orderedList.GetAll().ForEach((item) => Console.WriteLine(item.value));
        }

    }
}