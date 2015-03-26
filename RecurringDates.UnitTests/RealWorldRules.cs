using System;
using FluentAssertions;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class RealWorldRules
    {

        [TestCase(2015, 1, 19, true)]
        [TestCase(2015, 1, 22, true)]
        [TestCase(2015, 1, 23, true)]

        [TestCase(2015, 4, 20, true)]
        [TestCase(2015, 4, 23, true)]
        [TestCase(2015, 4, 24, true)]

        [TestCase(2015, 7, 20, true)]
        [TestCase(2015, 7, 23, true)]
        [TestCase(2015, 7, 24, true)]

        [TestCase(2015, 10, 19, true)]
        [TestCase(2015, 10, 22, true)]
        [TestCase(2015, 10, 23, true)]

        [TestCase(2015, 12, 21, true)]
        [TestCase(2015, 12, 24, true)]
        [TestCase(2015, 12, 25, true)]

        //The 3rd Monday and also Thursday & Friday after the 3rd Monday of January/April/July/October/December
        public void The3rdMondayAndFollowingThursdayAndFridayOfGivenMonths(int year, int month, int day, bool expected)
        {
            var anyThirdMonday = new NthInMonthRule()
            {
                Nth = 3,
                ReferencedRule = new DayOfWeekRule()
                {
                    DayOfWeek = DayOfWeek.Monday
                }
            };

            var thirdMonday = new MonthsFilterRule()
            {
                Months = new[] {Month.Jan, Month.Apr, Month.Jul, Month.Oct, Month.Dec},
                ReferencedRule = anyThirdMonday
            };


            var thuOrFri = new NthBeforeAfterRule()
            {
                Nth = 1,
                NthRule = new DaysOfWeekRule(DayOfWeek.Thursday, DayOfWeek.Friday),
                ReferencedRule = thirdMonday
            };

            var mondayAndNextDays = new SetUnionRule()
            {
                Rules = new IRule[]
                {
                    thirdMonday,
                    thuOrFri
                }
            };

            var date = new DateTime(year, month, day);
            mondayAndNextDays.IsMatch(date).Should().Be(expected);

        }

        [TestCase(2015, 3, 23, true)]
        [TestCase(2015, 3, 25, false)]
        public void EveryMonday(int year, int month, int day, bool expected)
        {
            //every monday
            var rule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Monday};

            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 12, 16, true)]
        [TestCase(2015, 12, 25, false)]
        public void The16thOfTheMonth(int year, int month, int day, bool expected)
        {
            //On the 16th of every month
            var rule = new NthInMonthRule {Nth = 16, ReferencedRule = new EveryDayRule()};
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 31, true)]
        [TestCase(2015, 3, 24, false)]
        [TestCase(2015, 3, 25, false)]
        public void LastTuesdayOfMonth(int year, int month, int day, bool expected)
        {
            //Last Tuesday of the month
            var rule = new NthInMonthRule {Nth = -1, ReferencedRule = new DayOfWeekRule {DayOfWeek = DayOfWeek.Tuesday}};
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 1, false)]
        [TestCase(2015, 3, 7, true)]
        [TestCase(2015, 3, 14, false)]
        public void TheFirstSaturdayOfMonth(int year, int month, int day, bool expected)
        {
            //1st Saturday of the month
            var rule = new NthInMonthRule {Nth = 1, ReferencedRule = new DayOfWeekRule {DayOfWeek = DayOfWeek.Saturday}};
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 1, false)]
        [TestCase(2015, 3, 2, true)]
        [TestCase(2015, 3, 3, false)]
        [TestCase(2015, 3, 14, false)]
        public void FirstWorkDayOfMonth(int year, int month, int day, bool expected)
        {
            //First workday of the month
            var rule = new NthInMonthRule {Nth = 1, ReferencedRule = new WorkDayRule()};
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        
        [TestCase(2015, 3, 2, true)]
        [TestCase(2015, 3, 9, false)]
        [TestCase(2015, 3, 16, true)]
        public void FirstAnThirdMondayOfMonth(int year, int month, int day, bool expected)
        {
            //First and Third Monday of the month
            var rule = new SetUnionRule
            {
                Rules = new[]
                {
                    new NthInMonthRule {Nth = 1, ReferencedRule = new DayOfWeekRule {DayOfWeek = DayOfWeek.Monday}},
                    new NthInMonthRule {Nth = 3, ReferencedRule = new DayOfWeekRule {DayOfWeek = DayOfWeek.Monday}}
                }
            };
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 2, false)]
        [TestCase(2015, 3, 6, false)]
        [TestCase(2015, 3, 9, false)]
        [TestCase(2015, 3, 16, true)]
        public void SecondMondayAfterFirstFriday(int year, int month, int day, bool expected)
        {
            //Second Monday after the first Friday of the month
            var rule = new NthBeforeAfterRule
            {
                Nth = 2,
                NthRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Monday},
                ReferencedRule =
                    new NthInMonthRule
                    {
                        Nth = 1,
                        ReferencedRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Friday}
                    }
            };
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 6, true)]
        [TestCase(2015, 6, 5, true)]
        [TestCase(2015, 9, 4, true)]
        [TestCase(2015, 12, 4, true)]
        public void FridayBefore2ndTuesdayOfGivenMonths(int year, int month, int day, bool expected)
        {
            //Friday before the 2nd Tuesday of each three months (March, June etc)
            var fridayBefore2ndTuesday = new NthBeforeAfterRule
            {
                Nth = -1,
                NthRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Friday},
                ReferencedRule =
                    new NthInMonthRule
                    {
                        Nth = 2,
                        ReferencedRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Tuesday}
                    }
            };

            var rule = new MonthsFilterRule
            {
                Months = new[] {Month.Mar, Month.Jun, Month.Sep, Month.Dec,},
                ReferencedRule = fridayBefore2ndTuesday
            };
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 3, 1, true)]
        [TestCase(2015, 3, 8, true)]
        [TestCase(2015, 3, 15, false)]
        [TestCase(2015, 3, 22, true)]
        [TestCase(2015, 3, 29, true)]
        public void EachSundayExceptTheThird(int year, int month, int day, bool expected)
        {
            //Each Sunday of Month (except the 3rd one)
            var rule = new SetDifferenceRule()
            {
                IncludeRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Sunday},
                ExcludeRule =
                    new NthInMonthRule
                    {
                        Nth = 3,
                        ReferencedRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Sunday}
                    }
            };
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);
        }

        [TestCase(2015, 1, 19, true)]
        [TestCase(2015, 1, 22, true)]
        [TestCase(2015, 1, 23, true)]

        [TestCase(2015, 4, 20, true)]
        [TestCase(2015, 4, 23, true)]
        [TestCase(2015, 4, 24, true)]

        [TestCase(2015, 7, 20, true)]
        [TestCase(2015, 7, 23, true)]
        [TestCase(2015, 7, 24, true)]

        [TestCase(2015, 10, 19, true)]
        [TestCase(2015, 10, 22, true)]
        [TestCase(2015, 10, 23, true)]

        [TestCase(2015, 12, 21, true)]
        [TestCase(2015, 12, 24, true)]
        [TestCase(2015, 12, 25, true)]

        public void The3rdMondayAndFollowingThursdayAndFridayOfGivenMonths_AsSeparateRules(int year, int month, int day, bool expected)
        {
            //The 3rd Monday and also Thursday && Friday after the 3rd Monday of January/April/July/October/December
            var thirdMonday = new NthInMonthRule
            {
                Nth = 3,
                ReferencedRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Monday}
            };
            var thirdMondayWithMonthsFilter = new MonthsFilterRule()
            {
                Months = new[] {Month.Jan, Month.Apr, Month.Jul, Month.Oct, Month.Dec},
                ReferencedRule = thirdMonday
            };

            var thursdayAfter = new NthBeforeAfterRule()
            {
                Nth = 1,
                NthRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Thursday},
                ReferencedRule = thirdMondayWithMonthsFilter
            };
            var fridayAfter = new NthBeforeAfterRule()
            {
                Nth = 1,
                NthRule = new DayOfWeekRule() {DayOfWeek = DayOfWeek.Friday},
                ReferencedRule = thirdMondayWithMonthsFilter
            };

            var rule = new SetUnionRule
            {
                Rules = new IRule[] {thirdMondayWithMonthsFilter, thursdayAfter, fridayAfter}
            };
            rule.IsMatch(new DateTime(year, month, day)).Should().Be(expected);

        }
    }
}