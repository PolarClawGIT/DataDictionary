using DataDictionary.BusinessLayer.Script;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeAliasItem: IDomainAttributeAliasItem, IScriptRow
    { }

    /// <inheritdoc/>
    public class AttributeAliasItem : DomainAttributeAliasItem, IAttributeAliasItem
    {
        /// <inheritdoc/>
        public AttributeAliasItem() : base() { }

        /// <inheritdoc/>
        public AttributeAliasItem(IDomainAttributeKey key) : base(key) { }

        /// <inheritdoc/>
        public IEnumerable<ScriptRow> GetScriptDataRow()
        { return ScriptRow.GetScriptDataRow(GetRow()); }

        /// <inheritdoc/>
        public XElement GetXElement(IEnumerable<ScriptRow>? options = null)
        { return ScriptRow.GetXElement(GetRow(), options); }
    }
}
