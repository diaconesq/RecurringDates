namespace RecurringDates.UnitTests
{
    class IdentityProjection : IRuleProjection
    {
        public IRule Project(IRule rule)
        {
            return rule;
        }
    }
}