using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorKata
{
    public class NumbersExtractor
    {
        private char _defaultSeparator;

        public NumbersExtractor(char defaultSeparator)
        {
            _defaultSeparator = defaultSeparator;
        }

        public IEnumerable<int> ExtractNumbers(string numbers)
        {
            return ConvertMultipleNumbers(numbers);
        }

        private IEnumerable<int> ConvertMultipleNumbers(string numbersString)
        {
            var splitNumbers = SplitNumbers(numbersString);
            return splitNumbers.Select(x => ConvertSingleNumber(x));

        }

        private IEnumerable<string> SplitNumbers(string numbersString)
        {
            return numbersString.Split(_defaultSeparator);
        }

        private static int ConvertSingleNumber(string numbersString)
        {
            return Int32.Parse(numbersString);
        }

    }
}