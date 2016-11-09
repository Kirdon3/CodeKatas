using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class GetStringMiddle
    {
        public static string GetMiddle(string s)
        {
            return s.Length%2 == 0 ? s[s.Length/2 -1].ToString() + s[s.Length / 2] : s[s.Length/2].ToString();

            //Probably better:   return s.Substring(s.Length / 2 - (1 - s.Length % 2), 2 - s.Length % 2);
        }

        [TestFixture]
        public class GetMiddleTest
        {
            [Test]
            public void GenericTests()
            {
                Assert.AreEqual("es", GetStringMiddle.GetMiddle("test"));
                Assert.AreEqual("t", GetStringMiddle.GetMiddle("testing"));
                Assert.AreEqual("dd", GetStringMiddle.GetMiddle("middle"));
                Assert.AreEqual("A", GetStringMiddle.GetMiddle("A"));
            }
        }
    }
}
