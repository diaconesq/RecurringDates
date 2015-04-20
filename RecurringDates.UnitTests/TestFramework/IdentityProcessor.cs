namespace RecurringDates.UnitTests
{
    class IdentityProcessor : IRuleProcessor
    {
        public IRule Process(IRule rule)
        {
            return rule;
        }
    }
}