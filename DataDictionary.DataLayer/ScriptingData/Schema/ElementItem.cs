// Ignore Spelling: Nillable

using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema Element data.
    /// </summary>
    public interface IElementItem : ISchemaKey, IElementKey, IScopeKey
    {

        /// <summary>
        /// Name of the Element to be generated. If Null/Empty, the Column Name is used.
        /// </summary>
        String? ElementName { get; }

        /// <summary>
        /// Type of the Element to be generated. If Null/Empty, do not generate the Type. 
        /// </summary>
        String? ElementType { get; }

        /// <inheritdoc cref="ScopeTypeExtension.ToName(ScopeType)"/>
        String? ScopeName { get; }

        /// <summary>
        /// The Nillable of the Element to be generated. If Null, do not generate the Nillable.
        /// </summary>
        Boolean ElementNillable { get; }

        /// <summary>
        /// Generate the Element as an XML Element (not Attribute)
        /// </summary>
        /// <remarks>if AsElement and AsAttribute are both null, the item is not generated</remarks>
        Boolean AsElement { get; }

        /// <summary>
        /// Generate the Element as an XML Attribute (not Element)
        /// </summary>
        /// <remarks>if AsElement and AsAttribute are both null, the item is not generated</remarks>
        Boolean AsAttribute { get; }

        /// <summary>
        /// Generate the data as Text. Attribute generate with "Data=" name.
        /// </summary>
        Boolean DataAsText { get; }

        /// <summary>
        /// Generate the data as CData. 
        /// </summary>
        Boolean DataAsCData { get; }

        /// <summary>
        /// Generate the data as an XML fragment. This will be added as an Element.
        /// </summary>
        Boolean DataAsXml { get; }
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
            get { return GetValue<Guid>(nameof(SchemaId)); }
            protected set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public Guid? ElementId
        {
            get { return GetValue<Guid>(nameof(ElementId)); }
            protected set { SetValue(nameof(ElementId), value); }
        }

        /// <inheritdoc/>
        public String? ColumnName { get { return GetValue(nameof(ColumnName)); } set { SetValue(nameof(ColumnName), value); } }

        /// <inheritdoc/>
        public String? ElementName { get { return GetValue(nameof(ElementName)); } set { SetValue(nameof(ElementName), value); } }

        /// <inheritdoc/>
        public String? ElementType { get { return GetValue(nameof(ElementType)); } set { SetValue(nameof(ElementType), value); } }

        /// <inheritdoc/>
        public Boolean ElementNillable
        {
            get { return GetValue<bool>(nameof(ElementNillable), BindingItemParsers.BooleanTryParse) == true; }
            set { SetValue<Boolean>(nameof(ElementNillable), value); }
        }

        /// <inheritdoc/>
        public Boolean AsElement
        {
            get { return GetValue<bool>(nameof(AsElement), BindingItemParsers.BooleanTryParse) == true; }
            set
            {
                SetValue<Boolean>(nameof(AsElement), value);
                if (value == true) { SetValue<Boolean>(nameof(AsAttribute), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean AsAttribute
        {
            get { return GetValue<bool>(nameof(AsAttribute), BindingItemParsers.BooleanTryParse) == true; }
            set
            {
                SetValue<Boolean>(nameof(AsAttribute), value);
                if (value == true) { SetValue<Boolean>(nameof(AsElement), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean DataAsText
        {
            get { return GetValue<bool>(nameof(DataAsText), BindingItemParsers.BooleanTryParse) == true; }
            set
            {
                if (value == true) { SetValue<Boolean>(nameof(DataAsCData), !value); }
                SetValue<Boolean>(nameof(DataAsText), value);
                if (value == true) { SetValue<Boolean>(nameof(DataAsXml), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean DataAsCData
        {
            get { return GetValue<bool>(nameof(DataAsCData), BindingItemParsers.BooleanTryParse) == true; }
            set
            {
                SetValue<Boolean>(nameof(DataAsCData), value);
                if (value == true) { SetValue<Boolean>(nameof(DataAsText), !value); }
                if (value == true) { SetValue<Boolean>(nameof(DataAsXml), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean DataAsXml
        {
            get { return GetValue<bool>(nameof(DataAsXml), BindingItemParsers.BooleanTryParse) == true; }
            set
            {
                if (value == true) { SetValue<Boolean>(nameof(DataAsCData), !value); }
                if (value == true) { SetValue<Boolean>(nameof(DataAsText), !value); }
                SetValue<Boolean>(nameof(DataAsXml), value);
            }
        }

        /// <inheritdoc/>
        public String? ScopeName { get { return Scope.ToName(); } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(GetValue(nameof(ScopeName)) ?? String.Empty); }
            set { SetValue(nameof(ScopeName), value.ToName()); OnPropertyChanged(nameof(Scope)); }
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

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(ElementId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ScopeName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ElementName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ElementType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ElementNillable), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AsElement), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AsAttribute), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(DataAsText), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(DataAsCData), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(DataAsXml), typeof(Boolean)){ AllowDBNull = true},
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
        { return String.Format("{0} {1}", ScopeName, ColumnName); }
    }
}
