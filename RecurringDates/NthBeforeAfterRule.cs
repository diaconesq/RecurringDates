using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public enum Direction
    {
        before,
        after
    }

    public class NthBeforeAfterRule : UnaryRule
    {
        private const int MaxSearchHorizon = 100; //days
        public int Nth { get; set; }
        public virtual IRule NthRule { get; set; }
        public int NthAbsolute
        {
            get { return Math.Abs(Nth); }
        }

        public override string GetDescription()
        {
            return "{0} {1} {2} {3}".Fmt(NthAbsolute.ToOrdinalString(), NthRule, Direction, ReferencedRule);
        }

        public override bool IsMatch(DateTime day)
        {
            if (Nth == 0)
            {
                throw new InvalidOperationException("Nth rule can't count to zero");
            }
            //first, the current day must match the rule (e.g. 2nd monday after xxx must be a Monday!)
            if (!NthRule.IsMatch(day))
            {
                return false;
            }

            var ruleWithAlternatives = NthRule as IHasAlternateRules;
            var nthRules = ruleWithAlternatives != null ? ruleWithAlternatives.Rules : new[] {NthRule};
            
            var searchDays = nthRules.SelectMany(crtRule => GetCandidateDays(day, crtRule))
                .Distinct() // skip duplicate days
                ;

            return searchDays.Any(d => ReferencedRule.IsMatch(d)); //in the search interval, there's a day matching the guiding rule
        }

        private IEnumerable<DateTime> GetCandidateDays(DateTime day, IRule crtNth)
        {
            //find the pivot day (the 1st one that matches NthRule
            var pivotDay = NthAbsolute == 1
                ? day //we're searching for the first occurence; the day IS the pivot
                : (Direction == Direction.after ? day.DaysBefore() : day.DaysAfter()) //search in the opposite direction
                    .Take(MaxSearchHorizon) // don't go into an infinite loop
                    .Where(crtNth.IsMatch) //find previous matches
                    .Skip(NthAbsolute - 2).First(); //take the 1st (of N) matches

            //Now search for a reference match that occurs closer to the pivot than the next occurence of NthRule
            var searchDays = (Direction == Direction.after ? pivotDay.DaysBefore() : pivotDay.DaysAfter())
                //search before or after
                .Take(MaxSearchHorizon)
                .TakeUntilIncluding(crtNth.IsMatch)
                ;
            return searchDays;
        }

        public Direction Direction
        {
            get
            {
                if (Nth > 0) 
                    return Direction.after;
                if (Nth < 0) 
                    return Direction.before;
                throw new NotSupportedException("'Nth' cannot be zero");
            }
        }
    }

    public static class NthBeforeAfterRuleHelpers
    {
        public static IEnumerable<T> TakeUntilIncluding<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (T el in list)
            {
                yield return el;
                if (predicate(el))
                    yield break;
            }
        }
        public static IEnumerable<DateTime> DaysBefore(this DateTime date)
        {
            return EveryNDays(date, -1);
        }
        public static IEnumerable<DateTime> DaysAfter(this DateTime date)
        {
            return EveryNDays(date, 1);
        }
        public static IEnumerable<DateTime> DaysBeforeIncludingSelf(this DateTime date)
        {
            return EveryNDays(date, -1, true);
        }
        public static IEnumerable<DateTime> DaysAfterIncludingSelf(this DateTime date)
        {
            return EveryNDays(date, 1, true);
        }
        private static IEnumerable<DateTime> EveryNDays(DateTime date, int N, bool including = false)
        {
            var crtDate = date;
            if (including)
                yield return date;

            while (true)
            {
                crtDate += TimeSpan.FromDays(N);
                yield return crtDate;
            }
        }

    }
}