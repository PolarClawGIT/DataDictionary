using DataDictionary.DataLayer;
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

namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Interface for the Model NameSpace Key
    /// </summary>
    public interface IModelNameSpaceKey : IKey
    {
        /// <summary>
        /// System Id of the Model Alias item.
        /// </summary>
        public Guid SystemId { get; }
    }

    /// <summary>
    /// Implementation for the Model NameSpace Key
    /// </summary>
    public class ModelNameSpaceKey : IModelNameSpaceKey, IKeyComparable<IModelNameSpaceKey>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal ModelNameSpaceKey() : base() { }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source" >A ModelAlias</param>
        public ModelNameSpaceKey(IModelNameSpaceKey source) : this()
        { SystemId = source.SystemId; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Catalog</param>
        public ModelNameSpaceKey(IDbCatalogKey source) : this()
        { SystemId = source.CatalogId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Schema</param>
        public ModelNameSpaceKey(IDbSchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Table</param>
        public ModelNameSpaceKey(IDbTableKey source) : this()
        {SystemId = source.TableId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Table Column</param>
        public ModelNameSpaceKey(IDbTableColumnKey source) : this()
        {SystemId = source.ColumnId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Domain</param>
        public ModelNameSpaceKey(IDbDomainKey source) : this()
        {SystemId = source.DomainId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Routine</param>
        public ModelNameSpaceKey(IDbRoutineKey source) : this()
        {SystemId = source.RoutineId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Routine Parameter</param>
        public ModelNameSpaceKey(IDbRoutineParameterKey source) : this()
        {SystemId = source.ParameterId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Database Constraint</param>
        public ModelNameSpaceKey(IDbConstraintKey source) : this()
        {SystemId = source.ConstraintId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Library Source</param>
        public ModelNameSpaceKey(ILibrarySourceKey source) : this()
        {SystemId = source.LibraryId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Model Alias Key
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ModelNameSpaceKey(ILibraryMemberKey source) : this()
        {SystemId = source.MemberId ?? Guid.Empty; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IModelNameSpaceKey? other)
        {
            return
                other is IModelNameSpaceKey &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelNameSpaceKey value && Equals(new ModelNameSpaceKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IModelNameSpaceKey? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IModelNameSpaceKey value) { return CompareTo(new ModelNameSpaceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ModelNameSpaceKey left, ModelNameSpaceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                SystemId.GetHashCode());
        }
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
}
