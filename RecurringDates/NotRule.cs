using System;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class NotRule : UnaryRule
    {
        public override bool IsMatch(DateTime day)
        {
            return !ReferencedRule.IsMatch(day);
        }

        public override string GetDescription()
        {
            return "not ({0})".Fmt(ReferencedRule);
        }
    }
}