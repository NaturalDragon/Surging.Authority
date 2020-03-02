using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MicroService.Common.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 数字转换为枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int enumValue)
        {
            return (T)Enum.Parse(typeof(T), enumValue.ToString());
        }

        /// <summary>
        /// 枚举转换为数字
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        public static string GetAttrDescription(this Enum value)
        {
            var attribute = GetAttribute<DescriptionAttribute>(value);

            return attribute.Description;
        }

        public static IEnumerable<SelectItem> GetEnumItem<T>()
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "EnumType");
            }
            yield return new SelectItem { Value = "", Text = "请选择", Name = "请选择" };
            foreach (Enum enumValue in Enum.GetValues(type))
            {
                var desc = enumValue.GetAttrDescription();
                yield return new SelectItem
                {
                    Id = Convert.ToInt32(enumValue),
                    Value = Convert.ToInt32(enumValue).ToString(),
                    Text = desc,
                    Name = desc
                };
            }
        }

        public static IEnumerable<SelectItem> GetEnumList<T>()
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "EnumType");
            }

            foreach (Enum enumValue in Enum.GetValues(type))
            {
                var desc = enumValue.GetAttrDescription();
                yield return new SelectItem
                {
                    Id = Convert.ToInt32(enumValue),
                    Value = Convert.ToInt32(enumValue).ToString(),
                    Text = desc,
                    Name = desc
                };
            }
        }

    }

    public class SelectItem
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}
