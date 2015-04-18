using RecurringDates.Serialization;

namespace RecurringDates.UnitTests.Serialization
{
    class SerializeDeserializeProjection : IRuleProjection
    {
        public IRule Project(IRule rule)
        {
            var serializedString = new RuleSerializer().Serialize(rule);
            var deserializedRule = new RuleSerializer().Deserialize(serializedString);
            return deserializedRule;
        }
    }
}