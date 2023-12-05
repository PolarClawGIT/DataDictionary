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
    { }

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

        /// <summary>
        /// List of keys that are the children of this record.
        /// </summary>
        public virtual List<ModelAliasKey> Children { get; } = new List<ModelAliasKey>();

        /// <summary>
        /// Source of the Model Alias
        /// </summary>
        public virtual Object? Source { get; init; }
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
    }
}
