using System;

namespace RecurringDates
{
    public class DayOfWeekRule : BaseRule
    {
        public override bool IsMatch(DateTime day)
        {
            return day.DayOfWeek == DayOfWeek;
        }

        public override string GetDescription()
        {
            return DayOfWeek.ToString();
        }

        public DayOfWeek DayOfWeek { get; set; }
    }
}