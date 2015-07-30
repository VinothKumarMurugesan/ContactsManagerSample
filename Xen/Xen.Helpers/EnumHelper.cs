using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Xen.Helpers
{
    public static class EnumHelper
    {
        public static object GetEnumDescriptions(Type enumType)
        {
            var list = new List<KeyValuePair<int, string>>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                string description = value.ToString();
                System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(description);
                var attribute = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0];

                if (attribute != null)
                {
                    description = (attribute as System.ComponentModel.DescriptionAttribute).Description;

                }
                list.Add(new KeyValuePair<int, string>(Convert.ToInt32(value), description));
            }
            return list;
        }


        public static object GetEnumDetails(Type enumType)
        {
            var list = new List<KeyValuePair<int, string>>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                list.Add(new KeyValuePair<int, string>(Convert.ToInt32(value), Enum.GetName(enumType, value)));
            }
            return list;
        }

        public static string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

    }
}
