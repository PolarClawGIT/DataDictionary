using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer;
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
using DataDictionary.DataLayer.ScriptingData.Schema;
using DataDictionary.DataLayer.ScriptingData.Selection;
using DataDictionary.DataLayer.ScriptingData.Transform;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// 
    /// </summary>
    public interface INameScopeGetKey
    {
        /// <summary>
        /// Method to get System Id of the Named Scope item.
        /// </summary>
        NamedScopeKey GetSystemId();
    }

    /// <summary>
    /// Interface for the NameScope Key
    /// </summary>
    public interface INamedScopeKey : IKey
    {
        /// <summary>
        /// System Id of the Named Scope item.
        /// </summary>
        public Guid SystemId { get; }
    }

    /// <summary>
    /// Implementation for the Named Scope Key
    /// </summary>
    public class NamedScopeKey : INamedScopeKey, IKeyComparable<INamedScopeKey>
    {
        /// <inheritdoc/>
        public Guid SystemId { get; init; } = Guid.Empty;

        internal NamedScopeKey() : base() { }

        /// <summary>
        /// Constructor for the NameScope Key
        /// </summary>
        /// <param name="source" >A ModelNameSpace</param>
        public NamedScopeKey(INamedScopeKey source) : this()
        { SystemId = source.SystemId; }

        /// <summary>
        /// Constructor for the NameScope Key, Application Help
        /// </summary>
        /// <param name="source"></param>
        public NamedScopeKey(IHelpKey source) : this()
        { SystemId = source.HelpId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Catalog
        /// </summary>
        /// <param name="source">A Database Catalog</param>
        public NamedScopeKey(IDbCatalogKey source) : this()
        { SystemId = source.CatalogId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Schema
        /// </summary>
        /// <param name="source">A Database Schema</param>
        public NamedScopeKey(IDbSchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Table
        /// </summary>
        /// <param name="source">A Database Table</param>
        public NamedScopeKey(IDbTableKey source) : this()
        { SystemId = source.TableId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Column
        /// </summary>
        /// <param name="source">A Database Table Column</param>
        public NamedScopeKey(IDbTableColumnKey source) : this()
        { SystemId = source.ColumnId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Domain
        /// </summary>
        /// <param name="source">A Database Domain</param>
        public NamedScopeKey(IDbDomainKey source) : this()
        { SystemId = source.DomainId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Routine
        /// </summary>
        /// <param name="source">A Database Routine</param>
        public NamedScopeKey(IDbRoutineKey source) : this()
        { SystemId = source.RoutineId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Parameter
        /// </summary>
        /// <param name="source">A Database Routine Parameter</param>
        public NamedScopeKey(IDbRoutineParameterKey source) : this()
        { SystemId = source.ParameterId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Constraint
        /// </summary>
        /// <param name="source">A Database Constraint</param>
        public NamedScopeKey(IDbConstraintKey source) : this()
        { SystemId = source.ConstraintId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Library
        /// </summary>
        /// <param name="source">A Library Source</param>
        public NamedScopeKey(ILibrarySourceKey source) : this()
        { SystemId = source.LibraryId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Library Member
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(ILibraryMemberKey source) : this()
        { SystemId = source.MemberId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Library Member
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(ILibraryMemberKeyParent source) : this()
        { SystemId = source.MemberParentId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Model
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(IModelKey source) : this()
        { SystemId = source.ModelId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Subject Area
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(IModelSubjectAreaKey source) : this()
        { SystemId = source.SubjectAreaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Attribute
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(IDomainAttributeKey source) : this()
        { SystemId = source.AttributeId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Entity
        /// </summary>
        /// <param name="source">A Library Member</param>
        public NamedScopeKey(IDomainEntityKey source) : this()
        { SystemId = source.EntityId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Scripting Schema
        /// </summary>
        /// <param name="source">A Scripting Schema</param>
        public NamedScopeKey(ISchemaKey source) : this()
        { SystemId = source.SchemaId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Scripting Transform
        /// </summary>
        /// <param name="source">A Scripting Transform</param>
        public NamedScopeKey(ITransformKey source) : this()
        { SystemId = source.TransformId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameScope Key, Scripting Selection
        /// </summary>
        /// <param name="source">A Scripting Transform</param>
        public NamedScopeKey(ISelectionKey source) : this()
        { SystemId = source.SelectionId ?? Guid.Empty; }

        /// <summary>
        /// Constructor for the NameSpace Key
        /// </summary>
        /// <param name="source"></param>
        public NamedScopeKey(INameSpaceItem source) : this()
        { SystemId = source.SystemId; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(INamedScopeKey? other)
        {
            return
                other is INamedScopeKey &&
                SystemId.Equals(other.SystemId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INamedScopeKey value && Equals(new NamedScopeKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(INamedScopeKey? other)
        {
            if (other is null) { return 1; }
            else { return SystemId.CompareTo(other.SystemId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is INamedScopeKey value) { return CompareTo(new NamedScopeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(NamedScopeKey left, NamedScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NamedScopeKey left, NamedScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NamedScopeKey left, NamedScopeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NamedScopeKey left, NamedScopeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NamedScopeKey left, NamedScopeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NamedScopeKey left, NamedScopeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
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
}
