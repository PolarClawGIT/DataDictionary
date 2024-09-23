﻿using DataDictionary.Resource;
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
    // TODO: Approach appears to work. Convert Index to a type, replacing DataLayerIndex.
    // Use that for IKeyComparable.

    /// <summary>
    /// Interface for the classes that implement the DataValue
    /// </summary>
    public interface IDataValue : IKey, IBindingPropertyChanged, IBindingRowState, IScopeType
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataIndex Index { get { return AsDataValue().Index; } }

        /// <summary>
        /// Title/Name for the value
        /// </summary>
        String Title { get { return AsDataValue().Title; } }

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
        /// <inheritdoc/>
        public DataIndex Index { get { return GetIndex(); } }

        /// <inheritdoc/>
        public String Title { get { return GetTitle(); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return GetScope(); } }

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

        Func<DataRowState> GetRowState { get; init; }

        public DataValue(IDataValue source)
        {
            GetRowState = source.RowState;
            source.PropertyChanged += OnPropertyChanged;
            source.RowStateChanged += OnRowStateChanged;
        }

        public virtual event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        protected virtual void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (IsTitleChanged(e))
            { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(Title)); }
        }

        void OnRowStateChanged(Object? sender, RowStateEventArgs e)
        {
            if (RowStateChanged is EventHandler<RowStateEventArgs> handler)
            { handler(this, new RowStateEventArgs(GetRowState())); }
        }

        public DataRowState RowState()
        { return GetRowState(); }

        public IDataValue AsDataValue()
        { return this; }

        public override String ToString()
        { return Title; }

    }
}
