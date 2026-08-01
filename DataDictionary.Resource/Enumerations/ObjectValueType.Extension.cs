using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DataDictionary.Resource.Enumerations
{
    public static class ValueTypeExtension
    {
        /// <summary>
        /// Try to parse the String into a ObjectPropertyType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out ObjectValueType result)
        {
            if (ObjectValueTypeEnumeration.TryParse(value, null, out ObjectValueTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = ObjectValueType.Null; return false; }
        }

        /// <summary>
        /// Gets the Name of the ObjectPropertyType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this ObjectValueType value)
        {
            if (ObjectValueTypeEnumeration.TryGetValue(value, out ObjectValueTypeEnumeration? result))
            { return result.Name; }
            else { return String.Empty; }
        }

        /// <summary>
        /// Try to convert the PropertyType into a ObjectValueType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <remarks>This handled the general case, not specialized classes.</remarks>
        public static Boolean TryConvert(this PropertyInfo type, out ObjectValueType result)
        {
            ObjectValueType? valueType = null;

            // I included but the .Net name and the C# name of the Type.
            // This is mostly for clarity and to remind myself that these things are the same.

            switch (type)
            {
                case PropertyInfo value when // Should never occur
                    value.PropertyType == null:
                    valueType = ObjectValueType.Null; break;

                // General cases
                case PropertyInfo value when
                    value.PropertyType == typeof(String)
                    || value.PropertyType == typeof(string):
                    valueType = ObjectValueType.String; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(Boolean)
                    || value.PropertyType == typeof(Nullable<Boolean>)
                    || value.PropertyType == typeof(bool)
                    || value.PropertyType == typeof(Nullable<bool>):
                    valueType = ObjectValueType.Boolean; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(Decimal)
                    || value.PropertyType == typeof(Nullable<Decimal>)
                    || value.PropertyType == typeof(float)
                    || value.PropertyType == typeof(Nullable<float>)
                    || value.PropertyType == typeof(Single)
                    || value.PropertyType == typeof(Nullable<Single>)
                    || value.PropertyType == typeof(double)
                    || value.PropertyType == typeof(Nullable<double>)
                    || value.PropertyType == typeof(Double)
                    || value.PropertyType == typeof(Nullable<Double>)
                    || value.PropertyType == typeof(Int128)
                    || value.PropertyType == typeof(Nullable<Int128>)
                    || value.PropertyType == typeof(long)
                    || value.PropertyType == typeof(Nullable<long>)
                    || value.PropertyType == typeof(Int64)
                    || value.PropertyType == typeof(Nullable<Int64>)
                    || value.PropertyType == typeof(int)
                    || value.PropertyType == typeof(Nullable<int>)
                    || value.PropertyType == typeof(Int32)
                    || value.PropertyType == typeof(Nullable<Int32>)
                    || value.PropertyType == typeof(short)
                    || value.PropertyType == typeof(Nullable<short>)
                    || value.PropertyType == typeof(Int16)
                    || value.PropertyType == typeof(Nullable<Int16>)
                    || value.PropertyType == typeof(sbyte)
                    || value.PropertyType == typeof(Nullable<sbyte>)
                    || value.PropertyType == typeof(UInt64)
                    || value.PropertyType == typeof(Nullable<UInt64>)
                    || value.PropertyType == typeof(UInt32)
                    || value.PropertyType == typeof(Nullable<UInt32>)
                    || value.PropertyType == typeof(UInt16)
                    || value.PropertyType == typeof(Nullable<UInt16>)
                    || value.PropertyType == typeof(byte)
                    || value.PropertyType == typeof(Nullable<byte>):
                    valueType = ObjectValueType.Numeric; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(Guid)
                    || value.PropertyType == typeof(Nullable<Guid>):
                    valueType = ObjectValueType.GUID; break;
                case PropertyInfo value when
                    value.PropertyType.IsEnum:
                    valueType = ObjectValueType.Enumeration; break;
                case PropertyInfo value when
                    value.PropertyType.IsClass:
                    valueType = ObjectValueType.Class; break;
                case PropertyInfo value when
                    value.PropertyType.IsInterface:
                    valueType = ObjectValueType.Interface; break;
                default:
                    break;
            }

            if (valueType is null)
            { result = ObjectValueType.Null; return false; }
            else { result = valueType.Value; return true; }

        }
    }
}
