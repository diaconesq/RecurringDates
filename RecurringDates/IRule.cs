using System;
using System.Collections.Generic;

namespace RecurringDates
{
    public interface IRule:IEnumerable<IRule>
    {
        bool IsMatch(DateTime day);
        string GetDescription();
    }
}