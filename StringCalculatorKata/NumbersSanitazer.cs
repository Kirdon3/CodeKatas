using NUnit.Framework;

namespace StringCalculatorKata
{
    public class NumbersSanitizer
    {
        private char _defaultSeparator;

        public NumbersSanitizer(char defaultSeparator)
        {
            _defaultSeparator = defaultSeparator;
        }

        public string SanitizeNumbers(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                numbers = numbers.Replace(numbers[2], _defaultSeparator);
                numbers = numbers.Substring(4);
            }
            return numbers.Replace("\n", _defaultSeparator.ToString());
        }
    }


    [TestFixture]
    public class NumbersSanitizerTests
    {
        [TestCase(@"//;\1;2", "1,2")]
        [TestCase(@"//;\3;5", "3,5")]


        public void Sanitizer_WhenSeparatorSpecifierExists_ReturnsSanitizedString(string numbers, string expectedResult)
        {
            var numbersSanitizer = new NumbersSanitizer(',');
            var result = numbersSanitizer.SanitizeNumbers(numbers);
            Assert.AreEqual(expectedResult, result);

        }
    }
}