using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class NthBeforeAfterRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProjection, new()
    {
        [TestCase(2015, 3, 2, true)]
        [TestCase(2015, 3, 1, false)]
        [TestCase(2015, 3, 3, false)]
        [TestCase(2015, 3, 4, false)]
        public void ThirdDayAfterLastFriday(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = 3,
                NthRule = new EveryDayRule(),
                ReferencedRule = DayOfWeek.Friday.EveryWeek().TheLastOccurenceInTheMonth()
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 2, false)]
        [TestCase(2015, 3, 9, false)]
        [TestCase(2015, 3, 8, false)]
        [TestCase(2015, 3, 10, false)]
        [TestCase(2015, 3, 16, true)]
        public void SecondMondayAfterFirstFriday(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = 2,
                NthRule = DayOfWeek.Monday.EveryWeek(),
                ReferencedRule = DayOfWeek.Friday.EveryWeek().The1stOccurenceInTheMonth()
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 1, true)]
        [TestCase(2015, 3, 2, false)]
        [TestCase(2015, 2, 28, false)]
        [TestCase(2015, 1, 1, true)]
        public void FirstDayAfterLastDayInMonth(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = 1,
                NthRule = new EveryDayRule(),
                ReferencedRule = new EveryDayRule().TheLastOccurenceInTheMonth()
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 14, true)]
        [TestCase(2015, 3, 15, false)]
        [TestCase(2015, 3, 16, false)]
        [TestCase(2015, 3, 13, false)]
        public void FirstDayBefore15OfTheMonth(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = -1,
                NthRule = new EveryDayRule(),
                ReferencedRule = new EveryDayRule().TheNthOccurenceInTheMonth( 15 )
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 4, true)]
        [TestCase(2015, 3, 5, true)]
        [TestCase(2015, 3, 11, true)]
        [TestCase(2015, 3, 12, true)]
        [TestCase(2015, 3, 18, true)]
        [TestCase(2015, 3, 19, true)]
        [TestCase(2015, 3, 25, true)]
        [TestCase(2015, 3, 26, true)]
        [TestCase(2015, 4, 1, true)]
        [TestCase(2015, 4, 2, true)]

        [TestCase(2015, 3, 3,  false)]
        [TestCase(2015, 3, 6,  false)]
        [TestCase(2015, 3, 10, false)]
        [TestCase(2015, 3, 13, false)]
        [TestCase(2015, 3, 17, false)]
        [TestCase(2015, 3, 20, false)]
        [TestCase(2015, 3, 24, false)]
        [TestCase(2015, 3, 27, false)]
        [TestCase(2015, 3, 31,  false)]
        [TestCase(2015, 4, 3,  false)]
        public void TwoDaysAfterEveryMondayAndTuesday(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = 2,
                NthRule = new EveryDayRule(),
                ReferencedRule = new DaysOfWeekRule(DayOfWeek.Monday, DayOfWeek.Tuesday)
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 9, true)]
        [TestCase(2015, 3, 11, true)]
        public void MondayAndWednesdayBefore15Th(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = 1,
                NthRule = new DaysOfWeekRule(DayOfWeek.Monday, DayOfWeek.Wednesday),
                ReferencedRule = new NthDayBeforeAfterRule() { Nth = 15, ReferencedRule = new EveryDayRule() }
            };
            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);

        }
        [TestCase(2015, 3, 9, true)]
        [TestCase(2015, 3, 11, true)]
        public void MondayAndWednesdayBefore2ndFriday(int year, int month, int day, bool expected)
        {
            var nthRule = new NthBeforeAfterRule()
            {
                Nth = -1,
                NthRule = new DaysOfWeekRule(DayOfWeek.Monday, DayOfWeek.Wednesday),
                ReferencedRule = DayOfWeek.Friday.EveryWeek().The2ndOccurenceInTheMonth()
            };

            var date = new DateTime(year, month, day);

            Project(nthRule).IsMatch(date).Should().Be(expected);

        }

    }
}