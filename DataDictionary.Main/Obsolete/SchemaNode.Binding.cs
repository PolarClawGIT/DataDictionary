using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode
    {
        [Obsolete]
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;
            public Action<XmlBuilderValue> OnSchemaChanged { get; init; } = (value) => { return; };

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderValue> BuilderData { get; }
            BindingView<SchemaNodeValue> schemaNodes;
            XmlBuilderData nodeValues = new XmlBuilderData();

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding)
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

                        if(builder.SchemaNode is null && node is not null) 
                        { builder.SchemaNode = node; }
                        else if (builder.SchemaNode is not null && node is null)
                        { builder.SchemaNode = null; }

                        OnSchemaChanged(builder);

                        // TODO: Schema Definition and Node screens not synced. Both lists are not updating on Add. 
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

            public void AddNew(ITemplateIndex template, ISchemaDefinitionIndex schema)
            {
                if (BuilderData.TryGetValue(out XmlBuilderValue? builder))
                {
                    SchemaNodeValue value = new SchemaNodeValue(template, schema);
                    builder.SchemaNode = value;

                    schemaNodes.Add(value);
                }
            }

            public void RemoveCurrent()
            {
                if (BuilderData.TryGetValue(out XmlBuilderValue? builder) && builder.SchemaNode != null)
                { schemaNodes.Remove(builder.SchemaNode); }
            }

            public override Boolean Authorize(ButtonType command)
            {
                return base.Authorize(command);
            }

            public IEnumerable<XmlBuilder> GetBuilders()
            { return nodeValues.Select(s => s.Builder); }
        }
    }
}
