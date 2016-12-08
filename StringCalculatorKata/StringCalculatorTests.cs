using NUnit.Framework;
using StringCalculatorKata;

namespace StringCalculator1
{
    [TestFixture]
    public class StringCalculatorTests
    {

        [Test]
        public void Add_WhenEmptyString_Returns0()
        {
            StringCalculator stringCalculator = new StringCalculator();
            var result = stringCalculator.Add("");
            Assert.AreEqual(0, result);
        }

        [TestCase("1", 1)]
        [TestCase("8", 8)]
        [TestCase("3", 3)]
        [TestCase("11", 11)]
        public void Add_WhenAddingSingleNumber_ReturnsThisNumber(string numbers, int expectedResult)
        {
            ArrangeAndAssert(numbers, expectedResult);
        }

        [TestCase("1,3", 4)]
        [TestCase("2,5", 7)]
        [TestCase("3,3", 6)]

        public void Add_WhenMultipleNumbers_ReturnsSumOfNumbers(string numbers, int expectedResult)
        {
            ArrangeAndAssert(numbers, expectedResult);
        }

        [TestCase("3,3,5", 11)]

        public void Add_WhenUnknownNumberOfNumbers_SumReturned(string numbers, int expectedResult)
        {
            ArrangeAndAssert(numbers, expectedResult);

        }
        [TestCase(@"//;\1;2", 3)]
        [TestCase(@"//;\3;5", 8)]


        public void Add_WhenContaintsSeparatorSpecifier_ThenSumOfNumbersReturned(string numbers, int expectedResult)
        {
            ArrangeAndAssert(numbers, expectedResult);

        }

        [TestCase("3\n5", 8)]

        public void Add_WhenNumbersSeparatedByANewLine_SumReturned(string numbers, int expectedResult)
        {
            ArrangeAndAssert(numbers, expectedResult);

        }

        [TestCase("0,-2,1")]
        [TestCase("-1")]

        public void Add_WhenContainsNegativeNumbers_ExceptionIsThrown(string numbers)
        {
            StringCalculator stringCalculator = new StringCalculator();

            NegativesNotAllowedException exception = 
                Assert.Throws<NegativesNotAllowedException>(delegate { stringCalculator.Add(numbers); });
        }

        private static void ArrangeAndAssert(string numbers, int expectedResult)
        {
            StringCalculator stringCalculator = new StringCalculator();
            var result = stringCalculator.Add(numbers);
            Assert.AreEqual(expectedResult, result);
        }


    }
}