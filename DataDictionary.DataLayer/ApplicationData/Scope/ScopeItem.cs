using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Interface for the Scope data.
    /// </summary>
    public interface IScopeItem : IScopeKey, IScopeKeyUnique, IDataItem
    {
        /// <summary>
        /// Description of the Scope
        /// </summary>
        String? ScopeDescription { get; }
    }

    /// <summary>
    /// Implementation of the Scope data.
    /// </summary>
    [Serializable]
    public class ScopeItem : BindingTableRow, IScopeItem, ISerializable
    {
        /// <inheritdoc/>
        public int? ScopeId => throw new NotImplementedException();

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } set { SetValue("ScopeName", value); } }

        /// <inheritdoc/>
        public string? ScopeDescription { get { return GetValue("ScopeDescription"); } set { SetValue("ScopeDescription", value); } }

        /// <summary>
        /// Constructor for the Scope Data.
        /// </summary>
        public ScopeItem() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ScopeId", typeof(Int32)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ScopeDescription", typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ScopeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
