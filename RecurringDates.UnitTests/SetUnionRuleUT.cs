using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
    public class SetUnionRuleUT<T> : ProjectedRuleTestFixture<T> where T : IRuleProcessor, new()
    {

        [TestCase(2015, 3, 31, true)]
        [TestCase(2015, 3, 30, true)]
        [TestCase(2015, 3, 1, false)]
        [TestCase(2015, 4, 1, false)]
        public void MondayOrTuesday(int year, int month, int day, bool expected)
        {
            var rule = DayOfWeek.Monday.EveryWeek()
                .Or(DayOfWeek.Tuesday.EveryWeek());

            var date = new DateTime(year, month, day);

            Process(rule).IsMatch(date).Should().Be(expected);
        }

    }

    [TestFixture]
    public class SetUnionRuleUT
    {
        [TestCase(true, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        [TestCase(false, false, false)]
        public void SetUnionRule_MatchesAllChildRules(bool left, bool right, bool expected)
        {
            var leftRule = Substitute.For<IRule>();
            leftRule.IsMatch(new DateTime()).ReturnsForAnyArgs(left);

            var rightRule = Substitute.For<IRule>();
            rightRule.IsMatch(new DateTime()).ReturnsForAnyArgs(right);

            var andRule = new SetUnionRule() { Rules = new[] { leftRule, rightRule } };

            andRule.IsMatch(new DateTime()).Should().Be(expected);
        }
    }
}