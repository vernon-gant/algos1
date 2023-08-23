using System;
using AlgorithmsDataStructures;

namespace StackCode
{
    public class SpecialTasks
    {

        public static bool ValidBracketString(String brackets)
        {
            var stack = new Stack<char>();

            foreach (var bracket in brackets)
            {
                if (bracket == '(') stack.Push(bracket);
                else
                {
                    if (stack.Size() == 0) return false;

                    stack.Pop();
                }
            }

            return stack.Size() == 0;
        }

        public static int PostfixEquation(Stack<char> equation)
        {
            var tempStack = new Stack<int>();

            while (!equation.IsEmpty())
            {
                var equationPart = equation.Pop();

                if (equationPart == '=') break;

                if (Char.IsDigit(equationPart)) tempStack.Push(equationPart - 48);
                else
                {
                    int result = equationPart is '*' or '/' ? 1 : 0;
                    var mapperFunction = GetOperation(equationPart);

                    while (!tempStack.IsEmpty())
                    {
                        result = mapperFunction(result, tempStack.Pop());
                    }

                    tempStack.Push(result);
                }
            }

            return tempStack.Pop();
        }

        private delegate int Operation(int a, int b);

        private static Operation GetOperation(char sign)
        {
            switch (sign)
            {
                case '+':
                    return (a, b) => a + b;
                case '-':
                    return (a, b) => a - b;
                case '*':
                    return (a, b) => a * b;
                case '/':
                    return (a, b) => a / b;
                default:
                    throw new ArgumentException("Invalid operation sign.");
            }
        }

    }
}