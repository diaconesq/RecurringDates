using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class SetDifferenceRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProjection, new()
    {
        [TestCase(2015, 4, 3, true)]
        [TestCase(2015, 4, 10, false)]
        [TestCase(2015, 4, 17, true)]
        [TestCase(2015, 4, 24, true)]
        public void FridaysExceptThe10thOfTheMonth(int year, int month, int day, bool expected)
        {
            var rule = DayOfWeek.Friday.EveryWeek()
                .Except(new EveryDayRule().TheNthOccurenceInTheMonth(10))
                ;
            Project(rule).IsMatch(new DateTime(year, month, day))
                .Should().Be(expected);
        }
    }

    [TestFixture]
    public class SetDifferenceRuleUT
    {
        [TestCase(true, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        public void SetDifferenceRule_MatchesAllChildRules(bool left, bool right, bool expected)
        {
            var leftRule = Substitute.For<IRule>();
            leftRule.IsMatch(new DateTime()).ReturnsForAnyArgs(left);

            var rightRule = Substitute.For<IRule>();
            rightRule.IsMatch(new DateTime()).ReturnsForAnyArgs(right);

            var differenceRule = new SetDifferenceRule() {IncludeRule = leftRule, ExcludeRule = rightRule};

            differenceRule.IsMatch(new DateTime()).Should().Be(expected);
        }
    }
}