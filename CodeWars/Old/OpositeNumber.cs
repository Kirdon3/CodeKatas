using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class OpositeNumber
    {
        public static int Opposite(int number)
        {
            return -number;
        }
    }

    [TestFixture]
    public class OpositeNumberTests
    {
        [Test]
        public void Test_1()
        {
            Assert.AreEqual(-1, OpositeNumber.Opposite(1));
        }
    }
}