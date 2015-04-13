using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringDates
{
    public static class Extensions
    {
        public static SetUnionRule Or(this IRule first, params IRule[] otherRules)
        {
            return new SetUnionRule {Rules = new[] {first}.Concat(otherRules)};
        }

        public static SetIntersectionRule And(this IRule first, params IRule[] otherRules)
        {
            return new SetIntersectionRule { Rules = new[] { first }.Concat(otherRules) };
        }

        public static SetDifferenceRule Except(this IRule includeRule, IRule excludeRule)
        {
            return new SetDifferenceRule { IncludeRule = includeRule , ExcludeRule = excludeRule };
        }

        public static NotRule Not(this IRule rule)
        {
            return new NotRule { ReferencedRule = rule };
        }

    }
}
