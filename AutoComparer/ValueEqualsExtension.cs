using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoComparer
{
    public static class ValueEqualsExtension
    {
        public static bool ValueEquals<T, U>(this T obj1, U obj2, bool recursivelyCheckInnerObjects = false)
            where T : class where U : class
        {
            if (obj2 == null)
            {
                return false;
            }

            var matchedProperties = MapMatchingPropertyNames<T, U>();

            if (matchedProperties.Item1.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < matchedProperties.Item1.Count; ++i)
            {
                var prop1 = matchedProperties.Item1[i];
                var prop2 = matchedProperties.Item2[i];

                if (prop1.PropertyType.IsClass && prop1.PropertyType.IsValueType && prop2.PropertyType.IsClass && 
                    prop2.PropertyType.IsValueType  && recursivelyCheckInnerObjects)
                {
                    if (!ValueEqualsCompare(prop1.GetValue(obj1), prop2.GetValue(obj2)))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!EqualsCompare(prop1.GetValue(obj1), prop2.GetValue(obj2)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static (List<PropertyInfo>, List<PropertyInfo>) MapMatchingPropertyNames<T, U>()
        {
            PropertyInfo[] obj1Properties = typeof(T).GetProperties();
            PropertyInfo[] obj2Properties = typeof(U).GetProperties();

            List<PropertyInfo> matchingObj1Properties = new List<PropertyInfo>();
            List<PropertyInfo> matchingObj2Properties = new List<PropertyInfo>();

            foreach (var obj1Property in obj1Properties)
            {
                foreach (var obj2Property in obj2Properties)
                {
                    if (obj1Property.Name.Equals(obj2Property.Name))
                    {
                        matchingObj1Properties.Add(obj1Property);
                        matchingObj2Properties.Add(obj2Property);
                    }
                }
            }

            return (matchingObj1Properties, matchingObj2Properties);
        }

        private static bool EqualsCompare(object val1, object val2)
        {
            if (val1 == null && val2 == null)
            {
                return true;
            }
            else if (val1 == null || val2 == null)
            {
                return false;
            }
            else
            {
                return val1.Equals(val2);
            }
        }

        private static bool ValueEqualsCompare(object val1, object val2)
        {
            if (val1 == null && val2 == null)
            {
                return true;
            }
            else if (val1 == null || val2 == null)
            {
                return false;
            }
            else
            {
                return val1.ValueEquals(val2);
            }
        }
    }
}
