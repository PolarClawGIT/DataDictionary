using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Data Source.
    /// </summary>
    public interface IDataSourceKey : IKey
    {
        /// <summary>
        /// Data Source Id of the Scripting Data Source.
        /// </summary>
        Guid? DataSourceId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Data Source.
    /// </summary>
    public class DataSourceKey : IDataSourceKey,
        IKeyEquality<IDataSourceKey>, IKeyEquality<DataSourceKey>
    {
        /// <inheritdoc/>
        public Guid? DataSourceId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty DataSource Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public DataSourceKey() : base()
        { }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Data Source.
        /// </summary>
        /// <param name="source"></param>
        public DataSourceKey(IDataSourceKey source) : base()
        {
            if (source.DataSourceId is Guid) { DataSourceId = source.DataSourceId; }
            else { DataSourceId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DataSourceKey? other)
        { return other is DataSourceKey key && key.DataSourceId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(DataSourceId, other.DataSourceId); }

        /// <inheritdoc/>
        public Boolean Equals(IDataSourceKey? other)
        { return other is IDataSourceKey value && Equals(new DataSourceKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDataSourceKey value && Equals(new DataSourceKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DataSourceKey left, DataSourceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DataSourceKey left, DataSourceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (DataSourceId is Guid) { return DataSourceId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
