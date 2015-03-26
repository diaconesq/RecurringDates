namespace RecurringDates
{
    public abstract class UnaryRule : BaseRule
    {
        /// <summary>
        /// The rule on which an operation is to be applied
        /// </summary>
        public virtual IRule ReferencedRule { get; set; }
    }
}