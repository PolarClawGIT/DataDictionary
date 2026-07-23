using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
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
        public SchemaNodeValue? SchemaNode
        {
            get { return field; }

            set
            {
                if (value is not null && field is null)
                {
                    value.ObjectScope = Builder.ObjectScope;
                    value.ObjectProperty = Builder.ObjectProperty;
                    value.RenderValueAs = Builder.RenderValueAs;

                    value.RowStateChanged += Value_RowStateChanged;
                    value.PropertyChanged += Value_PropertyChanged;
                    field = value;
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

        public DataRowState RowState()
        {
            if (SchemaNode is not null) { return SchemaNode.RowState(); }
            else { return DataRowState.Detached; }
        }

        public ScopeType ObjectScope { get { return Builder.ObjectScope; } }

        public String? ObjectProperty { get { return Builder.ObjectProperty; } }

        public ObjectValueType ObjectType { get { return Builder.ObjectType; } }

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
                else { Builder.NodeName = value ?? String.Empty; }
            }
        }

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
                else { Builder.RenderOrder = value; }
            }
        }

        public NodeRenderAsType RenderValueAs
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.RenderValueAs; }
                else { return Builder.RenderValueAs; }
            }

            set
            {
                if (SchemaNode is not null) { SchemaNode.RenderValueAs = value; }
                else { Builder.RenderValueAs = value; }
            }
        }

        public XmlTypeCode RenderTypeAs
        {
            get
            {
                if (SchemaNode is not null) { return SchemaNode.RenderTypeAs; }
                else { return Builder.RenderTypeAs; }
            }

            set
            {
                if (SchemaNode is not null) { SchemaNode.RenderTypeAs = value; }
                else { Builder.RenderTypeAs = value; }
            }
        }

        public Boolean IsReadOnly { get { return SchemaNode is not null; } }


        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        public override String ToString()
        { return Builder.ToString(); }

    }

    /// <summary>
    /// Wrapper list class used with the SchemaNodeTreeView to provided a list to bind to.
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
