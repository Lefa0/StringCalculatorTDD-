using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorKATA
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers))
                return 0;

            else if (numbers.Last() == ',' || numbers.Last() == '\n')
                throw new ArgumentException("Cannot use separators at the end.");

            else
            {
                var delimsList = new List<char>() { ',', '\n' };
                var errorMessages = new List<string>();

                if (numbers.StartsWith('/'))
                {
                    var customDelim = GetCustomDelims(numbers);
                    numbers = numbers.Replace("/", string.Empty);

                    // Check for unexpected delimiters
                    if (UnexpectedDelim(numbers, customDelim))
                    {
                        var expectedDelim = string.Join("or", customDelim.Select(d => $"'{d}'")); // Join multiple delimiters
                        var unexpectedDelim = numbers[numbers.IndexOf('\n') + 1];
                        var unexpectedDelimIndex = numbers.IndexOf(unexpectedDelim);
                        throw new ArgumentException($"'{expectedDelim}' expected but '{unexpectedDelim}' found at position {unexpectedDelimIndex + 1}");
                    }

                    delimsList.AddRange(customDelim);
                }
                var delimsArray = delimsList.ToArray();
                var numbersList = numbers.Split(delimsArray, StringSplitOptions.RemoveEmptyEntries)?.Select(Int32.Parse)?.ToList();

                if (CheckNegativeNumbers(numbersList!))
                {
                    var negValue = numbersList!.Where(x => x < 0).ToList();
                    throw new ArgumentException($"Negative number(s) not allowed: {string.Join(", ", negValue)}");
                }

                if (errorMessages.Any())
                {
                    throw new ArgumentException(string.Join("\n", errorMessages));
                }
                return numbersList!.Where(x => x <= 1000).Sum();
            }
        }
        public char[] GetCustomDelims(string numbers)
        {
            return numbers.Substring(0, numbers.IndexOf('\n')).ToArray();
        }
        public bool CheckNegativeNumbers(List<int> numbersList)
        {
            var isNegative = numbersList!.Any(x => x < 0);

            if (isNegative)
            {
                return true;
            }
            else
                return false;
        }
        public string SubString(string numbers)
        {
            return numbers.Substring(numbers.IndexOf('\n') + 1);
        }
        public bool UnexpectedDelim(string numbers, char[] customDelims)
        {
            var subString = SubString(numbers);         // Substring of the numbers string from the newline separator

            foreach (var c in subString)
            {
                if (!customDelims.Contains(c) && !Char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
