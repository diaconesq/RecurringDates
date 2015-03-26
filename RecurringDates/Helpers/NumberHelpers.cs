
namespace RecurringDates.Helpers
{
    public static class NumberHelpers
    {
        public static string ToOrdinalString(this decimal val)
        {
            if (val == -1)
            {
                return "last";
            }
            if (val < -1)
            {
                return ToOrdinalString(-val) + " to last";
            }
            return string.Format("{0}{1}", val, GetOrdinalSuffix(val));
        }
        public static string ToOrdinalString(this long val)
        {
            return ((decimal) val).ToOrdinalString();
        }
        public static string ToOrdinalString(this int val)
        {
            return ((decimal)val).ToOrdinalString();
        }

        private static string GetOrdinalSuffix(decimal val)
        {

            if(val%100 > 10 && val % 100 < 20)
            {
                //11th, 12th
                return "th";
            }
            switch ((int) (val%10))
            {
                case 1:
                    //1st, 21st
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}