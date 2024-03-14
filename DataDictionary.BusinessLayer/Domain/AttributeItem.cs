using DataDictionary.BusinessLayer.Script;
using DataDictionary.DataLayer.DomainData.Attribute;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeItem : IDomainAttributeItem, IScriptRow { }

    /// <inheritdoc/>
    public class AttributeItem : DomainAttributeItem, IAttributeItem
    {
        /// <inheritdoc/>
        public AttributeItem() : base() { }

        /// <inheritdoc/>
        public IEnumerable<ScriptRow> GetScriptDataRow()
        { return ScriptRow.GetScriptDataRow(GetRow()); }

        /// <inheritdoc/>
        public XElement GetXElement(IEnumerable<ScriptRow>? options = null)
        { return ScriptRow.GetXElement(GetRow(), options); }
    }
}
