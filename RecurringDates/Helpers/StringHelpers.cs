using System.Collections.Generic;
using System.Linq;

namespace RecurringDates.Helpers
{
    public static class StringHelpers
    {
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        public static string StringJoin<T>(this IEnumerable<T> list, string separator=",", string itemPadLeft="", string itemPadRight="")
        {
            if (itemPadLeft.IsNullOrEmpty() && itemPadRight.IsNullOrEmpty())
            {
                return string.Join(separator, list);
            }

            return string.Join(separator, list.Select(item => itemPadLeft + item.ToString() + itemPadRight));
        }
    }
}
