using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Property;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyValue : IDomainPropertyItem, IPropertyIndex, IPropertyIndexName
    { }

    /// <inheritdoc/>
    public class PropertyValue : DomainPropertyItem, IPropertyValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DomainPropertyItem()"/>
        public PropertyValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new PropertyIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return PropertyTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        { return new NamedScopePath(PropertyTitle); }

    }
}
