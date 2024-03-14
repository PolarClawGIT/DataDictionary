using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Attribute;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public class AttributePropertyItem: DomainAttributePropertyItem, IScriptRow
    {
        /// <inheritdoc/>
        public AttributePropertyItem() : base() { }

        /// <inheritdoc/>
        public AttributePropertyItem(IDomainAttributeKey attributeKey) : base(attributeKey) { }

        /// <inheritdoc/>
        public AttributePropertyItem(IDomainAttributeKey attributeKey,
                                     IPropertyKey propertyKey,
                                     IDbExtendedPropertyItem value)
            : base(attributeKey, propertyKey, value) { }

        /// <inheritdoc/>
        public IEnumerable<ScriptRow> GetScriptDataRow()
        { return ScriptRow.GetScriptDataRow(GetRow()); }

        /// <inheritdoc/>
        public XElement GetXElement(IEnumerable<ScriptRow>? options = null)
        { return ScriptRow.GetXElement(GetRow(), options); }
    }
}
