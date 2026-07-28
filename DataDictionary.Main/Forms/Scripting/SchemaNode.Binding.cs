using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode
    {
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderValue> NodeData { get; }
            XmlBuilderData nodeValues = new XmlBuilderData();

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding)
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                NodeData = new DataBinding<XmlBuilderValue>(nodeBinding, () => nodeValues);
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

            public void AddNew(ITemplateIndex template, ISchemaDefinitionIndex schema)
            {
                if (NodeData.TryGetValue(out XmlBuilderValue? builder))
                {
                    SchemaNodeValue value = new SchemaNodeValue(template, schema);
                    GetData().SchemataNodes.Add(value);
                    builder.SchemaNode = value;
                    NodeData.ResetCurrent();
                }
            }

            public void RemoveCurrent()
            {
                if (NodeData.TryGetValue(out XmlBuilderValue? builder) && builder.SchemaNode != null)
                {
                    SchemaNodeIndex key = new SchemaNodeIndex(builder.SchemaNode);
                    GetData().SchemataNodes.Remove(key);
                }
            }

            public override Boolean Authorize(ButtonType command)
            {
                return base.Authorize(command);
            }

            public IEnumerable<XmlBuilder> GetBuilders()
            { return GetData().XmlBuilders.Values; }
        }
    }
}
