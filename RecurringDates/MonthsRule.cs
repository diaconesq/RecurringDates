using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class MonthsRule : UnaryRule
    {
        public IEnumerable<Month> Months { get; set; }

        public override bool IsMatch(DateTime day)
        {
            return Months.Any(month => (int) month == day.Month);
        }

        public override string GetDescription()
        {
            return "in " + Months.StringJoin(" or ");
        }
    }
}