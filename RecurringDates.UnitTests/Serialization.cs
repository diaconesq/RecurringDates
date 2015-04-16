using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using FluentAssertions;
using RecurringDates.Serialization;

namespace RecurringDates.UnitTests
{
    [TestFixture]
    public class Serialization
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
