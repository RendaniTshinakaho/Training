using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestFixture]
    class StringCalculatorTests
    {
        ICalculator sut = new Calculator();
        private int _outcome;

        [SetUp]
        public void SetUp()
        {
            _outcome = sut.Add("");
        }

        [Test]
        public void WhenPassingAnEmptyStringToTheCalculatorItShouldReturnZero()
        {
            Assert.That(_outcome,Is.EqualTo(0));
        }

        [TestCase("1",1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void WhenPassingAsingleNumberToTheCalculator(string input,int expected)
        {
            _outcome = sut.Add(input);
            Assert.That(_outcome,Is.EqualTo(expected));
        }

        [TestCase("1,2",3)]
        [TestCase("2,3", 5)]
        [TestCase("3,4", 7)]
        public void WhenPassingMorethanOneCommaSeparetedValues(string input,int expected)
        {
            _outcome = sut.Add(input);
            Assert.That(_outcome,Is.EqualTo(expected));
        }
        [TestCase("1\n2,3",6)]
        public void WhenPassingMoreThanTwoNumbersSeparatedByCommasOrNewLines(string input,int expected)
        {
            _outcome = sut.Add(input);
            Assert.That(_outcome,Is.EqualTo(expected));
        }

        [TestCase("//;\n1;2",3)]
        public void whenSettingAnewDelimeterandPassingValuesUsingNewDelimeter()
        {

        }
    }

}
