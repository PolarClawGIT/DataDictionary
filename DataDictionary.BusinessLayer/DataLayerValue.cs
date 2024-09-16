using DataDictionary.Resource;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Interface for a generic DataLayer Value Source
    /// </summary>
    public interface IDataLayerSource : IBindingPropertyChanged
    {
        /// <summary>
        /// Return the DataLayer ID.
        /// </summary>
        /// <returns></returns>
        Guid GetId();

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        /// <returns></returns>
        String GetTitle();

        /// <summary>
        /// Generic Description for the value. Null = does not apply.
        /// </summary>
        /// <returns></returns>
        String? GetDescription();

        /// <summary>
        /// Condition to test if the Title was the property that changed.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDataLayerValue: IKey, IKeyComparable<IDataLayerValue>,
        IBindingPropertyChanged
    {
        /// <summary>
        /// Generic version of the specific data layer ID.
        /// </summary>
        Guid SystemId { get; }

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        public String Title { get; }

        /// <summary>
        /// Generic Description for the value. Null = does not apply.
        /// </summary>
        public String? Description { get; }
    }

    /// <summary>
    /// Generic Data Layer Value
    /// </summary>
    class DataLayerValue : IDataLayerValue
    {
        IDataLayerSource source;

        /// <summary>
        /// Generic version of the specific data layer ID.
        /// </summary>
        public Guid SystemId { get { return systemIdValue; } }
        Guid systemIdValue;

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        public String Title { get { return titleValue; } }
        String titleValue;

        /// <summary>
        /// Generic Description for the value. Null = does not apply.
        /// </summary>
        public String? Description { get { return descriptionValue; } }
        String? descriptionValue;

        /// <summary>
        /// Creates a DataLayerValue from a source value
        /// </summary>
        /// <param name="source"></param>
        public DataLayerValue(IDataLayerSource source)
        {
            this.source = source;
            systemIdValue = source.GetId();
            titleValue = source.GetTitle();
            descriptionValue = source.GetDescription();
            source.PropertyChanged += Source_PropertyChanged;
        }

        private void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (source.IsTitleChanged(e))
            {
                titleValue = source.GetTitle();
                descriptionValue = source.GetDescription();
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, e.PropertyName);
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual Boolean Equals(IDataLayerValue? other)
        {
            return
                other is IDataLayerValue &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDataLayerSource value && Equals(new DataLayerValue(value)); }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDataLayerValue? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDataLayerIndex value) { return CompareTo(new DataLayerIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DataLayerValue left, DataLayerValue right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DataLayerValue left, DataLayerValue right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DataLayerValue left, DataLayerValue right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DataLayerValue left, DataLayerValue right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DataLayerValue left, DataLayerValue right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DataLayerValue left, DataLayerValue right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return systemIdValue.GetHashCode(); }
        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        { return titleValue; }

    }
}
