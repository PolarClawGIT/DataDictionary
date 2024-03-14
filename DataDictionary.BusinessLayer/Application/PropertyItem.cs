using DataDictionary.BusinessLayer.Script;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyItem : DataLayer.ApplicationData.Property.IPropertyItem , IScriptRow
    { }

    /// <inheritdoc/>
    public class PropertyItem : DataLayer.ApplicationData.Property.PropertyItem
    {
        /// <inheritdoc/>
        public PropertyItem() : base() { }

        /// <inheritdoc/>
        public IEnumerable<ScriptRow> GetScriptDataRow()
        { return ScriptRow.GetScriptDataRow(GetRow()); }

        /// <inheritdoc/>
        public XElement GetXElement(IEnumerable<ScriptRow>? options = null)
        { return ScriptRow.GetXElement(GetRow(), options); }
    }
}
