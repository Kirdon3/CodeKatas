using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    public class ASum
    {

        public static long findNb(long m)
        {
            long sum = 0;
            long result = 0;

            for (long i = 1; sum<m ; i++)
            {
                sum += i*i*i;
                result = i;
            }

            return sum == m ? result : -1;
        }

    }
    [TestFixture]
    public class ASumTests
    {


        [Test]
        public void Test1()
        {
            Assert.AreEqual(2022, ASum.findNb(4183059834009));
        }
        [Test]
        public void Test2()
        {
            Assert.AreEqual(-1, ASum.findNb(24723578342962));
        }
        [Test]
        public void Test3()
        {
            Assert.AreEqual(4824, ASum.findNb(135440716410000));
        }
        [Test]
        public void Test4()
        {
            Assert.AreEqual(3568, ASum.findNb(40539911473216));

        }
    }
}
