using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RecurringDates
{
    public interface IEnumerableRule : IRule
    {
        IEnumerable<DateTime> MatchingDates { get; }
    }
}