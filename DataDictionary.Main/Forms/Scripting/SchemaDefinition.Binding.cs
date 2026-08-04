using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;
            public Action<XmlBuilderValue> OnSchemaChanged { get; init; } = (value) => { return; };

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderValue> BuilderData { get; }
            BindingView<SchemaNodeValue> schemaNodes;
            XmlBuilderData nodeValues = new XmlBuilderData();

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes);
                BuilderData = new DataBinding<XmlBuilderValue>(nodeBinding, () => nodeValues);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);

                schemaNodes.ListChanged += SchemaNodes_ListChanged;

                void SchemaNodes_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    // Reset list
                    foreach (var builder in BuilderData)
                    {
                        XmlBuilderIndex key = new XmlBuilderIndex(builder);
                        SchemaNodeValue? node = schemaNodes.FirstOrDefault(w => key.Equals(w));

                        if (builder.SchemaNode is null && node is not null)
                        { builder.SchemaNode = node; }
                        else if (builder.SchemaNode is not null && node is null)
                        { builder.SchemaNode = null; }

                        OnSchemaChanged(builder);
                    }
                }
            }

            public Boolean TrySetNode(XmlBuilderIndex key)
            { return BuilderData.TrySetValue(w => key.Equals(w)); }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                SchemaData.LoadBinding(w => key.Equals(w));
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes, w => key.Equals(w));

                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { TemplateData.LoadBinding(w => new TemplateIndex(schemaValue).Equals(w)); }
                else
                { TemplateData.LoadBinding(w => new TemplateIndex().Equals(w)); }

                nodeValues.Load(key, GetData().SchemataNodes);
                BuilderData.LoadBinding();
            }

            protected void RemoveValue(SchemaDefinitionIndex key)
            {
                GetData().Schemata.Remove(key);
                GetData().SchemataNodes.Remove(key);
                GetData().SchemaDocuments.Remove(key);
                throw new NotImplementedException();
            }

            public IEnumerable<XmlBuilder> GetBuilders()
            { return nodeValues.Select(s => s.Builder); }
        }
    }
}
