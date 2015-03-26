using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class DayOfWeekRuleUT
    {
        [Test]
        public void Day_ShouldMatch_Monday()
        {
            var rule = new DayOfWeekRule
            {
                DayOfWeek = DayOfWeek.Monday
            };
            var date = new DateTime(2015, 3, 9);

            rule.IsMatch(date).Should().BeTrue();
        }
    }
}