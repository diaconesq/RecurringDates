using System;
using System.Collections;
using System.Collections.Generic;

namespace RecurringDates
{
    public abstract class BaseRule : IRule
    {
        public abstract bool IsMatch(DateTime day);
        public virtual string GetDescription()
        {
            return this.GetType().Name;
        }

        public virtual IEnumerator<IRule> GetEnumerator()
        {
            yield return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return GetDescription();
        }

    }
}