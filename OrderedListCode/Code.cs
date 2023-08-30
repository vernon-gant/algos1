using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {

        public T value;

        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }

    }


    public class DummyNode<T> : Node<T>
    {

        public DummyNode(T _value) : base(_value) { }

    }

    public class OrderedList<T>
    {

        public Node<T> head, tail;

        public DummyNode<T> _dummy;

        private bool _ascending;

        public int size;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _dummy = null;
            size = 0;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            int result = 0;
            if (typeof(T) == typeof(String))
            {
                // trim strings and compare them
                var str1 = v1 as string;
                var str2 = v2 as string;
                str1 = str1?.Trim();
                str2 = str2?.Trim();
                result = String.Compare(str1, str2);
                if (result < 0) result = -1;
                else if (result > 0) result = 1;
                else result = 0;
            }
            else
            {
                // use object for type casting and then
                // to int
                var int1 = (int)(object)v1;
                var int2 = (int)(object)v2;
                if (int1 < int2) result = -1;
                else if (int1 > int2) result = 1;
                else result = 0;
            }

            return _ascending ? result : -result;
            // -1 если v1 < v2
            // 0 если v1 == v2
            // +1 если v1 > v2
        }

        public void Add(T value)
        {
            if (value == null) return;

            var newNode = new Node<T>(value);
            size++;
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                return;
            }
            // If value is less then head then we can add immediately add it
            if (Compare(value, head.value) == -1 || Compare(value, head.value) == 0)
            {
                newNode.next = head;
                head.prev = newNode;
                head = newNode;
                return;
            }
            // Or if it is bigger than tail
            if (Compare(value, tail.value) == 1 || Compare(value, head.value) == 0)
            {
                tail.next = newNode;
                newNode.prev = tail;
                tail = newNode;
                return;
            }
            var afterInsert = head;
            for (; Compare(afterInsert.value, value) == -1; afterInsert = afterInsert.next) { }
            newNode.next = afterInsert;
            newNode.prev = afterInsert.prev;
            afterInsert.prev.next = newNode;
            afterInsert.prev = newNode;
        }

        public Node<T> Find(T val)
        {
            // Check if element is out of max min range
            if (Compare(val, head.value) == -1 || Compare(val, tail.value) == 1) return null;

            var temp = head;
            // Iterate until a bigger number is found
            for (; Compare(temp.value, val) == -1; temp = temp.next) { }
            // If it does not equal to the val => not found
            if (Compare(temp.value, val) != 0) return null;

            return temp;
        }

        public void Delete(T val)
        {
            if (val == null) return;
            // Check if element is out of max min range
            if (Compare(val, head.value) == -1 || Compare(val, tail.value) == 1) return;

            if (Compare(val, head.value) == 0)
            {
                
            }
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            head = null;
            tail = null;
        }

        public int Count()
        {
            return size;
        }

        public List<Node<T>> GetAll()
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = head;
            while (node != null)
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }

    }
}