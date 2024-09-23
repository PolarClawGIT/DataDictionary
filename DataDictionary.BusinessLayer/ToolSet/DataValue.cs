using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Base interface for a DataValue
    /// </summary>
    public interface IDataItem: IScopeType
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataIndex Index { get; }

        /// <summary>
        /// Title/Name for the value
        /// </summary>
        String Title { get; }
    }

    /// <summary>
    /// Interface for the classes that implement the DataValue
    /// </summary>
    public interface IDataValue : IKey, IBindingPropertyChanged, IDataItem
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataIndex IDataItem.Index { get { return AsDataValue().Index; } }

        /// <summary>
        /// Title/Name for the value
        /// </summary>
        String IDataItem.Title { get { return AsDataValue().Title; } }

        /// <inheritdoc/>
        ScopeType IScopeType.Scope { get { return AsDataValue().Scope; } }

        /// <summary>
        /// Returns the value as the generic DataValue.
        /// </summary>
        /// <returns></returns>
        IDataValue AsDataValue();
    }

    /// <summary>
    /// Base Class for holding a DataLayer Value
    /// </summary>
    class DataValue : IDataValue
    {
        // Understanding what is going on:
        // Unlike Class methods and Properties, Interface Methods and Properties are Implicitly overridden.
        // That is, the interface Methods and Properties are overridden without the key word override.
        //
        // IDataItem sets up the basic definitions of the properties, without any extra code.
        // IDataValue overrides the properties and points the calls to the AsDataValue.
        // The class that implements IDataValue (except DataValue) needs only implements the AsDataValue method.
        // The class that implements IDataValue creates an instance of DataValue and returns it as IDataValue.
        // The class DataValue then overrides the Properties to return the values the implementing class specified.
        //
        // This gives two ways into the interface properties but returns the same values.
        // * The code can request the object directly by calling AsDataValue from the implementing class.
        //   This simply returns the DataValue instance created by the implementing class.
        // * The code can cast the implementing class into IDataItem or IDataValue and access the properties.
        //   When the property is referenced, the interface property is called.
        //   That causes the implementing class to create, if needed, and returns the DataValue.
        //   The property is then returned from the DataValue class.
        // The implementing class can also override the property.
        // In this case, the property can be accessed directly without using DataValue.
        //
        // Interface properties and methods with default implantation are always hidden unless specifically overridden.
        // Because DataValue overrides the default implantation, the properties are visible.
        //
        // Child classes can be used to extend DataValue for additional interfaces.
        //
        // What this is trying to solve is a couple of limitations of interfaces.
        // * Data Binding does not work against interface properties that have default implementations.
        // * Events cannot be owned by an interface as there is no backing field for the event.
        // Because the interface is being backed by an instance of a class,
        // this approach allows data binding and events.

        /// <inheritdoc/>
        public virtual DataIndex Index { get { return GetIndex(); } }

        /// <inheritdoc/>
        public virtual String Title { get { return GetTitle(); } }

        /// <inheritdoc/>
        public virtual ScopeType Scope { get { return GetScope(); } }

        /// <summary>
        /// Function that returns the GUID of the source value key.
        /// </summary>
        public required Func<DataIndex> GetIndex { get; init; }

        /// <summary>
        /// Function that returns the Title of the source value
        /// </summary>
        public required Func<String> GetTitle { get; init; }

        /// <summary>
        /// Function that returns the Scope of the source value
        /// </summary>
        public required Func<ScopeType> GetScope { get; init; }

        /// <summary>
        /// Function to indicate that the Title has changed.
        /// </summary>
        public required Func<PropertyChangedEventArgs, Boolean> IsTitleChanged { get; init; }

        public DataValue(IDataValue source)
        { source.PropertyChanged += OnPropertyChanged; }

        public virtual event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        protected virtual void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (IsTitleChanged(e))
            { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(Title)); }
        }

        public IDataValue AsDataValue()
        { return this; }

        public override String ToString()
        { return  Title; }

    }
}
