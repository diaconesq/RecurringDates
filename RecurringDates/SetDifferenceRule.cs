using System;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class SetDifferenceRule : BaseRule
    {
        public IRule IncludeRule { get; set; }
        public IRule ExcludeRule { get; set; }
        
        public override bool IsMatch(DateTime day)
        {
            return IncludeRule.IsMatch(day) && !ExcludeRule.IsMatch(day);
        }

        public override string GetDescription()
        {
            return "({0}), except ({1})".Fmt(IncludeRule, ExcludeRule);
        }
    }
}