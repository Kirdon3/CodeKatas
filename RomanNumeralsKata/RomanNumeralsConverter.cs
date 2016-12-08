using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RomanNumeralsKata
{
    public class RomanNumeralsConverter
    {
        public double ConvertToArabic(string romanNumber)
        {
            return 1;
        }
    }

    [TestFixture]
    public class RomanNumeralsConverterTests
    {
        [Test]
        public void Converting_Number_I_Returns_one()
        {
            var numeralsConverter = new RomanNumeralsConverter();

            var result = numeralsConverter.ConvertToArabic("I");

            Assert.AreEqual(1, result);
        }
    }
}
