using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CodeWars
{
    class FindNextSquares
    {
        public static long FindNextSquare(long num)
        {
            // your code here
            if ((Math.Sqrt(num) % 1) == 0)
            {
                long test = (long)((Math.Sqrt(num) + 1));
                return (long)(Math.Pow(test,2));
            }
            return -1;
        }
    }
    [TestFixture]
    public class FindNextSquareTests
    {
        [Test]
        [TestCase(155, -1)]
        [TestCase(121,  144)]
        [TestCase(625, 676)]
        [TestCase(319225,   320356)]
        [TestCase(15241383936,   15241630849)]
        public void FixedTest(long num, long expectedResult)
        {
            Assert.AreEqual(expectedResult, FindNextSquares.FindNextSquare(num));
        }
    }
}
