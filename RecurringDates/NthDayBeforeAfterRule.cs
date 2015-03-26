using System;
using RecurringDates.Helpers;

namespace RecurringDates
{
    public class NthDayBeforeAfterRule : UnaryRule
    {
        /// <summary>
        /// zero is an invalid value
        /// Positive values: Nth day after
        /// Negative values: Nth day before
        /// </summary>
        public int Nth { get; set; }

        public override bool IsMatch(DateTime day)
        {
            return ReferencedRule.IsMatch(day - TimeSpan.FromDays(Nth));
        }

        public int NthAbsolute
        {
            get { return Math.Abs(Nth); }
        }

        public Direction Direction
        {
            get
            {
                if (Nth > 0)
                    return Direction.after;
                if (Nth < 0)
                    return Direction.before;
                throw new NotSupportedException("'Nth' cannot be zero");
            }
        }

        public override string GetDescription()
        {
            return "{0} day {1} {2}".Fmt(NthAbsolute.ToOrdinalString(), Direction, ReferencedRule);
        }

    }

    //same result as above using NthBeforeAfterRule
    public class NthDayBeforeAfterRule2 : UnaryRule
    {
        private static readonly EveryDayRule EveryDayRule = new EveryDayRule();
        public int Nth { get; set; }

        public override bool IsMatch(DateTime day)
        {
            var shiftRule = new NthBeforeAfterRule()
            {
                NthRule = EveryDayRule,
                Nth = Nth ,
                ReferencedRule = ReferencedRule
            };

            return shiftRule.IsMatch(day);
        }
    }

}