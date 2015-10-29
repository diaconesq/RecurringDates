using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    public class NthInMonthRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProcessor, new()
    {
        [Test]
        public void NthIsZero_ThrowsException()
        {

            var rule = new NthInMonthRule()
            {
                Nth = 0, 
                ReferencedRule = Substitute.For<IRule>()
            };

            Action act = () => { Process(rule).IsMatch(new DateTime()); };

            act.ShouldThrow<Exception>();
        }

        [TestCase(2015, 3, 9, true)]
        [TestCase(2015, 3, 8, false)]
        [TestCase(2015, 3, 10, false)]
        [TestCase(2015, 3, 2, false)]
        [TestCase(2015, 3, 16, false)]
        public void SecondMonday(int year, int month, int day, bool expected)
        {
            var rule = DayOfWeek.Monday.EveryWeek().The2ndOccurenceInTheMonth();

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 31, true)]
        [TestCase(2015, 3, 30, false)]
        [TestCase(2015, 3, 1, false)]
        [TestCase(2015, 4, 1, false)]
        public void LastDay(int year, int month, int day, bool expected)
        {
            var rule = new EveryDayRule().TheLastOccurenceInTheMonth();

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 3, 31, false)]
        [TestCase(2015, 3, 30, true)]
        [TestCase(2015, 3, 23, false)]
        [TestCase(2015, 3, 2, false)]
        public void LastMonday(int year, int month, int day, bool expected)
        {
            var rule = DayOfWeek.Monday.EveryWeek().TheLastOccurenceInTheMonth();

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }

        [TestCase(2015, 4, 10, true)]
        [TestCase(2015, 4, 14, true)]
        [TestCase(2015, 4, 3, false)]
        [TestCase(2015, 4, 7, false)]
        [TestCase(2015, 4, 17, false)]
        [TestCase(2015, 4, 21, false)]
        public void SecondTuesdayOrFriday(int year, int month, int day, bool expected)
        {
            var rule = new DaysOfWeekRule(DayOfWeek.Tuesday, DayOfWeek.Friday)
                .The2ndOccurenceInTheMonth();

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }


        [TestCase(2015, 3, 31, false)]
        [TestCase(2015, 3, 15, true)]
        [TestCase(2015, 3, 14, false)]
        [TestCase(2015, 3, 16, false)]
        public void The15thOfTheMonth(int year, int month, int day, bool expected)
        {
            var rule = new NthInMonthRule()
            {
                Nth = 15,
                ReferencedRule = new EveryDayRule()
            };

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }

        [Test]
        public void The31stOfFebruary_ShouldNeverMatch()
        {
            var rule = new NthInMonthRule()
            {
                Nth = 31,
                ReferencedRule = new EveryDayRule()
            }.InMonths(Month.Feb);

            var dates = new DateTime(2015,2,28).UpTo(new DateTime(2015,3,5));
            foreach (var date in dates)
            {
                Process(rule).IsMatch(date).Should().Be(false);
            }

        }

    }
}