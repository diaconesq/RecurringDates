using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class SetUnionRule : NaryRule
    {
        public override bool IsMatch(DateTime day)
        {
            return Rules.Any(r => r.IsMatch(day));
        }
        public override string GetDescription()
        {
            return Rules.StringJoin(" or ", "(", ")");
        }

        public override IEnumerator<IRule> GetEnumerator()
        {
            return Rules.GetEnumerator();
        }
    }
}