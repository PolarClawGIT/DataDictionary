using System.Diagnostics.CodeAnalysis;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for a XmlType Enumeration.
    /// </summary>
    /// <remarks>Support only the W3C XML Schema types.</remarks>
    public interface IXmlTypeEnumeration : IEnumeration<XmlValueType>
    { }

    /// <summary>
    /// Enumeration support class for System.Xml.Schema.XmlType
    /// </summary>
    class XmlTypeEnumeration : Enumeration<XmlValueType, XmlTypeEnumeration>, IXmlTypeEnumeration
    {
        /// <summary>
        /// The Data Type native to .Net
        /// </summary>
        public Type? NetType { get; init; }

        public XmlTypeCode XmlType { get; init; } = XmlTypeCode.None;

        XmlTypeEnumeration(XmlValueType value, Type? netType = null, XmlTypeCode xmlType = XmlTypeCode.None) : base(value)
        {
            NetType = netType;
            XmlType = xmlType;
        }

        static XmlTypeEnumeration()
        {
            List<XmlTypeEnumeration> data = new List<XmlTypeEnumeration>()
            {
                new XmlTypeEnumeration(XmlValueType.None),
                new XmlTypeEnumeration(XmlValueType.String, typeof(string), XmlTypeCode.String),
                new XmlTypeEnumeration(XmlValueType.Integer, typeof(int), XmlTypeCode.Integer),
                new XmlTypeEnumeration(XmlValueType.Long, typeof(long), XmlTypeCode.Long),
                new XmlTypeEnumeration(XmlValueType.Boolean, typeof(bool), XmlTypeCode.Boolean),
                new XmlTypeEnumeration(XmlValueType.Decimal, typeof(decimal), XmlTypeCode.Decimal),
                new XmlTypeEnumeration(XmlValueType.Float, typeof(float), XmlTypeCode.Float),
                new XmlTypeEnumeration(XmlValueType.Double, typeof(double), XmlTypeCode.Double),
                new XmlTypeEnumeration(XmlValueType.DateTime, typeof(DateTime), XmlTypeCode.DateTime),
            };

            BuildDictionary(data);
        }

        /// <summary>
        /// Try to convert the .Net Type to an XmlValueType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryConvert(Type type, [NotNullWhen(true)] out XmlValueType? result)
        {
            var matched = EnumerationValues.Where(w => w.Value.NetType == type).ToList();

            if(matched.Count > 0 && matched.First().Key is XmlValueType value)
            { result = value; return true; }
            else { result = null; return false; }
        }
    }
}
