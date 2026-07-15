using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        partial class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<SchemaNodeTreeViewBindingItem> NodeData { get; }
            SchemaNodeTreeViewBindingList nodeValues = new SchemaNodeTreeViewBindingList();

            public class NodeValue : IXmlBuilder, IBindingPropertyChanged, IBindingRowState
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

                public Boolean IsReadOnly { get { return SchemaNode is not null; } }

                public event PropertyChangedEventHandler? PropertyChanged;
                public event EventHandler<RowStateEventArgs>? RowStateChanged;

                public override String ToString()
                { return Builder.ToString(); }
            }

            class NodeList : BindingList<NodeValue>, IBindingList<NodeValue>
            { }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                NodeData = new DataBinding<SchemaNodeTreeViewBindingItem>(nodeBinding, () => nodeValues);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }



            public Boolean TrySetNode(XmlBuilderIndex key)
            { return NodeData.TrySetValue(w => key.Equals(w)); }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();

                SchemaData.LoadBinding(w => key.Equals(w));
                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { templateKey = new TemplateIndex(schemaValue); }

                TemplateData.LoadBinding(w => templateKey.Equals(w));

                nodeValues.Load(key, GetData().SchemataNodes);
                NodeData.LoadBinding();
            }

            protected void RemoveValue(SchemaDefinitionIndex key)
            {
                GetData().Schemata.Remove(key);
                GetData().SchemataNodeOwners.Remove(key);
                GetData().SchemataNodes.Remove(key);
                GetData().SchemaDocuments.Remove(key);
                throw new NotImplementedException();
            }

            public IEnumerable<XmlBuilder> GetBuilders()
            { return nodeValues.Select(s => s.Builder); }
        }
    }
}
