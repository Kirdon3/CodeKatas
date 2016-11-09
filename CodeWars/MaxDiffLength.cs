using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    public class MaxDiffLength
    {

        public static int Mxdiflg(string[] a1, string[] a2)
        {
            var test1 = Math.Abs(a1.Select(x => x.Length).Max() - a2.Select(x => x.Length).Min());
            var test2 = Math.Abs(a2.Select(x => x.Length).Max() - a1.Select(x => x.Length).Min());

            if (a1.Length == 0 || a2.Length == 0)
            {
                return -1;
            }
            return test1 > test2 ? test1 : test2;


        }
        [TestFixture]
        public static class MaxDiffLengthTests
        {

            [Test]
            public static void test1()
            {
                string[] s1 = new string[] { "hoqq", "bbllkw", "oox", "ejjuyyy", "plmiis", "xxxzgpsssa", "xxwwkktt", "znnnnfqknaz", "qqquuhii", "dvvvwz" };
                string[] s2 = new string[] { "cccooommaaqqoxii", "gggqaffhhh", "tttoowwwmmww" };
                Assert.AreEqual(13, MaxDiffLength.Mxdiflg(s1, s2));
            }
        }

    }
}
