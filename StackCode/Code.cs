using System.Diagnostics.Contracts;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {

        public T _data;

        public Node<T> _next;

        public Node(T data)
        {
            _data = data;
        }

    }

    public class Stack<T>
    {

        public Node<T> _head;

        public Stack()
        {
            _head = null;
        }

        public int Size()
        {
            var result = 0;
            var tempNode = _head;

            while (tempNode != null)
            {
                result++;
                tempNode = tempNode._next;
            }

            return result;
        }

        public T Pop()
        {
            // ваш код
            if (_head == null) return default;

            var toReturn = _head;
            _head = _head._next;

            return toReturn._data;
        }

        public void Push(T val)
        {
            if (val == null) return;

            var newNode = new Node<T>(val)
            {
                _next = _head
            };
            _head = newNode;
        }

        public T Peek()
        {
            if (_head == null) return default;

            return _head._data;
        }

        public bool IsEmpty()
        {
            return _head == null;
        }

    }
}