using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for the Data Layer Index Name (title)
    /// </summary>
    public interface IDataIndexName : IKey
    {
        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        String Title { get; }
    }

    /// <summary>
    /// Interface for the Data Layer Index Name
    /// </summary>
    /// <remarks>
    /// Most Data Layer object have a Name or Title or both.
    /// This generalizes that into a single value.
    /// </remarks>
    public class DataIndexName : IDataIndexName,
        IKeyComparable<IDataIndexName>, IKeyComparable<DataIndexName>
    {
        /// <inheritdoc/>
        public String Title { get; internal init; } = String.Empty;

        internal DataIndexName() : base() { }

        /// <summary>
        /// Constructor for the DataLayer Key Name
        /// </summary>
        /// <param name="source"></param>
        public DataIndexName(IDataIndexName source) : this()
        { Title = source.Title; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DataIndexName? other)
        {
            return
                other is DataIndexName &&
                !string.IsNullOrEmpty(Title) &&
                !string.IsNullOrEmpty(other.Title) &&
                Title.Equals(other.Title, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDataIndexName? other)
        { return other is IDataIndexName value && Equals(new DataIndexName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDataIndexName value && Equals(new DataIndexName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DataIndexName? other)
        {
            if (other is DataIndexName value)
            { return string.Compare(Title, value.Title, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDataIndexName? other)
        { if (other is IDataIndexName value) { return CompareTo(new DataIndexName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDataIndexName value) { return CompareTo(new DataIndexName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DataIndexName left, DataIndexName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DataIndexName left, DataIndexName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DataIndexName left, DataIndexName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DataIndexName left, DataIndexName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DataIndexName left, DataIndexName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DataIndexName left, DataIndexName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return Title.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (Title is string) { return Title; }
            else { return string.Empty; }
        }
    }
}
