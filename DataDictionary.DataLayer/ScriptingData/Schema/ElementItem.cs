// Ignore Spelling: Nillable

using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    public interface IElementItem : ISchemaKey, IElementKey, IColumnKey, IScopeKeyName
    {

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
    public class ElementItem : BindingTableRow, IElementItem, ISerializable
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
        public ElementItem() : base()
        { if (ElementId is null) { ElementId = Guid.NewGuid(); } }

        /// <summary>
        /// Constructor for Schema Element Items
        /// </summary>
        /// <param name="key"></param>
        public ElementItem(ISchemaKey key) : this()
        { SchemaId = key.SchemaId; }

        /// <summary>
        /// Constructor for Schema Element Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="column"></param>
        public ElementItem(ISchemaKey key, IColumnItem column) : this (key)
        {
            ScopeName = column.ScopeName;
            ColumnName = column.ColumnName;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ElementId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SchemaId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ElementName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ElementType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementNillable", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("AsElement", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("AsAttribute", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("DataAsText", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("DataAsCData", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("DataAsXml", typeof(Boolean)){ AllowDBNull = true},
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
        protected ElementItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0} {1}",ScopeName, ColumnName); }
    }
}
