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

    }
}
