using System;
using FluentAssertions;
using NUnit.Framework;
using RecurringDates.UnitTests.Serialization;

namespace RecurringDates.UnitTests
{
    /// <summary>
    /// Base test fixture class that supports testing a rule against a date. 
    /// The rule is tested both 'as is' and also after a serialize/deserialize roundtrip, 
    /// to make sure the rule is not modified by serialization
    /// </summary>
    /// <typeparam name="T">a type that implements IRuleProcessor</typeparam>
    [TestFixture(typeof(IdentityProcessor))]
    [TestFixture(typeof(SerializeDeserializeProcessor))]
    public abstract class ProjectedRuleTestFixture<T> :IRuleProcessor
        where T: IRuleProcessor, new()
    {
        private readonly T _projection;

        protected ProjectedRuleTestFixture()
        {
            _projection = new T();
        }

        private void AssertMatch(IRule rule, DateTime date, bool expectedResult)
        {
            var projectedRule = _projection.Process(rule);
            Process(projectedRule).IsMatch(date).Should().Be(expectedResult);
        }


        public U Process<U>(U rule) where U : IRule
        {
            return _projection.Process(rule);
        }
    }
}