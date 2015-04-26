using System;
using FluentAssertions;
using NUnit.Framework;
using RecurringDates.Serialization;

namespace RecurringDates.UnitTests.Serialization
{
    [TestFixture]
    public class SerializationUT
    {
        [Test]
        public void AComplexRule_RoundTrips_WhenSerialized()
        {
            var anyThirdMonday = DayOfWeek.Monday.EveryWeek().The3rdOccurenceInTheMonth();

            var rule = anyThirdMonday.InMonths(Month.Jan, Month.Apr, Month.Jul, Month.Oct, Month.Dec);

            var serialized = RuleSerializer.Instance.Serialize(rule);

            var rehydratedRule = RuleSerializer.Instance.Deserialize(serialized);

            rehydratedRule.Should().NotBeNull();
        }

        [Test]
        public void ANullRuleIsSerialized()
        {
            var result = RuleSerializer.Instance.Serialize(null);
            var rule = RuleSerializer.Instance.Deserialize(result);
            rule.Should().BeNull();
        }
        [Test]
        public void ACustomRuleIsSerialized_WhenSpecifyingItsAssembly()
        {
            var rule = new CustomRule();
            var result = new RuleSerializer().Serialize(rule, rule.GetType().Assembly);
            var deserializedRule = new RuleSerializer().Deserialize(result, rule.GetType().Assembly);
            deserializedRule.Should().NotBeNull();
        }
        [Test]
        public void ACustomRuleIsSerialized_WhenSpecifyingItsType()
        {
            var rule = new CustomRule();
            var result = new RuleSerializer().Serialize(rule, rule.GetType());
            var deserializedRule = new RuleSerializer().Deserialize(result, rule.GetType());
            deserializedRule.Should().NotBeNull();
        }

    }

    public class CustomRule : IRule
    {
        public bool IsMatch(DateTime day)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }
    }
}
