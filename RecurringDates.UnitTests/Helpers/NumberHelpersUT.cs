using FluentAssertions;
using NUnit.Framework;
using RecurringDates.Helpers;

namespace RecurringDates.UnitTests.Helpers
{
    [TestFixture]
    public class NumberHelpersUT
    {
        [TestCase(0, "0th")]
        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(11, "11th")]
        [TestCase(12, "12th")]
        [TestCase(13, "13th")]
        [TestCase(14, "14th")]
        [TestCase(21, "21st")]
        [TestCase(22, "22nd")]
        [TestCase(23, "23rd")]
        [TestCase(24, "24th")]
        [TestCase(231,  "231st")]
        [TestCase(232,  "232nd")]
        [TestCase(233,  "233rd")]
        [TestCase(234,  "234th")]
        [TestCase(2311, "2311th")]
        [TestCase(2312, "2312th")]
        [TestCase(2313, "2313th")]
        [TestCase(2314, "2314th")]
        [TestCase(2321, "2321st")]
        [TestCase(2322, "2322nd")]
        [TestCase(2323, "2323rd")]
        [TestCase(2324, "2324th")]
        public void ToOrdinalString_WithPositiveIntegers(int number, string expected)
        {
            number.ToOrdinalString().Should().Be(expected);
        }
        [TestCase(-1, "last")]
        [TestCase(-2, "2nd to last")]
        [TestCase(-100, "100th to last")]
        public void ToOrdinalString_WithNegativeIntegers(int number, string expected)
        {
            number.ToOrdinalString().Should().Be(expected);
        }
    }
}
