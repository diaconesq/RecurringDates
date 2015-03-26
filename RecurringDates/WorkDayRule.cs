using System;

namespace RecurringDates
{
    public class WorkDayRule : BaseRule
    {
        public override bool IsMatch(DateTime day)
        {
            return day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday;
        }

        public override string GetDescription()
        {
            return "work days";
        }
    }
}