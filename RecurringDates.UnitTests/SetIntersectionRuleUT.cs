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

            var andRule = new SetIntersectionRule() { Rules = new[] { leftRule, rightRule } };

            andRule.IsMatch(new DateTime()).Should().Be(expected);
        }
    }
}
