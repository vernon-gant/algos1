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

        public static int PostfixEquation(Stack<string> equation)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));

            // Use second stack to store numbers before next sign and temporary results
            var tempStack = new Stack<int>();

            while (!equation.IsEmpty())
            {
                var equationOperand = equation.Pop();

                if (equationOperand == "=") break;

                // When operand is just a digit, push it and continue
                if (Int32.TryParse(equationOperand, out var number))
                {
                    tempStack.Push(number);
                    continue;
                }

                if (tempStack.Size() < 2) throw new ArgumentException("Invalid equation");

                int result = tempStack.Pop();

                switch (equationOperand)
                {
                    case "+":
                        result += tempStack.Pop();
                        break;
                    case "-":
                        result = tempStack.Pop() - result;
                        break;
                    case "*":
                        result *= tempStack.Pop();
                        break;
                    case "/":
                        if (result == 0) throw new DivideByZeroException();
                        result = tempStack.Pop() / result;
                        break;
                    default:
                        throw new ArgumentException("Invalid sign in the equation!");
                }

                tempStack.Push(result);
            }

            // If there is no trailing operator which maps last numbers to result => equation is invalid.
            if (tempStack.Size() > 1 || equation.Size() > 0) throw new ArgumentException("Invalid equation");

            return tempStack.Pop();
        }

    }
}