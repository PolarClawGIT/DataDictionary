using System.Xml;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for a XmlTypeCode Enumeration.
    /// </summary>
    public interface IXmlNodeTypeEnumeration : IEnumeration<XmlNodeType> { }

    public class XmlNodeTypeEnumeration : Enumeration<XmlNodeType, XmlNodeTypeEnumeration>, IXmlNodeTypeEnumeration
    {
        /// <summary>
        /// Is the XmlNodeType supported by this application.
        /// </summary>
        public Boolean IsSupported { get; init; } = true;

        XmlNodeTypeEnumeration(XmlNodeType value) : base(value)
        { }

        XmlNodeTypeEnumeration(XmlNodeType value, String name) : base(value, name)
        { }

        static XmlNodeTypeEnumeration()
        {
            List<XmlNodeTypeEnumeration> data = new List<XmlNodeTypeEnumeration>()
            {
                new XmlNodeTypeEnumeration(XmlNodeType.None, String.Empty) { DisplayName = "None"},
                new XmlNodeTypeEnumeration(XmlNodeType.Element),
                new XmlNodeTypeEnumeration(XmlNodeType.Attribute),
                new XmlNodeTypeEnumeration(XmlNodeType.Text),
                new XmlNodeTypeEnumeration(XmlNodeType.CDATA),
                new XmlNodeTypeEnumeration(XmlNodeType.EntityReference) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.Entity) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.ProcessingInstruction) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.Comment) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.Document) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.DocumentType) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.DocumentFragment) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.Notation) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.Whitespace) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.SignificantWhitespace) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.EndElement) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.EndEntity) {IsSupported = false},
                new XmlNodeTypeEnumeration(XmlNodeType.XmlDeclaration) {IsSupported = false},
            };

            BuildDictionary(data);
        }
    }
}
