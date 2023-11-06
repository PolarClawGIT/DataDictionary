﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Interface for the ScopeKey
    /// </summary>
    public interface IScopeKey : IKey
    {
        /// <summary>
        /// Primary Key ID for the Scope
        /// </summary>
        Int32? ScopeId { get; }
    }

    /// <summary>
    /// Implementation of the ScopeKey 
    /// </summary>
    public class ScopeKey : IScopeKey, IKeyEquality<IScopeKey>
    {
        /// <inheritdoc/>
        public Int32? ScopeId { get; init; } = 0;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(IScopeKey source) : base()
        {
            if (source is IScopeKey) { ScopeId = source.ScopeId; }
            else { ScopeId = 0; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IScopeKey? other)
        { return other is IScopeKey && this.ScopeId.HasValue && other.ScopeId.HasValue && EqualityComparer<Int32?>.Default.Equals(this.ScopeId, other.ScopeId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScopeKey value && this.Equals(new ScopeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ScopeKey left, ScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScopeKey left, ScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return (ScopeId).GetHashCode(); }
        #endregion
    }
}
