using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {

        public T value;

        public Node<T> next, prev;

        public Node() : this(default) { }

        public Node(T _value)
        {
            value = _value;
        }

    }


    public class DummyNode<T> : Node<T>
    {

        public DummyNode()
        {
            next = this;
            prev = this;
        }

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
            _dummy = new DummyNode<T>();
            size = 0;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            int result;
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
                // use object for type casting and then to int
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
            // If value is less then head then or equal to the smallest
            // element in list then we can add immediately add it or list is empty
            if (_dummy.next is DummyNode<T> ||
                Compare(value, _dummy.next.value) == -1 ||
                Compare(value, _dummy.next.value) == 0)
            {
                newNode.next = _dummy.next;
                _dummy.next.prev = newNode;
                newNode.prev = _dummy;
                _dummy.next = newNode;
                return;
            }
            var beforeInsert = _dummy.next;
            for (;
                 !(beforeInsert.next is DummyNode<T>) && Compare(beforeInsert.next.value, value) == -1;
                 beforeInsert = beforeInsert.next) { }
            newNode.next = beforeInsert.next;
            beforeInsert.next.prev = newNode;
            beforeInsert.next = newNode;
            newNode.prev = beforeInsert;
        }

        public Node<T> Find(T val)
        {
            // Check if element is out of max min range, list is empty
            // or passed element is null
            if (_dummy.next is DummyNode<T> ||
                Compare(val, _dummy.next.value) == -1 ||
                Compare(val, _dummy.prev.value) == 1 ||
                val == null) return null;

            var temp = _dummy.next;
            // Iterate until a bigger number is found
            for (; Compare(temp.value, val) == -1; temp = temp.next) { }
            // If it does not equal to the val => not found
            if (Compare(temp.value, val) != 0) return null;

            return temp;
        }

        public void Delete(T val)
        {
            var toDelete = Find(val);

            if (toDelete == null) return;

            var beforeDelete = toDelete.prev;
            var afterDelete = toDelete.next;
            beforeDelete.next = afterDelete;
            afterDelete.prev = beforeDelete;
            size--;
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            _dummy.next = _dummy;
            _dummy.prev = _dummy;
            size = 0;
        }

        public int Count()
        {
            return size;
        }

        public List<Node<T>> GetAll()
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = _dummy.next;
            while (!(node is DummyNode<T>))
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }

    }
}