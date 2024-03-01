using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameScope
{
    /// <summary>
    /// Interface for a NameSpace item within a NameScope
    /// </summary>
    public interface INameSpaceItem : INameScopeKey, INameSpaceKey, IScopeKey
    {

    }

    /// <summary>
    /// Implementation for a NameSpace item within a NameScope
    /// </summary>
    public class NameSpaceItem : NameSpaceKey, INameSpaceItem
    {
        /// <inheritdoc/>
        public Guid SystemId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public ScopeType Scope { get; init; }

        /// <summary>
        /// Constructor for NameSpace item
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        public NameSpaceItem(INameSpaceKey source, ScopeType scope = ScopeType.ModelNameSpace) : base(source)
        {   Scope = scope; }
    }
}
