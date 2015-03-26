using System;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class SetIntersectionRule : NaryRule
    {
        public override bool IsMatch(DateTime day)
        {
            return Rules.All(r => r.IsMatch(day));
        }

        public override string GetDescription()
        {
            return "(is " + Rules.StringJoin(" and also is ") + ")";
        }
    }
}