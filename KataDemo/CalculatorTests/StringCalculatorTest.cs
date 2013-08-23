using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CalculatorTests
{
//    1. AnEmptyStringReturnsZero
//2. AsingleNumberRetursTheValue
//3. TwoNumbersCommaDelimitedReturnsTheSum
//4. TwoNumbersNewlineDelimitedReturnsTheSum
//5. ThreeNumbersDelimitedEitherWayReturnsTheSum
//6. NegativeNumbersThrowAnException
//7. Numbers greater than 1000 are ignored
    [TestFixture]
    public class StringCalculatorTest
    {
        private Calculator _calc;

        [SetUp]
        public void Setup()
        {
            _calc = new Calculator();
        }

        [Test]
        public void AnEmptyStringReturnsZero()
        {
            Assert.That(_calc.Add(string.Empty), Is.EqualTo(0));
        }

        [Test]
        public void AsingleNumberRetursTheValue()
        {
            Assert.That(_calc.Add("1"), Is.EqualTo(1));
        }

        [Test]
        public void TwoNumbersCommaDelimitedReturnsTheSum()
        {
            Assert.That(_calc.Add("1,2"), Is.EqualTo(3));
        }

        [Test]
        public void TwoNumbersNewlineDelimitedReturnsTheSum()
        {
            Assert.That(_calc.Add("1\n2,3"), Is.EqualTo(6));
        }

        [Test]
        public void ThreeNumbersDelimitedEitherWayReturnsTheSum()
        {
            Assert.That(_calc.Add("//;\n1;2"), Is.EqualTo(3));
        }

        [Test]
        [ExpectedException(typeof(NegativeException), ExpectedMessage = "Negatives not allowed: -1")]
        public void Negativethrowexception()
        {
            _calc.Add("-1,2");
        }

        [Test]
        [ExpectedException(typeof(NegativeException), ExpectedMessage = "Negatives not allowed: -1,-5")]
        public void Negativesthrowexception()
        {
            _calc.Add("-1,2,-5,6");
        }

        [Test]
        public void DelimsofAnylength()
        {
            Assert.That(_calc.Add("//[***]\n1***2***3"), Is.EqualTo(6));
        }
        [Test]
        public void MultipledelimsofAnylength()
        {
            Assert.That(_calc.Add("//[***][%]\n1***2%3"), Is.EqualTo(6));
        }
        [Test]
        public void Graterthano0ShouldbeIgnored()
        {
            Assert.That(_calc.Add("1001,2"), Is.EqualTo(2));
        }

    }

    public class NegativeException : Exception
    {
        public NegativeException(string error)
            : base(error)
        {

        }
    }

    public class Calculator
    {
        public int Add(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            var delims = new List<string> { ",", "\n" };
            if (val.StartsWith("//["))
            {
                var dels = val.Substring(3, val.IndexOf("]\n", StringComparison.Ordinal) - 3)
                              .Split(new[] { "][" }, StringSplitOptions.None);
                delims.AddRange(dels);
                val = val.Substring(val.IndexOf("]\n", StringComparison.Ordinal) + 2);
            }
            else if (val.StartsWith("//"))
            {
                delims.Add(val[2].ToString());
                val = val.Substring(val.IndexOf('\n') + 1);
            }
            var numbers = val.Split(delims.ToArray(), StringSplitOptions.None);
            CheckforNegs(numbers);
            return numbers.Select(int.Parse).Where(x => x <= 1000).Sum();
        }

        private static void CheckforNegs(IEnumerable<string> numbers)
        {
            var negs = numbers.Select(int.Parse).Where(x => x < 0);
            if (!negs.Any()) return;
            const string error = "Negatives not allowed: {0}";
            throw new NegativeException(string.Format(error, string.Join(",", negs)));
        }
    }
}
