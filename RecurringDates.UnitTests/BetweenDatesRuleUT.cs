using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    public class BetweenDatesRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProcessor, new()
    {
        [Test]
        public void EnumeratingDates_ReturnsFiniteMatchingOccurrencesInOrder()
        {
            var rule = DayOfWeek.Wednesday.EveryWeek().Between(new DateTime(2015, 10, 28), new DateTime(2015, 11, 11));

            var enumerableRule = Process(rule);
            var first3 = enumerableRule.MatchingDates;
            first3.Should().BeEquivalentTo(
                new DateTime(2015, 10, 28),
                new DateTime(2015, 11, 4),
                new DateTime(2015, 11, 11)
                );
        }

        [Test]
        public void DateBeforeStart_DoesNotMatch()
        {
            var rule = DayOfWeek.Wednesday.EveryWeek().Between(new DateTime(2015, 10, 28), new DateTime(2015, 11, 11));

            Process(rule).IsMatch(new DateTime(2015, 10, 21)).Should().BeFalse();
        }

        [Test]
        public void DateAfterEnd_DoesNotMatch()
        {
            var rule = DayOfWeek.Wednesday.EveryWeek().Between(new DateTime(2015, 10, 28), new DateTime(2015, 11, 11));

            Process(rule).IsMatch(new DateTime(2015, 11, 18)).Should().BeFalse();
        }
    }
}