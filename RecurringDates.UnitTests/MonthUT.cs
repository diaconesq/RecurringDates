using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class MonthUT
    {
        [TestCase(1, Month.Jan)]
        [TestCase(6, Month.Jun)]
        [TestCase(12, Month.Dec)]
        public void MonthsMatchDateTimeMonthValue(int month, Month monthEnum)
        {
            new DateTime(2015, month, 1).Month.Should().Be((int) monthEnum);
        }
    }
}