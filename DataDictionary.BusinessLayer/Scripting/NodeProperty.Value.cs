using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for the Scripting Node Property data.
    /// </summary>
    public interface INodePropertyValue : INodePropertyIndex, ITemplateNodeIndexName
    {
        /// <inheritdoc cref="DataColumn.AllowDBNull"/>
        Boolean AllowDBNull { get; }

        /// <inheritdoc cref="DataColumn.DataType"/>
        Type DataType { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Node Property data.
    /// </summary>
    /// <remarks>The items are expected to be static.</remarks>
    public class NodePropertyValue : INodePropertyValue
    {
        /// <inheritdoc/>
        public ScopeType PropertyScope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String PropertyName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public Boolean AllowDBNull { get; init; } = false;

        /// <inheritdoc/>
        public Type DataType { get; init; } = typeof(object);

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public NodePropertyValue() : base()
        { }

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public NodePropertyValue(ScopeType scope, DataColumn source) : this()
        {
            PropertyScope = scope;
            PropertyName = source.ColumnName;
            AllowDBNull = source.AllowDBNull;
            DataType = source.DataType;
        }

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0} {1}", PropertyScope.ToName(), PropertyName); }
    }
}
