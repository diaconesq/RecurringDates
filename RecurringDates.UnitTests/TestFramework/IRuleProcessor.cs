namespace RecurringDates.UnitTests
{
    /// <summary>
    /// Given a rule, return another rule.
    /// </summary>
    public interface IRuleProcessor
    {
        IRule Process(IRule rule);
    }
}