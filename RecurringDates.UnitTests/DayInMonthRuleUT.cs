using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    public class DayInMonthRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProcessor, new()
    {
        [TestCase(2015, 10, 30, true)]
        [TestCase(2014, 10, 30, true)]
        [TestCase(2015, 10, 13, false)]
        [TestCase(2015, 7, 30, false)]
        public void ItMatchesDayAndMonthButNotYear(int year, int month, int day, bool expected)
        {
            var rule = Month.Oct.Day(30);

            Process(rule).IsMatch(new DateTime(year, month, day))
                .Should().Be(expected);
        }
    }
}