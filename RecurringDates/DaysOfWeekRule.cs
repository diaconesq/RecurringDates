using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using RecurringDates.Helpers;

namespace RecurringDates
{
    [DataContract]
    public class DaysOfWeekRule : BaseRule, IHasAlternateRules
    {
        [DataMember]
        private readonly SetUnionRule _rule;

        [DataMember]
        public IEnumerable<DayOfWeek> DaysOfWeek { get; private set; }

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

        public override bool IsMatch(DateTime day)
        {
            return _rule.IsMatch(day);
        }

        public override string GetDescription()
        {
            return DaysOfWeek.StringJoin(" or ");
        }

        public IEnumerable<IRule> Rules
        {
            get { return _rule.Rules; }
            set { _rule.Rules = value; }
        }
    }
}