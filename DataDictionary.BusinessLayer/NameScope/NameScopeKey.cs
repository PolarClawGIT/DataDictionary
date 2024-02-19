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

namespace DataDictionary.BusinessLayer.NameScope
{
    /// <summary>
    /// Interface for the NameScope Key
    /// </summary>
    public interface INameScopeKey : IKey
    {
        /// <summary>
        /// System Id of the NameScope item.
        /// </summary>
        public Guid SystemId { get; }
    }

    /// <summary>
    /// Implementation for the NameScope Key
    /// </summary>
    public class NameScopeKey : INameScopeKey, IKeyComparable<INameScopeKey>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal NameScopeKey() : base() { }

        /// <summary>
        /// Constructor for the NameScope Key
        /// </summary>
        /// <param name="source" >A ModelNameSpace</param>
        public NameScopeKey(INameScopeKey source) : this()
        { SystemId = source.SystemId; }

        /// <summary>
        /// Constructor for the NameScope Key, Application Help
        /// </summary>
        /// <param name="source"></param>
        public NameScopeKey(IHelpKey source): this()
        {   SystemId = source.HelpId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Catalog
        /// </summary>
        /// <param name="source">A Database Catalog</param>
        public NameScopeKey(IDbCatalogKey source) : this()
        { SystemId = source.CatalogId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Schema
        /// </summary>
        /// <param name="source">A Database Schema</param>
        public NameScopeKey(IDbSchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Table
        /// </summary>
        /// <param name="source">A Database Table</param>
        public NameScopeKey(IDbTableKey source) : this()
        {SystemId = source.TableId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Column
        /// </summary>
        /// <param name="source">A Database Table Column</param>
        public NameScopeKey(IDbTableColumnKey source) : this()
        {SystemId = source.ColumnId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Domain
        /// </summary>
        /// <param name="source">A Database Domain</param>
        public NameScopeKey(IDbDomainKey source) : this()
        {SystemId = source.DomainId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Routine
        /// </summary>
        /// <param name="source">A Database Routine</param>
        public NameScopeKey(IDbRoutineKey source) : this()
        {SystemId = source.RoutineId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Parameter
        /// </summary>
        /// <param name="source">A Database Routine Parameter</param>
        public NameScopeKey(IDbRoutineParameterKey source) : this()
        {SystemId = source.ParameterId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Constraint
        /// </summary>
        /// <param name="source">A Database Constraint</param>
        public NameScopeKey(IDbConstraintKey source) : this()
        {SystemId = source.ConstraintId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Library
        /// </summary>
        /// <param name="source">A Library Source</param>
        public NameScopeKey(ILibrarySourceKey source) : this()
        {SystemId = source.LibraryId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Library Member
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NameScopeKey(ILibraryMemberKey source) : this()
        {SystemId = source.MemberId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Model
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NameScopeKey(IModelKey source) : this()
        { SystemId = source.ModelId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Subject Area
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NameScopeKey(IModelSubjectAreaKey source) : this()
        { SystemId = source.SubjectAreaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Attribute
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NameScopeKey(IDomainAttributeKey source) : this()
        { SystemId = source.AttributeId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Entity
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NameScopeKey(IDomainEntityKey source) : this()
        { SystemId = source.EntityId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameSpace Key
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>NameSpaces do not have a GUID of their own, so a new GUID is created.</remarks>
        public NameScopeKey(INameSpaceKey source) : this()
        { SystemId = Guid.NewGuid(); }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(INameScopeKey? other)
        {
            return
                other is INameScopeKey &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INameScopeKey value && Equals(new NameScopeKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(INameScopeKey? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is INameScopeKey value) { return CompareTo(new NameScopeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(NameScopeKey left, NameScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NameScopeKey left, NameScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NameScopeKey left, NameScopeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NameScopeKey left, NameScopeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NameScopeKey left, NameScopeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NameScopeKey left, NameScopeKey right)
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
