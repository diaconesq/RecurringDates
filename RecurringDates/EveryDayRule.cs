using System;

namespace RecurringDates
{
    public class EveryDayRule : BaseRule
    {
        public override bool IsMatch(DateTime day)
        {
            return true;
        }

        public override string GetDescription()
        {
            return "day";
        }
    }
}