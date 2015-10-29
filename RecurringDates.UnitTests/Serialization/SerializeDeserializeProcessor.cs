using RecurringDates.Serialization;

namespace RecurringDates.UnitTests.Serialization
{
    class SerializeDeserializeProcessor : IRuleProcessor
    {
        public U Process<U>(U rule) where U: IRule
        {
            var serializedString = new RuleSerializer().Serialize(rule);
            var deserializedRule = new RuleSerializer().Deserialize<U>(serializedString);
            return deserializedRule;
        }
    }
}