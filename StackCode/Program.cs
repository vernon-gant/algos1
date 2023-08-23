using System;
using AlgorithmsDataStructures;

namespace StackCode
{
    class Program
    {

        static void Main(string[] args)
        {
            var equation = new Stack<char>();
            equation.Push('=');
            equation.Push('+');
            equation.Push('9');
            equation.Push('*');
            equation.Push('5');
            equation.Push('+');
            equation.Push('2');
            equation.Push('8');
            Console.WriteLine(SpecialTasks.PostfixEquation(equation));
        }

    }
}