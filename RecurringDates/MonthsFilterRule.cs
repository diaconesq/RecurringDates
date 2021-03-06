using System;
using System.Collections.Generic;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class MonthsFilterRule : UnaryRule
    {
        public IEnumerable<Month> Months { get; set; }
        
        public override bool IsMatch(DateTime day)
        {
            var intersectRule = CreateIntersectionRule();
            return intersectRule.IsMatch(day);
        }

        private SetIntersectionRule CreateIntersectionRule()
        {
            var intersectRule = ReferencedRule.IfAlso(
                new MonthsRule()
                {
                    Months = Months
                });
            
            return intersectRule;
        }

        public override string GetDescription()
        {
            return "{0} in {1}".Fmt(ReferencedRule, Months.StringJoin(", "));
        }
    }
}