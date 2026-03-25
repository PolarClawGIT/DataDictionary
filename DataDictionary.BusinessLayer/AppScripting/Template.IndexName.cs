using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateIndexName : ITemplateKeyName
    { }

    /// <inheritdoc/>
    public class TemplateIndexName : TemplateKeyName, ITemplateIndexName,
        IKeyEquality<ITemplateIndexName>, IKeyEquality<TemplateIndexName>
    {
        /// <inheritdoc cref="TemplateKeyName(ITemplateKeyName)"/>
        public TemplateIndexName(ITemplateIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndexName? other)
        { return other is ITemplateKeyName key && Equals(new TemplateKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndexName? other)
        { return other is ITemplateKeyName key && Equals(new TemplateKeyName(key)); }

        /// <summary>
        /// Convert TemplateIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TemplateIndexName source)
        { return new DataIndexName() { Title = source.TemplateTitle ?? String.Empty }; }
    }

}
