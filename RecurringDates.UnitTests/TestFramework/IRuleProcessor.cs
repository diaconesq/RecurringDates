namespace RecurringDates.UnitTests
{
    /// <summary>
    /// Given a rule, return another rule.
    /// </summary>
    public interface IRuleProcessor
    {
        U Process<U>(U rule) where U : IRule;
    }
}