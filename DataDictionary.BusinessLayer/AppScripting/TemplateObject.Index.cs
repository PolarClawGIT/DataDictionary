using DataDictionary.DataLayer.AppScript;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateObjectIndex : DataLayer.AppScript.ITemplateObjectKey
    { }

    /// <inheritdoc/>
    public class TemplateObjectIndex : DataLayer.AppScript.TemplateObjectKey, ITemplateObjectIndex,
        IKeyEquality<TemplateObjectIndex>,
        IKeyEquality<ITemplateObjectIndex>
    {
        /// <inheritdoc cref="TemplateObjectKey(DataLayer.AppScript.ITemplateObjectKey)"/>
        public TemplateObjectIndex(ITemplateObjectIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectIndex? other)
        { return other is DataLayer.AppScript.ITemplateObjectKey value && Equals(new DataLayer.AppScript.TemplateObjectKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectIndex? other)
        { return other is DataLayer.AppScript.ITemplateObjectKey value && Equals(new DataLayer.AppScript.TemplateObjectKey(value)); }
    }
}
