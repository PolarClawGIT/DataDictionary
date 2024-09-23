using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for the Data Layer Index
    /// </summary>
    public interface IDataIndex : IKey
    {
        /// <summary>
        /// System Id of the a Data Layer value.
        /// </summary>
        public Guid SystemId { get; }
    }

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
    /// Interface for the Data Layer general Index
    /// </summary>
    /// <remarks>
    /// This is a common index used the DataLayer.
    /// Implementation, it is just a GUID.
    /// Logically, this is the parent class to all the GUID's used be the database.
    /// </remarks>
    public class DataIndex : IDataIndex,
        IKeyComparable<IDataIndex>, IKeyComparable<DataIndex>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; internal init; } = Guid.Empty;

        internal DataIndex() : base() { }

        /// <summary>
        /// Constructor for the DataLayer Key
        /// </summary>
        /// <param name="source" ></param>
        public DataIndex(IDataIndex source) : this()
        { SystemId = source.SystemId; }

        /// <inheritdoc cref="Nullable{T}.HasValue"/>
        public Boolean HasValue { get { return SystemId != Guid.Empty; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DataIndex? other)
        { return other is DataIndex && SystemId.Equals(other.SystemId); }


        /// <inheritdoc/>
        public virtual Boolean Equals(IDataIndex? other)
        { return other is IDataIndex value && Equals(new DataIndex(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDataIndex value && Equals(new DataIndex(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DataIndex? other)
        {
            if (other is DataIndex value)
            { return SystemId.CompareTo(other.SystemId); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDataIndex? other)
        { if (other is IDataIndex value) { return CompareTo(new DataIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDataIndex value) { return CompareTo(new DataIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DataIndex left, DataIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DataIndex left, DataIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DataIndex left, DataIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DataIndex left, DataIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DataIndex left, DataIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DataIndex left, DataIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return SystemId.GetHashCode(); }
        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        {
            if (SystemId is Guid value) { return value.ToString(); }
            else { return base.ToString(); }
        }
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
