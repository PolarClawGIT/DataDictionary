using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support for System.Xml.Schema.XmlTypeCode
    /// </summary>
    public interface IXmlTypeCodeEnumeration : IEnumeration<XmlTypeCode>
    { }

    class XmlTypeCodeEnumeration : Enumeration<XmlTypeCode, XmlTypeCodeEnumeration>, IXmlTypeCodeEnumeration
    {

        XmlTypeCodeEnumeration(XmlTypeCode typeCode) : base(typeCode) { }

        static XmlTypeCodeEnumeration()
        {
            List<XmlTypeCodeEnumeration> data = new List<XmlTypeCodeEnumeration>();
            data.AddRange(Enum.GetValues<XmlTypeCode>().Select(s => new XmlTypeCodeEnumeration(s)));


            BuildDictionary(data);
        }

        /// <summary>
        /// Try to convert PropertyInfo into the corresponding XmlTypeCode
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <remarks>
        /// This handles converting known/simple types to the W3C XML Schema Definition Language (XSD) schema types.<br/>
        /// Most .Net base types translate directly to the corresponding names. There are exceptions.<br/>
        /// In some cases, the mapping is not 1 to 1.<br/>
        /// This function covers the mappings used by this application.
        /// </remarks>
        public static Boolean TryConvert(PropertyInfo type, [NotNullWhen(true)] out XmlTypeCode? result) 
        {
            result = null;
            
            // I included but the .Net name and the C# name of the Type.
            // This is mostly for clarity and to remind myself that these things are the same.
            // I could not find documentation for a complete mapping.

            switch (type)
            {
                case PropertyInfo value when // Should never occur
                    value.PropertyType == null:
                    result = XmlTypeCode.None; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(String)
                    || value.PropertyType == typeof(string):
                    result = XmlTypeCode.String; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(Boolean)
                    || value.PropertyType == typeof(Nullable<Boolean>)
                    || value.PropertyType == typeof(bool)
                    || value.PropertyType == typeof(Nullable<bool>):
                    result = XmlTypeCode.Boolean; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(Decimal)
                    || value.PropertyType == typeof(Nullable<Decimal>):
                    result = XmlTypeCode.Decimal; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(float)
                    || value.PropertyType == typeof(Nullable<float>)
                    || value.PropertyType == typeof(Single)
                    || value.PropertyType == typeof(Nullable<Single>):
                    result = XmlTypeCode.Float; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(double)
                    || value.PropertyType == typeof(Nullable<double>)
                    || value.PropertyType == typeof(Double)
                    || value.PropertyType == typeof(Nullable<Double>):
                    result = XmlTypeCode.Double; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(TimeSpan)
                    || value.PropertyType == typeof(Nullable<TimeSpan>):
                    result = XmlTypeCode.Duration; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(DateTime)
                    || value.PropertyType == typeof(Nullable<DateTime>):
                    result = XmlTypeCode.DateTime; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(TimeOnly)
                    || value.PropertyType == typeof(Nullable<TimeOnly>):
                    result = XmlTypeCode.Time; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(DateOnly)
                    || value.PropertyType == typeof(Nullable<DateOnly>):
                    result = XmlTypeCode.Date; break;

                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.GYearMonth; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.GYear; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.GMonthDay; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.GDay; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.GMonth; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.HexBinary; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Base64Binary; break;

                case PropertyInfo value when
                    value.PropertyType == typeof(System.Uri):
                    result = XmlTypeCode.AnyUri; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(System.Xml.XmlQualifiedName):
                    result = XmlTypeCode.QName; break;

                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Notation; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NormalizedString; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Token; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Language; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NmToken; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Name; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NCName; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Id; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Idref; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.Entity; break;

                case PropertyInfo value when // Unhandled Integers
                    value.PropertyType == typeof(Int128)
                    || value.PropertyType == typeof(Nullable<Int128>):
                    result = XmlTypeCode.Integer; break;

                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NonPositiveInteger; break;
                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NegativeInteger; break;

                case PropertyInfo value when
                    value.PropertyType == typeof(long)
                    || value.PropertyType == typeof(Nullable<long>)
                    || value.PropertyType == typeof(Int64)
                    || value.PropertyType == typeof(Nullable<Int64>):
                    result = XmlTypeCode.Long; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(int)
                    || value.PropertyType == typeof(Nullable<int>)
                    || value.PropertyType == typeof(Int32)
                    || value.PropertyType == typeof(Nullable<Int32>):
                    result = XmlTypeCode.Int; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(short)
                    || value.PropertyType == typeof(Nullable<short>)
                    || value.PropertyType == typeof(Int16)
                    || value.PropertyType == typeof(Nullable<Int16>):
                    result = XmlTypeCode.Short; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(sbyte)
                    || value.PropertyType == typeof(Nullable<sbyte>):
                    result = XmlTypeCode.Byte; break;

                case PropertyInfo value when// Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.NonNegativeInteger; break;

                case PropertyInfo value when
                    value.PropertyType == typeof(UInt64)
                    || value.PropertyType == typeof(Nullable<UInt64>):
                    result = XmlTypeCode.UnsignedLong; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(UInt32)
                    || value.PropertyType == typeof(Nullable<UInt32>):
                    result = XmlTypeCode.UnsignedInt; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(UInt16)
                    || value.PropertyType == typeof(Nullable<UInt16>):
                    result = XmlTypeCode.UnsignedShort; break;
                case PropertyInfo value when
                    value.PropertyType == typeof(byte)
                    || value.PropertyType == typeof(Nullable<byte>):
                    result = XmlTypeCode.UnsignedByte; break;

                case PropertyInfo value when // Never occurs, no mapping
                    value.PropertyType == null:
                    result = XmlTypeCode.PositiveInteger; break;

                default:
                    break;
            }


            if(result is null)
            { return false; }
            else { return true; }
        }
    }
}
