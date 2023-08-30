using System;
using AlgorithmsDataStructures;

namespace OrderedListCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var orderedList = new OrderedList<int>(false);
            orderedList.Add(1);
            orderedList.Add(21);
            orderedList.Add(1213);

            orderedList.GetAll().ForEach((item) => Console.WriteLine(item.value));
        }

    }
}