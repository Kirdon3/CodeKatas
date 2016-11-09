using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class Arge
    {

        public static int NbYear(int p0, double percent, int aug, int p)
        {
            // your code

            for (int i = 0; i < 1000; i++)
            {
                var population = p0 + p0 * percent / 100 + aug;
                if (population >= p)
                {
                    return i+1;
                }
                p0 = (int)population;

            }
            return -1;
        }
    }
    public static class ArgeTests
    {

        private static void testing(int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void test1()
        {
            Console.WriteLine("Testing NbYear");
            testing(Arge.NbYear(1000, 2, 50, 1200), 3);
            testing(Arge.NbYear(1500000, 2.5, 10000, 2000000), 10);
            testing(Arge.NbYear(1500000, 0.25, 1000, 2000000), 94);
        }
    }
}
