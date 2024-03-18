// Ignore Spelling: Nillable

using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema Element data.
    /// </summary>
    public interface ISchemaElementItem : ISchemaKey, ISchemaElementKey, IScopeKeyName
    {
        /// <summary>
        /// Name of the Column within the Scope to match to.
        /// </summary>
        String? ColumnName { get; }

        /// <summary>
        /// Name of the Element to be generated. If Null/Empty, the Column Name is used.
        /// </summary>
        String? ElementName { get; }

        /// <summary>
        /// Type of the Element to be generated. If Null/Empty, do not generate the Type. 
        /// </summary>
        String? ElementType { get; }

        /// <summary>
        /// The Nillable of the Element to be generated. If Null, do not generate the Nillable.
        /// </summary>
        Boolean? ElementNillable { get; }

        /// <summary>
        /// Generate the Element as an XML Element (not Attribute)
        /// </summary>
        /// <remarks>if AsElement and AsAttribute are both null, the item is not generated</remarks>
        Boolean? AsElement { get; }

        /// <summary>
        /// Generate the Element as an XML Attribute (not Element)
        /// </summary>
        /// <remarks>if AsElement and AsAttribute are both null, the item is not generated</remarks>
        Boolean? AsAttribute { get; }

        /// <summary>
        /// Generate the data as Text. Attribute generate with "Data=" name.
        /// </summary>
        Boolean? DataAsText { get; }

        /// <summary>
        /// Generate the data as CData. 
        /// </summary>
        Boolean? DataAsCData { get; }

        /// <summary>
        /// Generate the data as an XML fragment. This will be added as an Element.
        /// </summary>
        Boolean? DataAsXml { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Element data.
    /// </summary>
    [Serializable]
    public class SchemaElementItem : BindingTableRow, ISchemaElementItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>("SchemaId"); }
            protected set { SetValue("SchemaId", value); }
        }

        /// <inheritdoc/>
        public Guid? ElementId
        {
            get { return GetValue<Guid>("ElementId"); }
            protected set { SetValue("ElementId", value); }
        }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } set { SetValue("ScopeName", value); } }

        /// <inheritdoc/>
        public string? ColumnName { get { return GetValue("ColumnName"); } set { SetValue("ColumnName", value); } }

        /// <inheritdoc/>
        public string? ElementName { get { return GetValue("ElementName"); } set { SetValue("ElementName", value); } }

        /// <inheritdoc/>
        public string? ElementType { get { return GetValue("ElementType"); } set { SetValue("ElementType", value); } }

        /// <inheritdoc/>
        public bool? ElementNillable
        {
            get { return GetValue<bool>("ElementNillable", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("ElementNillable", value); }
        }

        /// <inheritdoc/>
        public bool? AsElement
        {
            get { return GetValue<bool>("AsElement", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("AsElement", value); }
        }

        /// <inheritdoc/>
        public bool? AsAttribute
        {
            get { return GetValue<bool>("AsAttribute", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("AsAttribute", value); }
        }

        /// <inheritdoc/>
        public bool? DataAsText
        {
            get { return GetValue<bool>("DataAsText", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("DataAsText", value); }
        }

        /// <inheritdoc/>
        public bool? DataAsCData
        {
            get { return GetValue<bool>("DataAsCData", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("DataAsCData", value); }
        }

        /// <inheritdoc/>
        public bool? DataAsXml
        {
            get { return GetValue<bool>("DataAsXml", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("DataAsXml", value); }
        }

        /// <summary>
        /// Constructor for Schema Element Items
        /// </summary>
        public SchemaElementItem() : base()
        { if (ElementId is null) { ElementId = Guid.NewGuid(); } }

        /// <summary>
        /// Constructor for Schema Element Items
        /// </summary>
        /// <param name="key"></param>
        public SchemaElementItem(ISchemaKey key) : this()
        { SchemaId = key.SchemaId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("SchemaId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ElementId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("TransformTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("TransformDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("AsText", typeof(bool)){ AllowDBNull = true},
            new DataColumn("AsXml", typeof(bool)){ AllowDBNull = true},
            new DataColumn("TransformScript", typeof(string)){ AllowDBNull = true},
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
        protected SchemaElementItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0} {1}",ScopeName, ColumnName); }
    }
}
