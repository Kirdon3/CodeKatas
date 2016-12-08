using System;
using System.Linq;
using StringCalculatorKata;

namespace StringCalculator1
{
    public class StringCalculator
    {
        private char _defaultSeparator = ',';
        private NumbersSanitizer _numbersSanitizer;
        private NumbersExtractor _numbersExtractor;
        private const int DefaultValue = 0;


        public StringCalculator()
        {
            _numbersSanitizer = new NumbersSanitizer(_defaultSeparator);
            _numbersExtractor = new NumbersExtractor(_defaultSeparator);
        }

        public int Add(string numbersString)
        {
            if (ShouldReturnDefaultValue(numbersString))
            {
                return DefaultValue;
            }


            numbersString = _numbersSanitizer.SanitizeNumbers(numbersString);
            var extractedNumbers = _numbersExtractor.ExtractNumbers(numbersString);

            var extractedNumbersArray = extractedNumbers as int[] ?? extractedNumbers.ToArray();
            if (extractedNumbersArray.Any(x => x <= 0))
            {
                throw new NegativesNotAllowedException();
            }

            return extractedNumbersArray.Sum();
        }




        private static bool ShouldReturnDefaultValue(string numbersString)
        {
            return String.IsNullOrEmpty(numbersString);
        }
    }
}
