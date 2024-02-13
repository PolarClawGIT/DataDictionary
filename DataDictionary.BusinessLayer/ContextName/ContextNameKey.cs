using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ContextName
{
    /// <summary>
    /// Interface for the Context Name Key
    /// </summary>
    public interface IContextNameKey : IKey
    {
        /// <summary>
        /// System Id of the Context Name item.
        /// </summary>
        public Guid SystemId { get; }
    }

    /// <summary>
    /// Implementation for the Context Name Key
    /// </summary>
    public class ContextNameKey : IContextNameKey, IKeyComparable<IContextNameKey>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal ContextNameKey() : base() { }

        /// <summary>
        /// Constructor for the Context Name Key
        /// </summary>
        /// <param name="source" >A ModelNameSpace</param>
        public ContextNameKey(IContextNameKey source) : this()
        { SystemId = source.SystemId; }

        /// <summary>
        /// Constructor for the Context Name Key, Application Help
        /// </summary>
        /// <param name="source"></param>
        public ContextNameKey(IHelpKey source): this()
        {   SystemId = source.HelpId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Catalog
        /// </summary>
        /// <param name="source">A Database Catalog</param>
        public ContextNameKey(IDbCatalogKey source) : this()
        { SystemId = source.CatalogId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Schema
        /// </summary>
        /// <param name="source">A Database Schema</param>
        public ContextNameKey(IDbSchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Table
        /// </summary>
        /// <param name="source">A Database Table</param>
        public ContextNameKey(IDbTableKey source) : this()
        {SystemId = source.TableId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Column
        /// </summary>
        /// <param name="source">A Database Table Column</param>
        public ContextNameKey(IDbTableColumnKey source) : this()
        {SystemId = source.ColumnId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Domain
        /// </summary>
        /// <param name="source">A Database Domain</param>
        public ContextNameKey(IDbDomainKey source) : this()
        {SystemId = source.DomainId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Routine
        /// </summary>
        /// <param name="source">A Database Routine</param>
        public ContextNameKey(IDbRoutineKey source) : this()
        {SystemId = source.RoutineId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Parameter
        /// </summary>
        /// <param name="source">A Database Routine Parameter</param>
        public ContextNameKey(IDbRoutineParameterKey source) : this()
        {SystemId = source.ParameterId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Constraint
        /// </summary>
        /// <param name="source">A Database Constraint</param>
        public ContextNameKey(IDbConstraintKey source) : this()
        {SystemId = source.ConstraintId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Library
        /// </summary>
        /// <param name="source">A Library Source</param>
        public ContextNameKey(ILibrarySourceKey source) : this()
        {SystemId = source.LibraryId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Library Member
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ContextNameKey(ILibraryMemberKey source) : this()
        {SystemId = source.MemberId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Model
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ContextNameKey(IModelKey source) : this()
        { SystemId = source.ModelId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Subject Area
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ContextNameKey(IModelSubjectAreaKey source) : this()
        { SystemId = source.SubjectAreaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Attribute
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ContextNameKey(IDomainAttributeKey source) : this()
        { SystemId = source.AttributeId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the Context Name Key, Entity
        /// </summary>
        /// <param name="source">A Library Member</param>
        public ContextNameKey(IDomainEntityKey source) : this()
        { SystemId = source.EntityId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameSpace Key
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>NameSpaces do not have a GUID of their own, so a new GUID is created.</remarks>
        public ContextNameKey(INameSpaceKey source) : this()
        { SystemId = Guid.NewGuid(); }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IContextNameKey? other)
        {
            return
                other is IContextNameKey &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IContextNameKey value && Equals(new ContextNameKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IContextNameKey? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IContextNameKey value) { return CompareTo(new ContextNameKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ContextNameKey left, ContextNameKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ContextNameKey left, ContextNameKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ContextNameKey left, ContextNameKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ContextNameKey left, ContextNameKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ContextNameKey left, ContextNameKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ContextNameKey left, ContextNameKey right)
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
