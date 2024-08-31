using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

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
        { return DefinitionTitle ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DefinitionTitle); }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(DefinitionTitle); }
    }
}
