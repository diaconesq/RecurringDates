namespace RecurringDates.UnitTests
{
    /// <summary>
    /// Given a rule, return another rule.
    /// </summary>
    public interface IRuleProjection
    {
        IRule Project(IRule rule);
    }
}