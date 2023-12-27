using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for a Model Alias Item.
    /// </summary>
    public interface IModelAliasItem : IAliasKeyName, IModelAliasKey, IScopeKey
    {
        /// <summary>
        /// Name of the item (short).
        /// </summary>
        String ItemName { get; }

        /// <summary>
        /// Parent System Id
        /// </summary>
        Guid SystemParentId { get; }

        /// <summary>
        /// The Position/Order of the Item within Like Items
        /// </summary>
        Int32 OrdinalPosition { get; }
    }

    /// <summary>
    /// Implementation for Model Alias Item.
    /// </summary>
    public class ModelAliasItem : IModelAliasItem
    {
        /// <inheritdoc/>
        public virtual String AliasName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public virtual ScopeType ScopeId { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual Guid SystemId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Guid SystemParentId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual String ItemName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = Int32.MaxValue;

        /// <summary>
        /// List of keys that are the children of this record.
        /// </summary>
        public virtual List<ModelAliasKey> Children { get; } = new List<ModelAliasKey>();

        /// <summary>
        /// Source of the Model Alias.
        /// TODO: Is this required?
        /// </summary>
        public virtual Object? Source { get; init; }

        internal ModelAliasItem() : base() { }

        /// <summary>
        /// Constructor for a ModelAliasItem
        /// </summary>
        /// <param name="source"></param>
        public ModelAliasItem(IModelAliasItem source) : this()
        {
            if (!String.IsNullOrWhiteSpace(source.AliasName)) { AliasName = source.AliasName; }
            ScopeId = source.ScopeId;
            SystemId = source.SystemId;
            SystemParentId = source.SystemParentId;
            if (!String.IsNullOrWhiteSpace(source.AliasName)) { ItemName = source.ItemName; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        { return AliasName; }
    }

    /// <summary>
    /// Implementation for Model Alias Item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelAliasItem<T> : ModelAliasItem
        where T : class//, IToScopeType, IToAliasName
    {
        /// <summary>
        /// Source of the Model Alias
        /// </summary>
        public new T? Source { get { return base.Source as T; } init { base.Source = value; } }

        internal ModelAliasItem() : base() { }
    }
}
