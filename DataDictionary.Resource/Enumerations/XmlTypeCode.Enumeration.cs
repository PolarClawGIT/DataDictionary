using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for a XmlTypeCode Enumeration.
    /// </summary>
    public interface IXmlTypeCodeEnumeration : IEnumeration<XmlTypeCode> { }

    public class XmlTypeCodeEnumeration : Enumeration<XmlTypeCode, XmlTypeCodeEnumeration>, IXmlTypeCodeEnumeration
    {
        // References:
        // Enum: https://learn.microsoft.com/en-us/dotnet/api/system.xml.schema.xmltypecode?view=net-10.0
        // .Net Mapping: https://learn.microsoft.com/en-us/dotnet/standard/data/xml/mapping-xml-data-types-to-clr-types

        /// <summary>
        /// Is the XmlTypeCode a W3C XML Schema type
        /// </summary>
        public Boolean IsXmlSchema { get; init; } = true;

        /// <summary>
        /// Is the XmlTypeCode a .NET Framework infrastructure and is not intended to be used directly.
        /// </summary>
        public Boolean IsInfrastructure { get; init; }

        /// <summary>
        /// Is the XmlTypeCode supported by this application.
        /// </summary>
        public Boolean IsSupported { get; init; }

        /// <summary>
        /// List of CLR (Common Language Runtime) Types that map to the XmlTypeCode 
        /// </summary>
        public IEnumerable<Type> ClrType { get; }

        /// <summary>
        /// Constructor for Infrastructure.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>IsXmlSchema = false, IsInfrastructure = true, IsSupported = false</remarks>
        XmlTypeCodeEnumeration(XmlTypeCode value) : base(value)
        {
            IsXmlSchema = false;
            IsInfrastructure = true;
            IsSupported = false;
            ClrType = new List<Type>();
        }

        /// <summary>
        /// Constructor for W3C XML Schema type with no .Net mapping.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <remarks>IsXmlSchema = true, IsInfrastructure = false, IsSupported = false</remarks>
        XmlTypeCodeEnumeration(XmlTypeCode value, String name) : base(value, name)
        {
            IsXmlSchema = !String.IsNullOrWhiteSpace(name); 
            IsInfrastructure = false;
            IsSupported = false;
            ClrType = new List<Type>();
        }

        /// <summary>
        /// Constructor for W3C XML Schema type with .Net mapping.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <param name="clrTypes"></param>
        /// <remarks>IsXmlSchema = true, IsInfrastructure = false, IsSupported = true</remarks>
        XmlTypeCodeEnumeration(XmlTypeCode value, String name, params IEnumerable<Type> clrTypes) : base(value, name)
        {
            IsXmlSchema = !String.IsNullOrWhiteSpace(name);
            IsInfrastructure = false;
            IsSupported = clrTypes.Any();

            ClrType = clrTypes.Distinct().ToList();
        }

        static XmlTypeCodeEnumeration()
        {
            List<XmlTypeCodeEnumeration> data = new List<XmlTypeCodeEnumeration>()
            {
                new XmlTypeCodeEnumeration(XmlTypeCode.None, String.Empty) { DisplayName = "None", IsInfrastructure = false, IsSupported = true},
                new XmlTypeCodeEnumeration(XmlTypeCode.Item) {IsInfrastructure = false},
                new XmlTypeCodeEnumeration(XmlTypeCode.Node),
                new XmlTypeCodeEnumeration(XmlTypeCode.Document),
                new XmlTypeCodeEnumeration(XmlTypeCode.Element),
                new XmlTypeCodeEnumeration(XmlTypeCode.Attribute),
                new XmlTypeCodeEnumeration(XmlTypeCode.Namespace),
                new XmlTypeCodeEnumeration(XmlTypeCode.ProcessingInstruction),
                new XmlTypeCodeEnumeration(XmlTypeCode.Comment),
                new XmlTypeCodeEnumeration(XmlTypeCode.Text),
                new XmlTypeCodeEnumeration(XmlTypeCode.AnyAtomicType, "xdt:anyAtomicType"),
                new XmlTypeCodeEnumeration(XmlTypeCode.UntypedAtomic, "xdt:untypedAtomic"),
                new XmlTypeCodeEnumeration(XmlTypeCode.String, "xs:string", typeof(String), typeof(string)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Boolean, "xs:boolean", typeof(Boolean), typeof(bool), typeof(Nullable<Boolean>), typeof(Nullable<bool>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Decimal, "xs:decimal", typeof(Decimal), typeof(Nullable<Decimal>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Float, "xs:float", typeof(Single), typeof(float), typeof(Nullable<Single>), typeof(Nullable<float>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Double, "xs:double", typeof(Double), typeof(double), typeof(Nullable<Double>), typeof(Nullable<double>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Duration, "xs:duration", typeof(TimeSpan), typeof(Nullable<TimeSpan>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.DateTime, "xs:dateTime", typeof(DateTime), typeof(Nullable<DateTime>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Time, "xs:time", typeof(TimeOnly), typeof(Nullable<TimeOnly>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Date, "xs:date", typeof(DateOnly), typeof(Nullable<DateOnly>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.GYearMonth, "xs:gYearMonth"),
                new XmlTypeCodeEnumeration(XmlTypeCode.GYear, "xs:gYear"),
                new XmlTypeCodeEnumeration(XmlTypeCode.GMonthDay, "xs:gMonthDay"),
                new XmlTypeCodeEnumeration(XmlTypeCode.GDay, "xs:gDay"),
                new XmlTypeCodeEnumeration(XmlTypeCode.GMonth, "xs:gMonth"),
                new XmlTypeCodeEnumeration(XmlTypeCode.HexBinary, "xs:hexBinary"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Base64Binary, "xs:base64Binary"),
                new XmlTypeCodeEnumeration(XmlTypeCode.AnyUri, "xs:anyURI", typeof(Uri)),
                new XmlTypeCodeEnumeration(XmlTypeCode.QName, "xs:QName", typeof(System.Xml.XmlQualifiedName)) {IsSupported = false},
                new XmlTypeCodeEnumeration(XmlTypeCode.Notation, "xs:NOTATION"),
                new XmlTypeCodeEnumeration(XmlTypeCode.NormalizedString, "xs:normalizedString"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Token, "xs:token"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Language, "xs:language"),
                new XmlTypeCodeEnumeration(XmlTypeCode.NmToken, "xs:NMTOKEN"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Name, "xs:Name"),
                new XmlTypeCodeEnumeration(XmlTypeCode.NCName, "xs:NCName"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Id, "xs:ID"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Idref, "xs:IDREF"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Entity, "xs:ENTITY"),
                new XmlTypeCodeEnumeration(XmlTypeCode.Integer, "xs:integer", typeof(Int128), typeof(Nullable<Int128>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.NonPositiveInteger, "xs:nonPositiveInteger"), // Does not include zero
                new XmlTypeCodeEnumeration(XmlTypeCode.NegativeInteger, "xs:negativeInteger"), // Does not include zero
                new XmlTypeCodeEnumeration(XmlTypeCode.Long, "xs:long", typeof(long), typeof(Nullable<long>), typeof(Int64), typeof(Nullable<Int64>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Int, "xs:int", typeof(int), typeof(Nullable<int>), typeof(Int32), typeof(Nullable<Int32>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Short, "xs:short", typeof(short), typeof(Nullable<short>), typeof(Int16), typeof(Nullable<Int16>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.Byte, "xs:byte", typeof(sbyte), typeof(Nullable<sbyte>), typeof(SByte), typeof(Nullable<SByte>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.NonNegativeInteger, "xs:nonNegativeInteger", typeof(UInt128), typeof(Nullable<UInt128>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.UnsignedLong, "xs:unsignedLong", typeof(UInt64), typeof(Nullable<UInt64>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.UnsignedInt, "xs:unsignedInt", typeof(UInt32), typeof(Nullable<UInt32>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.UnsignedShort, "xs:unsignedShort", typeof(UInt16), typeof(Nullable<UInt16>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.UnsignedByte, "xs:unsignedByte", typeof(byte), typeof(Nullable<byte>), typeof(Byte), typeof(Nullable<Byte>)),
                new XmlTypeCodeEnumeration(XmlTypeCode.PositiveInteger, "xs:positiveInteger"), // Does not include zero
                new XmlTypeCodeEnumeration(XmlTypeCode.YearMonthDuration, "xdt:yearMonthDuration"),
                new XmlTypeCodeEnumeration(XmlTypeCode.DayTimeDuration, "xdt:dayTimeDuration"),
            };

            BuildDictionary(data);
        }

        /// <summary>
        /// Converter that tries to determine the XmlTypeCode from the Clr Type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryConvert(Type value, out XmlTypeCode result)
        {
            var results = EnumerationValues.Where(w => w.Value.ClrType.Any(a => value == a)).ToList();

            if(results.Count == 0)
            { result = XmlTypeCode.None; return false; }
            else if(results.Count > 1) // Multiple mappings exist. Error?
            { result = results.First().Key; return false; }
            else { result = results.First().Key; return true; }
        }
    }
}
