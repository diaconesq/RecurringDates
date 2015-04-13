using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringDates
{
    public static class Extensions
    {
        public static SetUnionRule Or(this IRule first, params IRule[] otherRules)
        {
            return new SetUnionRule {Rules = new[] {first}.Concat(otherRules)};
        }

        public static SetIntersectionRule And(this IRule first, params IRule[] otherRules)
        {
            return new SetIntersectionRule { Rules = new[] { first }.Concat(otherRules) };
        }

        public static SetDifferenceRule Except(this IRule includeRule, IRule excludeRule)
        {
            return new SetDifferenceRule { IncludeRule = includeRule , ExcludeRule = excludeRule };
        }

        public static NotRule Not(this IRule rule)
        {
            return new NotRule { ReferencedRule = rule };
        }

        public static NthInMonthRule NthInMonth(this IRule rule, int nthOccurrence)
        {
            return new NthInMonthRule {Nth = nthOccurrence, ReferencedRule = rule};
        }

        public static MonthsFilterRule InMonths(this IRule rule, params Month[] months)
        {
            return new MonthsFilterRule { Months = months, ReferencedRule = rule };
        }

        public static DayOfWeekRule EveryWeek(this DayOfWeek dow)
        {
            return new DayOfWeekRule(dow);
        }

        public static IRule TheNthOccurenceInTheMonth(this IRule rule, int Nth)
        {
            return new NthInMonthRule() { Nth = Nth, ReferencedRule = rule };
        }

        public static IRule The1stOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(1);
        }
        public static IRule The2ndOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(2);
        }
        public static IRule The3rdOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(3);
        }
        public static IRule The4thOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(4);
        }
        public static IRule The5thOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(5);
        }
        public static IRule TheLastOccurenceInTheMonth(this IRule rule)
        {
            return rule.TheNthOccurenceInTheMonth(-1);
        }
    }
}
