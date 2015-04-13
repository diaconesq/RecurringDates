using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class SetIntersectionRule : BaseRule
    {
        public SetIntersectionRule()
        {
            Rules = Enumerable.Empty<BaseRule>();
        }
        public override bool IsMatch(DateTime day)
        {
            return Rules.All(r => r.IsMatch(day));
        }

        public override string GetDescription()
        {
            return "(is " + Rules.StringJoin(" and also is ") + ")";
        }

        public IEnumerable<IRule> Rules { get; set; }
    }
}