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
        /// Parent System Id of the Model Alias item.
        /// </summary>
        public Guid? SystemParentId { get; }

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
        public Guid? SystemParentId { get; init; }

        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal ModelAliasKey() : base() { }

        /*
        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IModelAliasKey source) : this()
        {
            SystemParentId = source.SystemParentId;
            SystemId = source.SystemId;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(IDbCatalogKey source) : this()
        {
            SystemParentId = null;
            SystemId = source.CatalogId??Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbCatalogKey parent, IDbSchemaKey child) : this()
        {
            SystemParentId = parent.CatalogId;
            SystemId = child.SchemaId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbSchemaKey parent, IDbTableKey child) : this()
        {
            SystemParentId = parent.SchemaId;
            SystemId = child.TableId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbTableKey parent, IDbTableColumnKey child) : this()
        {
            SystemParentId = parent.TableId;
            SystemId = child.ColumnId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbSchemaKey parent, IDbDomainKey child) : this()
        {
            SystemParentId = parent.SchemaId;
            SystemId = child.DomainId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbSchemaKey parent, IDbRoutineKey child) : this()
        {
            SystemParentId = parent.SchemaId;
            SystemId = child.RoutineId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbRoutineKey parent, IDbRoutineParameterKey child) : this()
        {
            SystemParentId = parent.RoutineId;
            SystemId = child.ParameterId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(IDbSchemaKey parent, IDbConstraintKey child) : this()
        {
            SystemParentId = parent.SchemaId;
            SystemId = child.ConstraintId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasKey(ILibrarySourceKey source) : this()
        {
            SystemParentId = null;
            SystemId = source.LibraryId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(ILibrarySourceKey parent, ILibraryMemberKey child) : this()
        {
            SystemParentId = parent.LibraryId;
            SystemId = child.MemberId ?? Guid.Empty;
        }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ModelAliasKey(ILibraryMemberKey parent, ILibraryMemberKey child) : this()
        {
            SystemParentId = parent.MemberId;
            SystemId = child.MemberId ?? Guid.Empty;
        }*/

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IModelAliasKey? other)
        {
            return
                other is IModelAliasKey &&
                ((SystemParentId is null && other.SystemParentId is null) ||
                  SystemParentId.Equals(other.SystemParentId)) &&
                SystemId.Equals(other.SystemId) ;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelAliasKey value && Equals(new ModelAliasKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IModelAliasKey? other)
        {
            if (other is null) { return 1; }
            else if (SystemParentId is not null && other.SystemParentId is null) { return 1; }
            else if (SystemParentId is null && other.SystemParentId is not null) { return -1; }
            else if (SystemParentId is Guid parentGuid && parentGuid.CompareTo(other.SystemParentId) is Int32 parentValue && parentValue !=0) { return parentValue; }
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
            SystemParentId.GetHashCode(),
            SystemId.GetHashCode());
        }
        #endregion
    }
}
