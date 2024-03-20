using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// List of XML Data Types.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/standard/data/xml/mapping-xml-data-types-to-clr-types"/>
    public enum XmlDataType
    { // There may be an existing list but I could not find one.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        xs_anyURI,
        xs_base64Binary,
        xs_boolean,
        xs_byte,
        xs_date,
        xs_dateTime,
        xs_decimal,
        xs_double,
        xs_duration,
        xs_ENTITIES,
        xs_ENTITY,
        xs_float,
        xs_gDay,
        xs_gMonthDay,
        xs_gYear,
        xs_gYearMonth,
        xs_hexBinary,
        xs_ID,
        xs_IDREF,
        xs_IDREFS,
        xs_int,
        xs_integer,
        xs_language,
        xs_long,
        xs_gMonth,
        xs_Name,
        xs_NCName,
        xs_negativeInteger,
        xs_NMTOKEN,
        xs_NMTOKENS,
        xs_nonNegativeInteger,
        xs_nonPositiveInteger,
        xs_normalizedString,
        xs_positiveInteger,
        xs_short,
        xs_string,
        xs_time,
        xs_token,
        xs_unsignedByte,
        xs_unsignedInt,
        xs_unsignedLong,
        xs_unsignedShort,
        xs_anySimpleType,
        // Not mapped
        net_Guid
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Extension for dealing with XML Data Types
    /// </summary>
    public static class XmlDataTypeExtension
    {
        static Dictionary<XmlDataType, (String Name, Type NetType, Boolean IsSupported)> parseValues = new Dictionary<XmlDataType, (string Name, Type NetType, Boolean IsSupported)>()
        {
            { XmlDataType.xs_anyURI,            new("xs:anyURI",            typeof(Uri),        false)},
            { XmlDataType.xs_base64Binary,      new("xs:base64Binary",      typeof(Byte[]),     false)},
            { XmlDataType.xs_boolean,           new("xs:boolean",           typeof(Boolean),    true)},
            { XmlDataType.xs_byte,              new("xs:byte",              typeof(SByte),      true)},
            { XmlDataType.xs_date,              new("xs:date",              typeof(DateTime),   false)},
            { XmlDataType.xs_dateTime,          new("xs:dateTime",          typeof(DateTime),   true)},
            { XmlDataType.xs_decimal,           new("xs:decimal",           typeof(Decimal),    true)},
            { XmlDataType.xs_double,            new("xs:double",            typeof(Double),     true)},
            { XmlDataType.xs_duration,          new("xs:duration",          typeof(TimeSpan),   true)},
            { XmlDataType.xs_ENTITIES,          new("xs:ENTITIES",          typeof(String[]),   false)},
            { XmlDataType.xs_ENTITY,            new("xs:ENTITY",            typeof(String),     false)},
            { XmlDataType.xs_float,             new("xs:float",             typeof(Single),     true)},
            { XmlDataType.xs_gDay,              new("xs:gDay",              typeof(DateTime),   false)},
            { XmlDataType.xs_gMonthDay,         new("xs:gMonthDay",         typeof(DateTime),   false)},
            { XmlDataType.xs_gYear,             new("xs:gYear",             typeof(DateTime),   false)},
            { XmlDataType.xs_gYearMonth,        new("xs:gYearMonth",        typeof(DateTime),   false)},
            { XmlDataType.xs_hexBinary,         new("xs:hexBinary",         typeof(Byte[]),     false)},
            { XmlDataType.xs_ID,                new("xs:ID",                typeof(String),     false)},
            { XmlDataType.xs_IDREF,             new("xs:IDREF",             typeof(String),     false)},
            { XmlDataType.xs_IDREFS,            new("xs:IDREFS",            typeof(String[]),   false)},
            { XmlDataType.xs_int,               new("xs:int",               typeof(Int32),      true)},
            { XmlDataType.xs_integer,           new("xs:integer",           typeof(Decimal),    false)},
            { XmlDataType.xs_language,          new("xs:language",          typeof(String),     false)},
            { XmlDataType.xs_long,              new("xs:long",              typeof(Int64),      true)},
            { XmlDataType.xs_gMonth,            new("xs:gMonth",            typeof(DateTime),   false)},
            { XmlDataType.xs_Name,              new("xs:Name",              typeof(String),     false)},
            { XmlDataType.xs_NCName,            new("xs:NCName",            typeof(String),     false)},
            { XmlDataType.xs_negativeInteger,   new("xs:negativeInteger",   typeof(Decimal),    false)},
            { XmlDataType.xs_NMTOKEN,           new("xs:NMTOKEN",           typeof(String),     false)},
            { XmlDataType.xs_NMTOKENS,          new("xs:NMTOKENS",          typeof(String[]),   false)},
            { XmlDataType.xs_nonNegativeInteger,new("xs:nonNegativeInteger",typeof(Decimal),    false)},
            { XmlDataType.xs_nonPositiveInteger,new("xs:nonPositiveInteger",typeof(Decimal),    false)},
            { XmlDataType.xs_normalizedString,  new("xs:normalizedString",  typeof(String),     false)},
            { XmlDataType.xs_positiveInteger,   new("xs:positiveInteger",   typeof(Decimal),    false)},
            { XmlDataType.xs_short,             new("xs:short",             typeof(Int16),      true)},
            { XmlDataType.xs_string,            new("xs:string",            typeof(String),     true)},
            { XmlDataType.xs_time,              new("xs:time",              typeof(DateTime),   false)},
            { XmlDataType.xs_token,             new("xs:token",             typeof(String),     false)},
            { XmlDataType.xs_unsignedByte,      new("xs:unsignedByte",      typeof(Byte),       true)},
            { XmlDataType.xs_unsignedInt,       new("xs:unsignedInt",       typeof(UInt32),     true)},
            { XmlDataType.xs_unsignedLong,      new("xs:unsignedLong",      typeof(UInt64),     true)},
            { XmlDataType.xs_unsignedShort,     new("xs:unsignedShort",     typeof(UInt16),     true)},
            { XmlDataType.xs_anySimpleType,     new("xs:anySimpleType",     typeof(String),     false)},
            { XmlDataType.net_Guid,             new("net:System.Guid",      typeof(Guid),       true)},
        };

        /// <summary>
        /// Used to get the XML Data Type Cross Reference
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (String Name, Type NetType, Boolean IsSupported)? ToCrossReference(this XmlDataType value)
        {
            if (parseValues.ContainsKey(value))
            {   return parseValues[value]; }
            else { return null; }
        }
    }
}
