using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class PlayingWithNumbers
    {
        public static long digPow(int n, int p)
        {
            // your code
            var digits = n.ToString().Select(x => double.Parse(x.ToString()));

            var poweredDigits = digits.Select((x, y) => Math.Pow(x, p + y));

            var poweredDigitsSum = poweredDigits.Sum()/n;

            return (long) (poweredDigitsSum % 1 == 0 ? poweredDigitsSum : -1);


            //Possibly better:

            //var sum = Convert.ToInt64(n.ToString().Select(s => Math.Pow(int.Parse(s.ToString()), p++)).Sum());
            //return sum % n == 0 ? sum / n : -1;
        }


    }
    [TestFixture]
    public class DigPowTests
    {

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, PlayingWithNumbers.digPow(89, 1));
        }
        [Test]
        public void Test2()
        {
            Assert.AreEqual(-1, PlayingWithNumbers.digPow(92, 1));
        }
        [Test]
        public void Test3()
        {
            Assert.AreEqual(51, PlayingWithNumbers.digPow(46288, 3));
        }
    }
}
