﻿using DataDictionary.BusinessLayer.NameSpace;
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
    public interface IGetNamedScopeKey
    {
        /// <summary>
        /// Method to get System Id of the Named Scope item.
        /// </summary>
        NamedScopeKey GetKey();
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
        /// Constructor for the NameScope Key
        /// </summary>
        internal NamedScopeKey(Guid? source) : this()
        { SystemId = source ?? Guid.Empty; }

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
