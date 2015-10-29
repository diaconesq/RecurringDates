using System;
using System.Runtime.Serialization;
using RecurringDates.Helpers;

namespace RecurringDates
{
    [DataContract]
    public class DayInMonthRule : BaseRule
    {
        [DataMember]
        public int DayInMonth { get; private set; }
        [DataMember]
        public Month Month { get; private set; }

        public DayInMonthRule(int dayInMonth, Month month)
        {
            DayInMonth = dayInMonth;
            Month = month;
        }

        public override bool IsMatch(DateTime day)
        {
            return day.Day == DayInMonth && day.Month == (int) Month;
        }

        public override string GetDescription()
        {
            return Month + " " + DayInMonth.ToOrdinalString();
        }
    }
}