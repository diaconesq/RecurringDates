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



            var serialized = Serialize(rule);

            var rehydratedRule = Deserialize(serialized);

            rehydratedRule.Should().NotBeNull();
        }

        private static IRule Deserialize(string serialized)
        {

            var IRuleTypes = typeof (IRule).Assembly.DefinedTypes
                .Where(t => typeof (IRule).IsAssignableFrom(t))
                ;

            DataContractSerializer serializer = new DataContractSerializer(typeof(IRule), IRuleTypes);
            var xmlReader = XmlReader.Create(new StringReader(serialized));
            var obj = serializer.ReadObject(xmlReader);

            return (IRule) obj;

            //var deserializer = new XmlSerializer(typeof (IRule));
            //var sr = new StringReader(serialized);

            //var rehydratedRule = (IRule)deserializer.Deserialize(sr);
            //return rehydratedRule;
        }

        private static string Serialize(IRule rule)
        {
            var IRuleTypes = typeof (IRule).Assembly.DefinedTypes
                .Where(t => typeof (IRule).IsAssignableFrom(t))
                ;

            DataContractSerializer serializer = new DataContractSerializer(typeof(IRule), IRuleTypes);
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            serializer.WriteObject(xw, rule);
            return sw.ToString();

            //var serializer = new XmlSerializer(rule.GetType());
            //var sw = new StringWriter();
            //serializer.Serialize(sw, rule);

            var serialized = sw.ToString();
            return serialized;
        }
    }
}
