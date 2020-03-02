using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace MicroService.Common.Extensions
{
    public static class ObjectExtension
    {
        public static bool TryConvert<T>(this object obj, out T result)
        {
            try
            {
                var converted = Convert.ChangeType(obj, typeof(T));
                if (converted is T)
                {
                    result = (T)converted;
                    return true;
                }
            }
            catch
            {

            }

            result = default(T);
            return false;
        }

        public static T ConvertTo<T>(this object obj)
        {
            try
            {
                var converted = Convert.ChangeType(obj, typeof(T));
                if (converted is T)
                {
                    return (T)converted;
                }
            }
            catch
            {

            }

            return default(T);
        }


        /// <summary>
        /// 对象序列化为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var result = JsonConvert.SerializeObject(obj, jsonSerializerSettings);

            return result;
        }

        /// <summary>
        /// 序列化为XML字符串
        /// </summary>
        public static string ToXml(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                var xml = new XmlSerializer(obj.GetType());
                try
                {
                    //序列化对象
                    xml.Serialize(stream, obj);
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
                stream.Position = 0;
                using (var streamReader = new StreamReader(stream))
                {
                    string xmlString = streamReader.ReadToEnd();
                    return xmlString;
                }
            }
        }

        /// <summary>
        /// XML字符串反虚拟化对象
        /// </summary>
        public static T XmlToObj<T>(this string xmlString)
        {
            try
            {
                using (var stringReader = new StringReader(xmlString))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    return (T)xml.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToTime(this string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            long lTime = long.Parse(timeStamp);
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string TimeToStamp(this System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            var result = (int)(time - startTime).TotalSeconds;
            return result.ToString();
        }

        public static bool IsNull<T>(this T value)
        {
            if (value != null)
                return false;

            Type type = typeof(T);

            if (!type.IsValueType)
                return true; // ref-type

            if (Nullable.GetUnderlyingType(type) != null)
                return true; // Nullable<T>

            return false; // value-type
        }

        public static string ToJSON(this object obj)
        {
            var jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            var result = JsonConvert.SerializeObject(obj, jsonSerializerSettings);

            return result;
        }

        public static T GetAttribute<T>(this MemberInfo field) where T : Attribute
        {
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        public static string GetAttrDescription(this object value, string memberName)
        {
            MemberInfo memberInfo = value.GetType().GetMembers().FirstOrDefault(m => m.Name == memberName);

            if (memberInfo == null) return string.Empty;

            var attribute = GetAttribute<DescriptionAttribute>(memberInfo);

            return attribute.Description;
        }

        public static string GetObjDescription(this object value, string name)
        {
            var attrs = value.GetType().GetCustomAttributes();

            foreach (var attr in attrs)
            {
                if (attr is DescriptionAttribute)
                {
                    return ((DescriptionAttribute)attr).Description;
                }
            }
            return string.Empty;
        }

        public static string GetObjDescription(this object value)
        {
            var attrs = value.GetType().GetCustomAttributes();

            foreach (var attr in attrs)
            {
                if (attr is DescriptionAttribute)
                {
                    return ((DescriptionAttribute)attr).Description;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// 把简单一个对象转换成属性名-属性值的键值对字典
        /// 注意：此扩展方法不支持嵌套的复杂对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            if (obj == null)
                return new Dictionary<string, object>();

            var dict = new Dictionary<string, object>();

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                dict[prop.Name] = prop.GetValue(obj);
            }
            return dict;
        }

        /// <summary>
        /// JSON序列化和反序列化实现的深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


        public static List<T> DeepCloneClear<T>(this List<T> source)
        {
            var result = source.DeepClone();
            source.Clear();
            return result;
        }
    }
}
