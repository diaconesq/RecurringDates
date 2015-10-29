using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurringDates
{
    public static class Extensions
    {
        public static SetUnionRule Or(this IRule first, params IRule[] otherRules)
        {
            return new SetUnionRule {Rules = new[] {first}.Concat(otherRules)};
        }

        public static SetIntersectionRule IfAlso(this IRule first, params IRule[] otherRules)
        {
            return new SetIntersectionRule {Rules = new[] {first}.Concat(otherRules)};
        }

        public static SetDifferenceRule Except(this IRule includeRule, IRule excludeRule)
        {
            return new SetDifferenceRule {IncludeRule = includeRule, ExcludeRule = excludeRule};
        }

        public static NotRule Not(this IRule rule)
        {
            return new NotRule {ReferencedRule = rule};
        }

        public static NthInMonthRule NthInMonth(this IRule rule, int nthOccurrence)
        {
            return new NthInMonthRule {Nth = nthOccurrence, ReferencedRule = rule};
        }

        public static MonthsFilterRule InMonths(this IRule rule, params Month[] months)
        {
            return new MonthsFilterRule {Months = months, ReferencedRule = rule};
        }

        public static DayOfWeekRule EveryWeek(this DayOfWeek dow)
        {
            return new DayOfWeekRule(dow);
        }

        public static IRule TheNthOccurenceInTheMonth(this IRule rule, int Nth)
        {
            return new NthInMonthRule() {Nth = Nth, ReferencedRule = rule};
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

        /// <summary>
        /// Returns a rule that only matches dates at or after a specified start date,
        /// in addition to the original rule.
        /// Warning! This is an infinite list. When consuming it, apply a limiting criteria
        /// such as <code>rule.MatchingDates.Take(100);</code>
        /// </summary>
        public static IEnumerableRule Starting(this IRule rule, DateTime startDateTime)
        {
            return new StartingAtDateRule(startDateTime) {ReferencedRule = rule};
        }

        /// <summary>
        /// Returns a rule that only matches dates at or after a specified start date,
        /// in addition to the original rule.
        /// This is a potentially large list, 
        /// depending on the number of days between startDate and endDate, and the referenced rule. 
        /// Consume in moderation.
        /// </summary>
        public static IEnumerableRule Between(this IRule rule, DateTime startDate, DateTime endDate)
        {
            return new BetweenDatesRule(startDate, endDate) {ReferencedRule = rule};
        }
    }
    
    public static class DateTimeExtensions 
    {
        public static IEnumerable<DateTime> UpTo(this DateTime startDate, DateTime endDate)
        {
            return startDate.DaysAfterIncludingSelf()
                .TakeWhile(day => day <= endDate.Date);
        }
        public static IEnumerable<DateTime> UpToExcluding(this DateTime startDate, DateTime endDate)
        {
            return startDate.DaysAfterIncludingSelf()
                .TakeWhile(day => day < endDate.Date);
        } 
    }
}
