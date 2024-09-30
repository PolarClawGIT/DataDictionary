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
    public interface IDataValue: IScopeType, IBindingPropertyChanged
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
    /// Base Class for holding a DataLayer Value
    /// </summary>
    class DataValue : IDataValue
    {
        // Understanding what is going on:
        // Unlike Class methods and Properties, Interface Methods and Properties are Implicitly overridden.
        // That is, the interface Methods and Properties are overridden without the key word override.
        //
        // This take two forms.
        //  * public DataIndex Index { get { return ??; } }
        //    This overrides all "Index", regardless of interface it comes from.
        //  * DataIndex IDataValue.Index { get { return ??.Index; } 
        //    This overrides the "Index" from the interface "IDataValue" only.
        //    These are not visible/available to the implementing class without casting the class back to the interface.
        //
        // DataValue class is a backing class for the interface IDataValue.
        // Classes that implement IDataValue or one of its child interfaces need to create an instance of the class.
        // This can be done in the constructor.
        //
        //   IDataValue dataValue;
        //   public ImplmentingValue() : base()
        //   {
        //       dataValue = new dataValue(this)
        //       {
        //           GetIndex = () => new ImplmentingIndex(this),
        //           GetTitle = () => ImplmentingTitle ?? String.Empty,
        //           GetScope = () => Scope,
        //           IsTitleChanged = (e) => e.PropertyName is nameof(ImplmentingTitle)
        //       };
        //   }
        //
        // Additionally, any missing (or all) properties need to be overridden to get their values from the instance of DataValue.
        //
        //   DataIndex IDataValue.Index { get { return datavalue.Index; } }
        //
        // Data-binding: Data-binding does not work on an interface property.
        // The property that is bound to must be a member of the class or a class that it inherits from.
        // Interface properties are not available in data-binding.
        // Even properties that are overridden by an interface property returns the original property.
        // This class does not solve the binding issue.

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

        public DataValue(IBindingPropertyChanged source)
        { source.PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (IsTitleChanged(e))
            { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(Title)); }
        }
        public override String ToString()
        { return  Title; }

    }
}
