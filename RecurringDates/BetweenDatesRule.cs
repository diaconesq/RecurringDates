using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RecurringDates
{
    [DataContract]
    public class BetweenDatesRule : UnaryRule, IEnumerableRule
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public BetweenDatesRule(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override bool IsMatch(DateTime day)
        {
            return _startDate <= day.Date && day.Date <= _endDate
                   && ReferencedRule.IsMatch(day);

        }

        public IEnumerable<DateTime> MatchingDates
        {
            get
            {
                return _startDate.UpTo(_endDate)
                    .Where(IsMatch);
            }
        }
    }
}