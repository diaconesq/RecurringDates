using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace RecurringDates.UnitTests
{
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

            var andRule = new SetDifferenceRule() { IncludeRule = leftRule, ExcludeRule = rightRule };

            andRule.IsMatch(new DateTime()).Should().Be(expected);
        }
    }
}