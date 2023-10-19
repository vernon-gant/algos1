using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {
        public int[] HeapArray; // хранит неотрицательные числа-ключи

        public int Count;

        public Heap()
        {
            HeapArray = null;
        }

        public void MakeHeap(int[] a, int depth)
        {
            int heapSize = 0;
            for (int i = 0; i <= depth; i++) heapSize = heapSize * 2 + 1;
            HeapArray = new int[heapSize];

            foreach (int key in a) Add(key);
        }

        public int GetMax()
        {
            if (Count == 0) return -1;

            int rootKey = HeapArray[0];
            HeapArray[0] =  Count == 1 ? 0 : HeapArray[Count - 1];
            HeapArray[Count - 1] = 0;

            int currentIdx = 0;
            int maxChildIdx = GetMaxChildIndex(currentIdx);

            while (maxChildIdx != -1 && HeapArray[currentIdx] < HeapArray[maxChildIdx])
            {
                (HeapArray[currentIdx], HeapArray[maxChildIdx]) = (HeapArray[maxChildIdx], HeapArray[currentIdx]);
                currentIdx = maxChildIdx;
                maxChildIdx = GetMaxChildIndex(currentIdx);
            }

            Count--;

            return rootKey;
        }

        private int GetMaxChildIndex(int idx)
        {
            int leftChildIdx = idx * 2 + 1;
            int rightChildIdx = idx * 2 + 2;

            if (leftChildIdx >= Count) return -1;
            if (rightChildIdx >= Count) return leftChildIdx;

            return HeapArray[leftChildIdx] > HeapArray[rightChildIdx] ? leftChildIdx : rightChildIdx;
        }

        public bool Add(int key)
        {
            if (Count == HeapArray.Length) return false;

            int currentIdx = Count;
            HeapArray[currentIdx] = key;
            int parentIdx = (currentIdx - 1) / 2;
            bool isBiggerThanParent = key > HeapArray[parentIdx];

            while (isBiggerThanParent)
            {
                HeapArray[currentIdx] = HeapArray[parentIdx];
                HeapArray[parentIdx] = key;
                currentIdx = parentIdx;
                parentIdx = (currentIdx - 1) / 2;
                isBiggerThanParent = key > HeapArray[parentIdx];
            }

            Count++;

            return true;
        }
    }
}