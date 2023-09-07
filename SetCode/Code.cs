using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public class HashTable
    {

        public int size;

        public int step;

        public int counter;

        public string[] slots;

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            counter = 0;
            slots = new string[size];
            for (int i = 0; i < size; i++) slots[i] = null;
        }

        public int HashFun(string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                result += (value[i] * (int)Math.Pow(26, value.Length - i)) % slots.Length;
            }
            return result % slots.Length;
        }

        public int SeekSlot(string value)
        {
            int idx = HashFun(value);
            while (slots[idx] != null)
            {
                if (slots[idx] == value) return -1;

                idx = (idx + step) % slots.Length;
            }
            return idx;
        }

        public int Put(string value)
        {
            if (counter == size || value == null) return -1;

            int idx = SeekSlot(value);

            if (idx == -1) return idx;

            slots[idx] = value;
            counter++;

            return idx;
        }

        public int Find(string value)
        {
            if (value == null || counter == 0) return -1;

            int slowIdx = HashFun(value);
            int fastIdx = (slowIdx + step) % slots.Length;
            while (slots[slowIdx] != value && slowIdx != fastIdx)
            {
                slowIdx = (slowIdx + step) % slots.Length;
                fastIdx = ((fastIdx + step) % slots.Length + step) % slots.Length;
            }

            return slots[slowIdx] == value ? slowIdx : -1;
        }

    }

    // наследуйте этот класс от HashTable
    // или расширьте его методами из HashTable
    public class PowerSet<T> : HashTable
    {

        public PowerSet() : base(20000, 1) { }

        public int Size()
        {
            return counter;
        }

        public void Put(T value)
        {
            Put(value as string);
        }

        public bool Get(T value)
        {
            return Find(value as string) != -1;
        }

        public bool Remove(T value)
        {
            int idx = Find(value as string);
            if (idx == -1) return false;

            slots[idx] = null;
            return true;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            var resultSet = new PowerSet<T>();
            foreach (var element in slots)
            {
                if (set2.Find(element) != -1) resultSet.Put(element);
            }
            return resultSet;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            var resultSet = new PowerSet<T>();
            Array.Copy(slots, resultSet.slots, slots.Length);
            foreach (var element in set2.slots)
            {
                resultSet.Put(element);
            }
            return resultSet;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            var resultSet = new PowerSet<T>();
            foreach (var element in slots)
            {
                if (set2.Find(element) == -1) resultSet.Put(element);
            }
            return resultSet;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            return set2.slots.All(element => Find(element) != -1);
        }

    }
}