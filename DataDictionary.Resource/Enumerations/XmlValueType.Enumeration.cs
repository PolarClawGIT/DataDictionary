using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for a XmlType Enumeration.
    /// </summary>
    /// <remarks>Support only the W3C XML Schema types.</remarks>
    [Obsolete("Switch to XmlTypeCode and ObjectPropertyType")]
    public interface IXmlTypeEnumeration : IEnumeration<XmlValueType>
    { }

    /// <summary>
    /// Enumeration support class for System.Xml.Schema.XmlType
    /// </summary>
    [Obsolete("Switch to XmlTypeCode and ObjectPropertyType")]
    class XmlTypeEnumeration : Enumeration<XmlValueType, XmlTypeEnumeration>, IXmlTypeEnumeration
    {
        /// <summary>
        /// The equivalent XmlTypeCode that represents the W3C XML Schema type.
        /// </summary>
        /// <remarks>None represents not determined.</remarks>
        public XmlTypeCode XmlType { get; init; } = XmlTypeCode.None;

        /// <summary>
        /// A function used by the TryConvert to determine if the Property Info matches the given type
        /// </summary>
        public Func<PropertyInfo, Boolean> IsOfType { get; init; } = (info) => false;

        XmlTypeEnumeration(XmlValueType value, XmlTypeCode xmlType = XmlTypeCode.None, Func<PropertyInfo, Boolean>? isOfType = null) : base(value)
        {
            XmlType = xmlType;

            if (isOfType is not null)
            { IsOfType = isOfType; }
        }

        static XmlTypeEnumeration()
        {
            List<XmlTypeEnumeration> data = new List<XmlTypeEnumeration>()
            {
                new XmlTypeEnumeration(XmlValueType.None),
                new XmlTypeEnumeration(XmlValueType.String, XmlTypeCode.String,
                    (p) => p.PropertyType == typeof(string)),
                new XmlTypeEnumeration(XmlValueType.Integer, XmlTypeCode.Integer,
                    (p) => p.PropertyType == typeof(Int16)
                        || p.PropertyType == typeof(Nullable<Int16>)
                        || p.PropertyType == typeof(Int32)
                        || p.PropertyType == typeof(Nullable<Int32>)
                        || p.PropertyType == typeof(Int64)
                        || p.PropertyType == typeof(Nullable<Int64>)
                        || p.PropertyType == typeof(Int128)
                        || p.PropertyType == typeof(Nullable<Int128>)
                        || p.PropertyType == typeof(Byte)
                        || p.PropertyType == typeof(Nullable<Byte>)
                        || p.PropertyType == typeof(UInt16)
                        || p.PropertyType == typeof(Nullable<UInt16>)
                        || p.PropertyType == typeof(UInt32)
                        || p.PropertyType == typeof(Nullable<UInt32>)
                        || p.PropertyType == typeof(UInt64)
                        || p.PropertyType == typeof(Nullable<UInt64>)
                        || p.PropertyType == typeof(UInt128)
                        || p.PropertyType == typeof(Nullable<UInt128>)
                        || p.PropertyType == typeof(SByte)
                        || p.PropertyType == typeof(Nullable<SByte>)),
                new XmlTypeEnumeration(XmlValueType.Long, XmlTypeCode.Long,
                    (p) => p.PropertyType == typeof(long)
                        || p.PropertyType == typeof(Nullable<long>)),
                new XmlTypeEnumeration(XmlValueType.Boolean, XmlTypeCode.Boolean,
                    (p) => p.PropertyType == typeof(bool)
                        || p.PropertyType == typeof(Nullable<bool>)),
                new XmlTypeEnumeration(XmlValueType.Decimal, XmlTypeCode.Decimal,
                    (p) => p.PropertyType == typeof(decimal)
                        || p.PropertyType == typeof(Nullable<decimal>)),
                new XmlTypeEnumeration(XmlValueType.Float, XmlTypeCode.Float,
                    (p) => p.PropertyType == typeof(float)
                        || p.PropertyType == typeof(Nullable<float>)),
                new XmlTypeEnumeration(XmlValueType.Double, XmlTypeCode.Double,
                    (p) => p.PropertyType == typeof(float)
                        || p.PropertyType == typeof(Nullable<float>)),
                new XmlTypeEnumeration(XmlValueType.DateTime, XmlTypeCode.DateTime,
                    (p) => p.PropertyType == typeof(DateTime)
                        || p.PropertyType == typeof(Nullable<DateTime>)),

                // Does not translate directly to a XmlTypeCode
                new XmlTypeEnumeration(XmlValueType.Guid, default,
                    (p) => p.PropertyType == typeof(Guid)
                        || p.PropertyType == typeof(Nullable<Guid>)),
                new XmlTypeEnumeration(XmlValueType.Class, default,
                    (p) => (p.PropertyType.IsClass || p.PropertyType.IsInterface)
                        && p.PropertyType != typeof(string)),
                new XmlTypeEnumeration(XmlValueType.Enum, default,
                    (p) => p.PropertyType.IsEnum),

                // Cannot determine by Property Type, does not translate directly to XmlTypeCode.
                new XmlTypeEnumeration(XmlValueType.Xml),
                new XmlTypeEnumeration(XmlValueType.RichText),
                new XmlTypeEnumeration(XmlValueType.List),
            };

            BuildDictionary(data);

            Boolean tester(PropertyInfo info)
            {   // Used to test the IsOfType logic
                return info.PropertyType.IsClass;
            }
        }



        /// <summary>
        /// Try to convert the .Net Type to an XmlValueType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryConvert(PropertyInfo type, [NotNullWhen(true)] out XmlValueType? result)
        {
            var matched = EnumerationValues.Where(w => w.Value.IsOfType(type)).ToList();


            //var x = GetXmlTypeCode(type);

            if (matched.Count > 0 && matched.First().Key is XmlValueType value)
            { result = value; return true; }
            else
            {
#if DEBUG
                Exception ex = new InvalidOperationException("Could not determine Type");
                ex.Data.Add(nameof(type.PropertyType), type.PropertyType.Name);
                throw ex;
#else
                result = null; return false; 
#endif
            }
        }

        public static XmlTypeCode GetXmlTypeCode(PropertyInfo property)
        {
            // This code was gotten from Google AI search result.
            // It does not actually work as even a String return XmlTypeCode.None.

            // 1. Get the underlying CLR type of the property
            Type propertyType = property.PropertyType;



            // 2. Query the built-in simple type mapping for this type
            XmlSchemaDatatype? datatype = XmlSchemaType.GetBuiltInSimpleType(new XmlQualifiedName(propertyType.Name, "http://w3.org"))?.Datatype;


            // 3. Return the TypeCode, or None if it's a complex/unmapped type
            return datatype?.TypeCode ?? XmlTypeCode.None;
        }

        /// <summary>
        /// Try to convert the DomainPropertyType to an XmlValueType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryConvert(DomainPropertyType type, [NotNullWhen(true)] out XmlValueType? result)
        {
            switch (type)
            {
                case DomainPropertyType.Null: result = XmlValueType.None; break;
                case DomainPropertyType.String: result = XmlValueType.String; break;
                case DomainPropertyType.Integer: result = XmlValueType.Integer; break;
                case DomainPropertyType.List: result = XmlValueType.List; break;
                case DomainPropertyType.Xml: result = XmlValueType.Xml; break;
                case DomainPropertyType.MS_ExtendedProperty: result = XmlValueType.String; break;
                default: result = null; break;
            }

            if (result is null) { return false; }
            else { return true; }

        }
    }
}
