using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class NthInMonthRule : UnaryRule
    {
        /// <summary>
        /// Zero is not allowed
        /// Positive values: Nth occurrence from the beginning of month
        /// Negative values: Nth occurrence backwards from the end of month
        /// </summary>

        public int Nth { get; set; }
        public int NthAbsolute
        {
            get { return Math.Abs(Nth); }
        }

        public override bool IsMatch(DateTime day)
        {
            if (Nth == 0)
            {
                throw new InvalidOperationException("Nth rule can't count to zero");
            }

            var daysInMonth = Nth > 0 ? DaysInMonthAscending(day) : DaysInMonthDescending(day);
            var alternateRules = ReferencedRule as IHasAlternateRules;
            IEnumerable<IRule> rulesThatCanMatch ;
            if (alternateRules != null)
            {
                rulesThatCanMatch = alternateRules.Rules;
            }
            else
            {
                rulesThatCanMatch = new[] {ReferencedRule};
            }

            foreach (var subRule in rulesThatCanMatch)
            try
            {
                var rule = subRule;
                var matches = daysInMonth.Where(d => rule.IsMatch(d));

                var nthMatch = matches.Skip(NthAbsolute - 1).First();

                if (nthMatch == day)
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;

        }

        public override string GetDescription()
        {
            return "{0} {1} of the month".Fmt(Nth.ToOrdinalString(), ReferencedRule);
        }

        internal IEnumerable<DateTime> DaysInMonthAscending(DateTime day)
        {
            var daysInMonth = DateTime.DaysInMonth(day.Year, day.Month);

            for (int crtDay = 1; crtDay <= daysInMonth; crtDay++)
            {
                var crtDate = new DateTime(day.Year, day.Month, crtDay);
                yield return crtDate;
            }
        }

        internal IEnumerable<DateTime> DaysInMonthDescending(DateTime day)
        {
            var daysInMonth = DateTime.DaysInMonth(day.Year, day.Month);

            for (int crtDay = daysInMonth; crtDay >= 1; crtDay--)
            {
                var crtDate = new DateTime(day.Year, day.Month, crtDay);
                yield return crtDate;
            }
        }
    }
}