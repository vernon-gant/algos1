using System;
using System.Collections.Generic;
using System.Diagnostics;
using AlgorithmsDataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SetTests
{
    [TestClass]
    public class TestIntersection
    {

        private readonly PowerSet<string> _set1 = new();

        private readonly PowerSet<string> _set2 = new();

        [TestMethod]
        public void NotEmptyResult()
        {
            
            SetGenerator.From0To20000(_set1);
            SetGenerator.From0To10000(_set2);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var resultSet = _set1.Intersection(_set2);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            
            Assert.IsTrue(ts.TotalMilliseconds < 2000, $"Execution Time of Intersection exceeded 2 seconds: {ts.TotalMilliseconds} ms");
            Assert.AreEqual(resultSet.Size(),10000);
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsTrue(resultSet.Get($"{i}"));
            }
        }
        
        [TestMethod]
        public void EmptyResult()
        {
            
        }

    }

    public static class SetGenerator
    {

        public static void From0To10000(PowerSet<string> powerSet)
        {
            for (int i = 0; i < 10000; i++)
            {
                powerSet.Put($"{i}");
            }
        }
        
        public static void From10000To20000(PowerSet<string> powerSet)
        {
            for (int i = 10000; i < 20000; i++)
            {
                powerSet.Put($"{i}");
            }
        }

        public static void From0To20000(PowerSet<string> powerSet)
        {
            for (int i = 0; i < 20000; i++)
            {
                powerSet.Put($"{i}");
            }
        }

    }
}