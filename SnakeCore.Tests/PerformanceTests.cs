using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static SnakeCore.Web.GameState;

namespace SnakeCore.Tests
{
    /// <summary>
    /// A place for testing performance tweaks that aren't directly related to a specific class
    /// </summary>
    [TestFixture]
    public class PerformanceTests
    {
        [Test]
        public void ArrayVsList_CopyThenInsertHeadAndRemoveTail_ArrayIsFaster()
        {
            var list = new List<BodyPartPosition> { new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1) };
            var array = new BodyPartPosition[] { new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1), new BodyPartPosition(1, 1) };

            var stopwatch = Stopwatch.StartNew();
            List<BodyPartPosition> newList;
            for (int i = 0; i < 65536; i++)
            {
                newList = list.ToList();
                newList.Insert(0, new BodyPartPosition(2, 2));
                newList.RemoveAt(list.Count - 1);
            }
            stopwatch.Stop();
            long listTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"List: {listTime} ms");

            stopwatch.Restart();
            BodyPartPosition[] newArray;
            for (int i = 0; i < 65536; i++)
            {
                newArray = new BodyPartPosition[array.Length];
                Array.Copy(array, 0, newArray, 1, array.Length - 1);
                newArray[0] = new BodyPartPosition(2, 2);
            }
            stopwatch.Stop();
            var arrayTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Array: {arrayTime} ms");

            Assert.That(arrayTime, Is.LessThan(listTime));
        }
    }
}
