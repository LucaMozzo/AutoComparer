using System;

namespace AutoComparer
{
    public static class ValueEqualsExtension
    {
        public static bool ValueEquals<T, U>(this T obj1, U obj2, bool recursivelyCheckInnerObjects = false)
            where T : new() where U : new()
        {
            return false;
        }
    }
}
