using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Definition;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionValue : IDomainDefinitionItem, IDefinitionIndex, IDefinitionIndexName
    { }

    /// <inheritdoc/>
    public class DefinitionValue : DomainDefinitionItem, IDefinitionValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DomainDefinitionItem()"/>
        public DefinitionValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DefinitionIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return DefinitionTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DefinitionTitle); }

    }
}
