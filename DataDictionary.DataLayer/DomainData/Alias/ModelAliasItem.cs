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
    public interface IModelAliasItem : IAliasKeyName, IScopeKey
    { }

    /// <summary>
    /// Implementation for Model Alias Item.
    /// </summary>
    public abstract class ModelAliasItem : IModelAliasItem
    {
        /// <inheritdoc/>
        public virtual String AliasName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public virtual ScopeType ScopeId { get; init; } = ScopeType.Null;

        /// <summary>
        /// System Source Id of the Model Alias item.
        /// </summary>
        public virtual Guid SystemSourceId { get; init; } = Guid.Empty;
    }

    /// <summary>
    /// Implementation for Model Alias Item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelAliasItem<T> : ModelAliasItem
        where T : class, IToScopeType, IToAliasName
    {
        /// <summary>
        /// Source of the Model Alias
        /// </summary>
        public virtual T? Source { get; init; }
    }
}
