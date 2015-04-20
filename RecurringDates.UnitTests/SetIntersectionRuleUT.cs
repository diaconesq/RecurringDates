using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class SetIntersectionRuleUT
    {

        [TestCase(true, true, true)]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        public void SetIntersectionRule_MatchesAllChildRules(bool left, bool right, bool expected)
        {
            var leftRule = Substitute.For<IRule>();
            leftRule.IsMatch(new DateTime()).ReturnsForAnyArgs(left);

            var rightRule = Substitute.For<IRule>();
            rightRule.IsMatch(new DateTime()).ReturnsForAnyArgs(right);

            var andRule = new SetIntersectionRule() {Rules = new[] {leftRule, rightRule}};

            andRule.IsMatch(new DateTime()).Should().Be(expected);
        }
    }

    [TestFixture]
    public class SetIntersectionRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProjection, new()
    {

        [TestCase(2015, 4, 5, false)]
        [TestCase(2015, 4, 3, false)]
        [TestCase(2015, 6, 5, true)]
        public void The5thOfTheMonthIfItIsAFriday(int year, int month, int day, bool expected)
        {
            var rule = new EveryDayRule().TheNthOccurenceInTheMonth(5)
                .IfAlso(DayOfWeek.Friday.EveryWeek());

            Project(rule).IsMatch(new DateTime(year, month, day))
                .Should().Be(expected);
        }
    }
}
