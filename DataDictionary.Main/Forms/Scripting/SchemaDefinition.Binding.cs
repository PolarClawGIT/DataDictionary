using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        partial class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<SchemaNodeValue> NodeData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                NodeData = new DataBinding<SchemaNodeValue>(nodeBinding, () => GetData().SchemataNodes);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();

                SchemaData.LoadBinding(w => key.Equals(w));
                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { templateKey = new TemplateIndex(schemaValue); }

                NodeData.LoadBinding(w => key.Equals(w));
                TemplateData.LoadBinding(w => templateKey.Equals(w));
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
            { return GetData().XmlBuilders.Values; }
        }
    }
}
