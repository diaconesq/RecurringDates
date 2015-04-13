using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class DaysOfWeekRule : BaseRule
    {
        private readonly SetUnionRule _rule;

        public DaysOfWeekRule(params DayOfWeek[] daysOfWeek)
            : this((IEnumerable<DayOfWeek>) daysOfWeek)
        {
        }

        public DaysOfWeekRule(IEnumerable<DayOfWeek> daysOfWeek)
        {
            var daysOfWeekArray = daysOfWeek as DayOfWeek[] ?? daysOfWeek.ToArray();
            DaysOfWeek = daysOfWeekArray;
            _rule = new SetUnionRule()
            {
                Rules = daysOfWeekArray.Select(day => new DayOfWeekRule(day))
            };
        }

        public IEnumerable<DayOfWeek> DaysOfWeek { get; private set; }

        public override bool IsMatch(DateTime day)
        {
            return _rule.IsMatch(day);
        }

        public override string GetDescription()
        {
            return DaysOfWeek.StringJoin(" or ");
        }

        public override IEnumerator<IRule> GetEnumerator()
        {
            return _rule.GetEnumerator();
        }
    }
}