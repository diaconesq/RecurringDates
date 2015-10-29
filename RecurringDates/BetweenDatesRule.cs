using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public class BetweenDatesRule : UnaryRule, IEnumerableRule
    {
        [DataMember]
        public DateTime StartDate { get; private set; }
        [DataMember]
        public DateTime EndDate { get; private set; }

        public BetweenDatesRule(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public override bool IsMatch(DateTime day)
        {
            return StartDate <= day.Date && day.Date <= EndDate
                   && ReferencedRule.IsMatch(day);

        }

        public IEnumerable<DateTime> MatchingDates
        {
            get
            {
                return StartDate.UpTo(EndDate)
                    .Where(IsMatch);
            }
        }
        public override string GetDescription()
        {
            return ReferencedRule.GetDescription() + string.Format(" between {0:d} and {1:d}", StartDate, EndDate);
        }

    }
}