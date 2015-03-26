using System.Collections.Generic;
using System.Linq;

namespace RecurringDates
{
    public abstract class NaryRule : BaseRule
    {
        protected NaryRule()
        {
            Rules = Enumerable.Empty<BaseRule>();
        }
        public IEnumerable<IRule> Rules { get; set; }
    }
}