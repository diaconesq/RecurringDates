using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public abstract class UnaryRule : BaseRule
    {
        /// <summary>
        /// The rule on which an operation is to be applied
        /// </summary>
        [DataMember]
        public virtual IRule ReferencedRule { get; set; }
    }
}