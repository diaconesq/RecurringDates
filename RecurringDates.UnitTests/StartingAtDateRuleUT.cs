using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    public class StartingAtDateRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProcessor, new()
    {
        [Test]
        public void EnumeratingDates_ReturnsOnlyMatchingOccurrencesInOrder()
        {
            var rule = DayOfWeek.Wednesday.EveryWeek().Starting(new DateTime(2015, 10, 28));

            var first3 = Process(rule).MatchingDates.Take(3);
            first3.Should().BeEquivalentTo(
                new DateTime(2015, 10, 28),
                new DateTime(2015, 11, 4),
                new DateTime(2015, 11, 11)
                );
        }
        [Test]
        public void DateBeforeStart_DoesNotMatch()
        {
            var rule = DayOfWeek.Wednesday.EveryWeek().Starting(new DateTime(2015, 10, 28));

            Process(rule).IsMatch(new DateTime(2015, 10, 21)).Should().BeFalse();
        }

    }
}