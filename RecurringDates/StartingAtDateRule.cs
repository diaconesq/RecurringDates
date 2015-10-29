using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public class StartingAtDateRule : UnaryRule, IEnumerableRule
    {
        [DataMember]
        public DateTime StartDate { get; private set; }

        internal StartingAtDateRule(DateTime startDate)
        {
            StartDate = startDate.Date;
        }

        public override bool IsMatch(DateTime day)
        {
            return day >= StartDate && ReferencedRule.IsMatch(day);
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
                return StartDate.DaysAfterIncludingSelf()
                    .Where(IsMatch);
            }
        }
    }
}