using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Model Alias Key
    /// </summary>
    public interface IModelAliasKey : IKey
    {
        /// <summary>
        /// System Id of the Model Alias item.
        /// </summary>
        public Guid SystemId { get; }
    }

    /// <summary>
    /// Implementation for the Model Alias Key
    /// </summary>
    public class ModelAliasKey : IModelAliasKey, IKeyComparable<IModelAliasKey>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal ModelAliasKey() : base() { }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IModelAliasKey source) : this()
        { SystemId = source.SystemId; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbCatalogKey source) : this()
        { SystemId = source.CatalogId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbSchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbTableKey source) : this()
        {SystemId = source.TableId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbTableColumnKey source) : this()
        {SystemId = source.ColumnId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbDomainKey source) : this()
        {SystemId = source.DomainId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbRoutineKey source) : this()
        {SystemId = source.RoutineId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbRoutineParameterKey source) : this()
        {SystemId = source.ParameterId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbConstraintKey source) : this()
        {SystemId = source.ConstraintId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(ILibrarySourceKey source) : this()
        {SystemId = source.LibraryId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(ILibraryMemberKey source) : this()
        {SystemId = source.MemberId ?? Guid.Empty; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IModelAliasKey? other)
        {
            return
                other is IModelAliasKey &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelAliasKey value && Equals(new ModelAliasKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IModelAliasKey? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IModelAliasKey value) { return CompareTo(new ModelAliasKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ModelAliasKey left, ModelAliasKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelAliasKey left, ModelAliasKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ModelAliasKey left, ModelAliasKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ModelAliasKey left, ModelAliasKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ModelAliasKey left, ModelAliasKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ModelAliasKey left, ModelAliasKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                SystemId.GetHashCode());
        }
        #endregion
    }
}
