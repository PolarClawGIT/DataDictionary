using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    /// <summary>
    /// Wrapper item class used to connect the XMLBuilder to the SchemaNodeValue
    /// </summary>
    class XmlBuilderValue : IXmlBuilder, IBindingPropertyChanged, IBindingRowState
    {
        public required XmlBuilder Builder { get; init; }

        /// <summary>
        /// Sets the Schema Node linked to the XmlBuilder.
        /// </summary>
        public SchemaNodeValue? SchemaNode
        {
            get { return field; }

            set
            {
                if (value is not null && field is null)
                {
                    if(value.ObjectScope is ScopeType.Null)
                    {   // The incoming SchemaNode is "blank", fill it with defaults.
                        value.ObjectScope = Builder.ObjectScope;
                        value.ObjectProperty = Builder.ObjectProperty;
                        value.NodeName = Builder.NodeName;
                        value.RenderNodeType = Builder.RenderNodeType;
                        value.RenderTypeCode = Builder.RenderTypeCode;
                        value.RenderOrder = Builder.RenderOrder;
                    }

                    value.RowStateChanged += Value_RowStateChanged;
                    value.PropertyChanged += Value_PropertyChanged;
                    field = value;

                    this.OnPropertyChanged(PropertyChanged, nameof(IsOverride));
                    this.OnPropertyChanged(PropertyChanged, nameof(ObjectScope));
                    this.OnPropertyChanged(PropertyChanged, nameof(ObjectProperty));
                    this.OnPropertyChanged(PropertyChanged, nameof(NodeName));
                    this.OnPropertyChanged(PropertyChanged, nameof(RenderNodeType));
                    this.OnPropertyChanged(PropertyChanged, nameof(RenderTypeCode));
                    this.OnPropertyChanged(PropertyChanged, nameof(RenderOrder));
                    this.OnPropertyChanged(PropertyChanged, nameof(SchemaNode));
                }
                else if (value is null && field is not null)
                {
                    field.RowStateChanged -= Value_RowStateChanged;
                    field.PropertyChanged -= Value_PropertyChanged;
                    field = null;
                }
            }
        }

        private void Value_RowStateChanged(Object? sender, RowStateEventArgs e)
        {
            if (RowStateChanged is EventHandler<RowStateEventArgs> handler)
            { handler(this, e); }
        }

        private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        { this.OnPropertyChanged(PropertyChanged, nameof(e.PropertyName)); }

        /// <inheritdoc/>
        public ScopeType ObjectScope { get { return Builder.ObjectScope; } }

        /// <inheritdoc/>
        public String? ObjectProperty { get { return Builder.ObjectProperty; } }

        /// <inheritdoc/>
        public ObjectValueType ObjectType { get { return Builder.ObjectType; } }

        /// <inheritdoc/>
        public String? NodeName
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.NodeName; }
                else { return Builder.NodeName; }
            }

            set
            {
                if (SchemaNode is not null) { SchemaNode.NodeName = value; }
                this.OnPropertyChanged(PropertyChanged, nameof(NodeName));
            }
        }

        /// <inheritdoc/>
        public Int32? RenderOrder
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.RenderOrder; }
                else { return Builder.RenderOrder; }
            }

            set
            {   
                if (SchemaNode is not null) { SchemaNode.RenderOrder = value; }
                this.OnPropertyChanged(PropertyChanged, nameof(RenderOrder));
            }
        }

        /// <summary>
        /// This is Override. No SchemaNode is associated with this instance.
        /// </summary>
        public Boolean IsOverride { get { return SchemaNode is not null; } }

        /// <inheritdoc/>
        public XmlNodeType RenderNodeType
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.RenderNodeType; }
                else { return Builder.RenderNodeType; }
            }

            set
            {
                if (SchemaNode is not null) { SchemaNode.RenderNodeType = value; }
                this.OnPropertyChanged(PropertyChanged, nameof(RenderNodeType));
            }
        }

        /// <inheritdoc/>
        public XmlTypeCode RenderTypeCode
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.RenderTypeCode; }
                else { return Builder.RenderTypeCode; }
            }

            set
            {
                if (SchemaNode is not null) { SchemaNode.RenderTypeCode = value; }
                this.OnPropertyChanged(PropertyChanged, nameof(RenderTypeCode));
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public DataRowState RowState()
        {
            if (SchemaNode is not null) { return SchemaNode.RowState(); }
            else { return DataRowState.Detached; }
        }

        /// <inheritdoc/>
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        /// <inheritdoc/>
        public override String ToString()
        { return Builder.ToString(); }

    }

    /// <summary>
    /// Wrapper list class used with the XmlBuilderTreeView to provided a list to bind to.
    /// </summary>
    class XmlBuilderData : BindingList<XmlBuilderValue>, IBindingList<XmlBuilderValue>
    {
        public void Load(SchemaDefinitionIndex key, ISchemaNodeData data)
        {
            Clear();

            foreach (var item in BusinessData.Templates.XmlBuilders.Values)
            {
                XmlBuilderValue newValue = new XmlBuilderValue() { Builder = item };
                XmlBuilderIndex builderKey = new XmlBuilderIndex(item);

                if (data.Where(w => key.Equals(w) && builderKey.Equals(w)) is SchemaNodeValue value)
                { newValue.SchemaNode = value; }
                else { newValue.SchemaNode = null; }

                Add(newValue);
            }
        }
    }
}
