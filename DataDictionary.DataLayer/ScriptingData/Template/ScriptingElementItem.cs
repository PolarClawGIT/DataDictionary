using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template Element data.
    /// </summary>
    public interface IScriptingElementItem : IScriptingTemplateKey, IScriptingElementKey, IElementDataAsType, IScopeKey
    {
        /// <summary>
        /// Application Scope of the Property
        /// </summary>
        ScopeType PropertyScope { get; }

        /// <summary>
        /// Name of the Property
        /// </summary>
        String? PropertyName { get; }

        /// <summary>
        /// Script the Property as an Element
        /// </summary>
        Boolean AsElement { get; }

        /// <summary>
        /// Script the Property as an Attribute.
        /// </summary>
        Boolean AsAttribute { get; }

        /// <summary>
        /// Name of the Element/Attribute for the Property
        /// </summary>
        String? ElementName { get; }

        /// <summary>
        /// The XS Type of the Element/Attribute
        /// </summary>
        String? ElementType { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Element data.
    /// </summary>
    [Serializable]
    public class ScriptingElementItem : BindingTableRow, IScriptingElementItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? ElementId
        {
            get { return GetValue<Guid>(nameof(ElementId)); }
            protected set { SetValue(nameof(ElementId), value); }
        }

        /// <inheritdoc/>
        public ScopeType PropertyScope
        {
            get { return ScopeKey.Parse(GetValue(nameof(PropertyScope)) ?? String.Empty).Scope; }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(PropertyScope), null); }
                else { SetValue(nameof(PropertyScope), value.ToName()); }
            }
        }

        /// <inheritdoc/>
        public String? PropertyName
        {
            get { return GetValue(nameof(PropertyName)); }
            set { SetValue(nameof(PropertyName), value); }
        }

        /// <inheritdoc/>
        public Boolean AsElement
        {
            get
            {
                if (GetValue<Boolean>(nameof(AsElement), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            { SetValue<Boolean>(nameof(AsElement), value); OnPropertyChanged(nameof(AsAttribute)); }
        }

        /// <inheritdoc/>
        public Boolean AsAttribute
        {
            get
            {
                if (GetValue<Boolean>(nameof(AsElement), BindingItemParsers.BooleanTryParse) == false) { return true; }
                else { return false; }
            }
            set
            { SetValue<Boolean>(nameof(AsElement), !value); OnPropertyChanged(nameof(AsAttribute)); }
        }

        /// <inheritdoc/>
        public String? ElementName
        {
            get { return GetValue(nameof(ElementName)); }
            set { SetValue(nameof(ElementName), value); }
        }

        /// <inheritdoc/>
        public String? ElementType
        {
            get { return GetValue(nameof(ElementType)); }
            set { SetValue(nameof(ElementType), value); }
        }

        /// <inheritdoc/>
        public ElementDataAsType DataAs
        {
            get { return ElementDataAsTypeKey.Parse(GetValue(nameof(DataAs)) ?? String.Empty).DataAs; }
            set { SetValue(nameof(DataAs), new ElementDataAsTypeKey(value).ToString()); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ScriptingTemplateElement;

        /// <summary>
        /// Constructor for Scripting Transform Element
        /// </summary>
        public ScriptingElementItem(IScriptingTemplateKey template) : base()
        {
            TemplateId = template.TemplateId;

            if (ElementId is null) { ElementId = Guid.NewGuid(); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ElementId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AsElement), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(ElementName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ElementType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DataAs), typeof(String)){ AllowDBNull = true},
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
        protected ScriptingElementItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return PropertyName ?? String.Empty; }
    }
}
