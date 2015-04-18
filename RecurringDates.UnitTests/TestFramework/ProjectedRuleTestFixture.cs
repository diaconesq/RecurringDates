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
    /// <typeparam name="T">a type that implements IRuleProjection</typeparam>
    [TestFixture(typeof(IdentityProjection))]
    [TestFixture(typeof(SerializeDeserializeProjection))]
    public abstract class ProjectedRuleTestFixture<T> where T: IRuleProjection, new()
    {
        private readonly T _projection;

        protected ProjectedRuleTestFixture()
        {
            _projection = new T();
        }

        private void AssertMatch(IRule rule, DateTime date, bool expectedResult)
        {
            var projectedRule = _projection.Project(rule);
            projectedRule.IsMatch(date).Should().Be(expectedResult);
        }

        public void AssertIsMatch(IRule rule, DateTime date)
        {
            AssertMatch(rule, date, true); 
        }
        public void AssertIsNotMatch(IRule rule, DateTime date)
        {
            AssertMatch(rule, date, false);
        }
    }
}