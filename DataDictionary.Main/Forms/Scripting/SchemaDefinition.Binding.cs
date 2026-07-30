using DataDictionary.BusinessLayer.AppScripting;
using System.Data;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        partial class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderValue> NodeData { get; }
            XmlBuilderData nodeValues = new XmlBuilderData();

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding) : base()
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
