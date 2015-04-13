using System;
using System.Collections.Generic;

namespace RecurringDates
{
    public interface IRule
    {
        bool IsMatch(DateTime day);
        string GetDescription();
    }
}