using RecurringDates.Serialization;

namespace RecurringDates.UnitTests.Serialization
{
    class SerializeDeserializeProcessor : IRuleProcessor
    {
        public IRule Process(IRule rule)
        {
            var serializedString = new RuleSerializer().Serialize(rule);
            var deserializedRule = new RuleSerializer().Deserialize(serializedString);
            return deserializedRule;
        }
    }
}