using System;
using AlgorithmsDataStructures;

namespace HashTableCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var table = new HashTable(19, 3);
            // Same
            table.Put("Etergerb");
            table.Put("aeqrew");
            table.Put("aeqrew");
            
            table.Put("erhtrnbfnz");
            table.Put("Test");
            table.Put("Aleks");
            table.Put("Rafek");
        }

    }
}