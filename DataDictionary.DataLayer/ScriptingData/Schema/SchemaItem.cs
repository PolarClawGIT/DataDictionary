using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema data.
    /// </summary>
    public interface ISchemaItem : ISchemaKey, IScopeKey
    {
        /// <summary>
        /// Title of the Schema.
        /// </summary>
        String? SchemaTitle { get; }

        /// <summary>
        /// Description of the Schema.
        /// </summary>
        String? SchemaDescription { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema data.
    /// </summary>
    [Serializable]
    public class SchemaItem: BindingTableRow, ISchemaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>("SchemaId"); }
            protected set { SetValue("SchemaId", value); }
        }

        /// <inheritdoc/>
        public string? SchemaTitle { get { return GetValue("SchemaTitle"); } set { SetValue("SchemaTitle", value); } }

        /// <inheritdoc/>
        public string? SchemaDescription { get { return GetValue("SchemaDescription"); } set { SetValue("SchemaDescription", value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingSchema; } }

        /// <summary>
        /// Constructor for Scripting Transform
        /// </summary>
        public SchemaItem() : base()
        {
            if (SchemaId is null) { SchemaId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SchemaTitle)) { SchemaTitle = "(new Schema)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("SchemaId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SchemaTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaDescription", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return SchemaTitle ?? String.Empty; }
    }
}
