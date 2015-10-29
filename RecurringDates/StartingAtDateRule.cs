using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public class StartingAtDateRule : UnaryRule, IEnumerableRule
    {
        private readonly DateTime _startDate;

        internal StartingAtDateRule(DateTime startDate)
        {
            _startDate = startDate.Date;
        }

        public override bool IsMatch(DateTime day)
        {
            return day >= _startDate && ReferencedRule.IsMatch(day);
        }

        /// <summary>
        /// Returns all dates at or after the starting date, that match the specified rule.
        /// Warning! this is an infinite list. When consuming it, apply a limiting criteria
        /// such as <code>rule.MatchingDates.Take(100);</code>
        /// </summary>
        public IEnumerable<DateTime> MatchingDates
        {
            get
            {
                return _startDate.DaysAfterIncludingSelf()
                    .Where(IsMatch);
            }
        }
    }
}