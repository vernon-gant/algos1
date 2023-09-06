using System;
using AlgorithmsDataStructures;

namespace DictionaryCode
{
    internal record Customer(string name, int age, double salary){}
    
    class Program
    {

        static void Main(string[] args)
        {
            var dictionary = new NativeDictionary<Customer>(19);
            dictionary.Put("Bob", new Customer("Bob", 22, 2000.5));
            dictionary.Put("Aleks", new Customer("Aleks", 25, 5500.5));
            dictionary.Put("Martin", new Customer("Martin", 20, 500.5));
            dictionary.Put("Aleks", new Customer("Aleks", 30, 600.5));
            
            Console.WriteLine(dictionary.IsKey("BOOOB"));
        }

    }
}