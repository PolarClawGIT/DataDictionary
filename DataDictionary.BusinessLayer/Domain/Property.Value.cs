using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyValue : IDomainPropertyItem, IPropertyIndex, IPropertyIndexName
    { }

    /// <inheritdoc/>
    public class PropertyValue : DomainPropertyItem, IPropertyValue, INamedScopeSourceValue
    {
        /// <summary>
        /// Provides the List of Choices for PropertyType is List
        /// </summary>
        public List<String> Choices
        {
            get
            {
                if (PropertyType is DomainPropertyType.List && PropertyData is String)
                { return PropertyData.Split(',').ToList(); }
                else return new List<String>();
            }
            set
            {
                String values = String.Join(',', value.
                    Select(s => { if (String.IsNullOrWhiteSpace(s)) { return null; } else { return s.Trim(); } }).
                    OfType<String>().ToArray());

                if (String.IsNullOrWhiteSpace(values))
                {
                    PropertyType = DomainPropertyType.Null;
                    PropertyData = null;
                    OnPropertyChanged(nameof(PropertyType));
                    OnPropertyChanged(nameof(PropertyData));
                }
                else
                {
                    PropertyType = DomainPropertyType.List;
                    PropertyData = values;
                    OnPropertyChanged(nameof(PropertyType));
                    OnPropertyChanged(nameof(PropertyData));
                }
            }
        }

        /// <summary>
        /// Provides the MS Extended Property Name for PropertyType is Extended Property
        /// </summary>
        public String ExtendedPropertyName
        {
            get
            {
                if (PropertyType is DomainPropertyType.MS_ExtendedProperty && PropertyData is String)
                { return PropertyData; }
                else { return String.Empty; }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    PropertyType = DomainPropertyType.Null;
                    PropertyData = null;
                    OnPropertyChanged(nameof(PropertyType));
                    OnPropertyChanged(nameof(PropertyData));
                }
                else
                {
                    PropertyType = DomainPropertyType.MS_ExtendedProperty;
                    PropertyData = value;
                    OnPropertyChanged(nameof(PropertyType));
                    OnPropertyChanged(nameof(PropertyData));
                }
            }
        }

        /// <inheritdoc cref="DomainPropertyItem()"/>
        public PropertyValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new PropertyIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return PropertyTitle ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        { return new NamedScopePath(PropertyTitle); }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(PropertyTitle); }
    }
}
