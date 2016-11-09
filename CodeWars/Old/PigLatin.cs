using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class PigLatin
    {
        public static string PigIt(string str)
        {
            return string.Join(" ", str.Split().Select(x => x.Substring(1)+x[0]+"ay"));
        }

    }

    [TestFixture]
    public class KataTest
    {
        [Test]
        public void KataTests()
        {
            Assert.AreEqual("igPay atinlay siay oolcay", PigLatin.PigIt("Pig latin is cool"));
            Assert.AreEqual("hisTay siay ymay tringsay", PigLatin.PigIt("This is my string"));
        }
    }
}

