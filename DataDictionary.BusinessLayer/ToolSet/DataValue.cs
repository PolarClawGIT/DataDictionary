using DataDictionary.Resource;
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
    public interface IDataValue : IKey, IBindingPropertyChanged, IBindingRowState
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        Guid Index { get { return AsDataValue().Index; } }

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        String Title { get { return AsDataValue().Title; } }

        /// <summary>
        /// Returns the value as the generic DataValue.
        /// </summary>
        /// <returns></returns>
        IDataValue AsDataValue();
    }

    /// <summary>
    /// Base Class for holding a generic DataLayer Value
    /// </summary>
    class DataValue : IDataValue, IKeyComparable<IDataValue>
    {
        /// <inheritdoc/>
        public Guid Index { get { return GetIndex(); } }

        /// <inheritdoc/>
        public String Title { get { return GetTitle(); } }

        /// <summary>
        /// Function that returns the GUID of the source value key.
        /// </summary>
        public required Func<Guid> GetIndex { get; init; }

        /// <summary>
        /// Function that returns the Title of the source value
        /// </summary>
        public required Func<String> GetTitle { get; init; }

        /// <summary>
        /// Function to indicate that the Title has changed.
        /// </summary>
        public required Func<PropertyChangedEventArgs, Boolean> IsTitleChanged { get; init; }

        Func<DataRowState> GetRowState { get; init; }

        public DataValue(IDataValue source)
        {
            GetRowState = source.RowState;
            source.PropertyChanged += Source_PropertyChanged;
            source.RowStateChanged += Source_RowStateChanged;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (IsTitleChanged(e))
            { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(IDataValue.Title)); }
        }

        void Source_RowStateChanged(Object? sender, RowStateEventArgs e)
        {
            if (RowStateChanged is EventHandler<RowStateEventArgs> handler)
            { handler(this, new RowStateEventArgs(GetRowState())); }
        }

        public DataRowState RowState()
        { return GetRowState(); }

        public IDataValue AsDataValue()
        { return this; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual Boolean Equals(IDataValue? other)
        {
            return
                other is IDataValue &&
                !Index.Equals(Guid.Empty) &&
                !other.Index.Equals(Guid.Empty) &&
                Index.Equals(other.Index);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDataValue value && Equals(value); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDataValue? other)
        {
            if (other is null) { return 1; }
            else if (Index.Equals(Guid.Empty)) { return 1; }
            else if (other.Index.Equals(Guid.Empty)) { return -1; }
            else { return Index.CompareTo(other.Index); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDataValue value) { return CompareTo(value); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DataValue left, DataValue right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DataValue left, DataValue right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DataValue left, DataValue right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DataValue left, DataValue right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DataValue left, DataValue right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DataValue left, DataValue right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return Index.GetHashCode(); }
        #endregion

        public override String ToString()
        { return Title; }

    }
}
