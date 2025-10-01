using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the unique Name of a DataSource.
    /// </summary>
    public interface IDataSourceKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting DataSource
        /// </summary>
        String? DataSourceTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a DataSource.
    /// </summary>
    public class DataSourceKeyName : IDataSourceKeyName,
        IKeyComparable<IDataSourceKeyName>, IKeyComparable<DataSourceKeyName>
    {
        /// <inheritdoc/>
        public String DataSourceTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the DataSource Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DataSourceKeyName(IDataSourceKeyName source) : base()
        {
            if (source.DataSourceTitle is string) { DataSourceTitle = source.DataSourceTitle; }
            else { DataSourceTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DataSourceKeyName? other)
        {
            return
                other is DataSourceKeyName &&
                !string.IsNullOrEmpty(DataSourceTitle) &&
                !string.IsNullOrEmpty(other.DataSourceTitle) &&
                DataSourceTitle.Equals(other.DataSourceTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDataSourceKeyName? other)
        { return other is IDataSourceKeyName value && Equals(new DataSourceKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDataSourceKeyName value && Equals(new DataSourceKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DataSourceKeyName? other)
        {
            if (other is DataSourceKeyName value)
            { return string.Compare(DataSourceTitle, value.DataSourceTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDataSourceKeyName? other)
        { if (other is IDataSourceKeyName value) { return CompareTo(new DataSourceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDataSourceKeyName value) { return CompareTo(new DataSourceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DataSourceKeyName left, DataSourceKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DataSourceKeyName left, DataSourceKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DataSourceKeyName left, DataSourceKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DataSourceKeyName left, DataSourceKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DataSourceKeyName left, DataSourceKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DataSourceKeyName left, DataSourceKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DataSourceTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (DataSourceTitle is string) { return DataSourceTitle; }
            else { return string.Empty; }
        }
    }
}
