using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface for the BusinessLayer Index
    /// </summary>
    [Obsolete]
    public interface IDataLayerIndex : IKey
    {
        //TODO: Not sure if this is the best name.

        /// <summary>
        /// System Id of the a Data Layer value.
        /// </summary>
        public Guid DataLayerId { get; }
    }

    /// <summary>
    /// Interface for the DataLayer Index
    /// </summary>
    /// <remarks>
    /// This is a common index used the DataLayer.
    /// Implementation, it is just a GUID.
    /// Logically, this is the parent class to all the GUID's used be the database.
    /// </remarks>
    [Obsolete]
    public partial class DataLayerIndex : IDataLayerIndex, IKeyComparable<IDataLayerIndex>
    {
        /// <inheritdoc/>
        public Guid DataLayerId { get; internal init; } = Guid.Empty;

        internal DataLayerIndex() : base() { }

        /// <summary>
        /// Constructor for the NameScope Key
        /// </summary>
        /// <param name="source" >A ModelNameSpace</param>
        public DataLayerIndex(IDataLayerIndex source) : this()
        { DataLayerId = source.DataLayerId; }

        /// <inheritdoc cref="Nullable{T}.HasValue"/>
        public Boolean HasValue { get { return DataLayerId != Guid.Empty; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDataLayerIndex? other)
        {
            return
                other is IDataLayerIndex &&
                DataLayerId.Equals(other.DataLayerId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDataLayerIndex value && Equals(new DataLayerIndex(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDataLayerIndex? other)
        {
            if (other is null) { return 1; }
            else { return DataLayerId.CompareTo(other.DataLayerId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDataLayerIndex value) { return CompareTo(new DataLayerIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DataLayerIndex left, DataLayerIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DataLayerIndex left, DataLayerIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DataLayerIndex left, DataLayerIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DataLayerIndex left, DataLayerIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DataLayerIndex left, DataLayerIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DataLayerIndex left, DataLayerIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return DataLayerId.GetHashCode(); }
        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        {
            if (DataLayerId is Guid value) { return value.ToString(); }
            else { return base.ToString(); }
        }
    }

}
