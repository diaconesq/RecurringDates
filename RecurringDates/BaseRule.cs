using System;
using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public abstract class BaseRule : IRule
    {
        public abstract bool IsMatch(DateTime day);
        public virtual string GetDescription()
        {
            return this.GetType().Name;
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}