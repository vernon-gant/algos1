using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {

        public int filter_len;

        public uint[] _bitArray;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            _bitArray = new uint[BitsToArrayLength(f_len)];
        }

        private int BitsToArrayLength(int bitsAmount)
        {
            return (bitsAmount - 1 + 32) / 32;
        }

        // хэш-функции
        public int Hash1(string str1)
        {
            int result = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                int code = str1[i];
                result = (result * 17 + code) % (_bitArray.Length * 32);
            }
            return result;
        }

        public int Hash2(string str1)
        {
            int result = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                int code = str1[i];
                result = (result * 223 + code) % (_bitArray.Length * 32);
            }
            return result;
        }

        public void Add(string str1)
        {
            int hash1 = Hash1(str1);
            int hash2 = Hash2(str1);
            int bitSequencePosition1 = hash1 / 32;
            int bitSequencePosition2 = hash2 / 32;
            _bitArray[bitSequencePosition1] |= (uint)(1 << hash1);
            _bitArray[bitSequencePosition2] |= (uint)(1 << hash2);
        }

        public bool IsValue(string str1)
        {
            int hash1 = Hash1(str1);
            int hash2 = Hash2(str1);
            int bitSequencePosition1 = hash1 / 32;
            int bitSequencePosition2 = hash2 / 32;
            return (_bitArray[bitSequencePosition1] & (uint)(1 << hash1)) != 0 &&
                   (_bitArray[bitSequencePosition2] & (uint)(1 << hash2)) != 0;
        }

    }
}