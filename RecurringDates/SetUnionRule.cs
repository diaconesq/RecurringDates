using System;
using System.Collections.Generic;
using System.Linq;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class SetUnionRule : BaseRule, IHasAlternateRules
    {
        public SetUnionRule()
        {
            Rules = Enumerable.Empty<BaseRule>();
        }
        public override bool IsMatch(DateTime day)
        {
            return Rules.Any(r => r.IsMatch(day));
        }
        public override string GetDescription()
        {
            return Rules.StringJoin(" or ", "(", ")");
        }

        public virtual IEnumerator<IRule> GetEnumerator()
        {
            return Rules.GetEnumerator();
        }

        public IEnumerable<IRule> Rules { get; set; }
    }

    public interface IHasAlternateRules
    {
        IEnumerable<IRule> Rules { get; set; }
    }
}