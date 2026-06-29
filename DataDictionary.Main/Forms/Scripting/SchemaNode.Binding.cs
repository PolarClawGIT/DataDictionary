using DataDictionary.BusinessLayer.AppScripting;
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
            public DataBinding<SchemaNodeValue> NodeData { get; }
            public DataBinding<SchemaNodeOwnerValue> OwnerData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding,
                BindingSource ownerBinding)
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                NodeData = new DataBinding<SchemaNodeValue>(nodeBinding, () => GetData().SchemataNodes);
                OwnerData = new DataBinding<SchemaNodeOwnerValue>(ownerBinding, () => GetData().SchemataNodeOwners);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public Boolean TryGetValue([NotNullWhen(true)] out SchemaNodeValue? result)
            {
                if (NodeData.TryGetValue(out SchemaNodeValue? value))
                { result = value; return true; }
                else { result = null; return false; }
            }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();

                SchemaData.LoadBinding(w => key.Equals(w));
                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { templateKey = new TemplateIndex(schemaValue); }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
                NodeData.LoadBinding(w => key.Equals(w));
                OwnerData.LoadBinding(w => key.Equals(w));
            }

            public void AddNew(ITemplateIndex template, ISchemaDefinitionIndex schema)
            { NodeData.Add(new SchemaNodeValue(template, schema)); }

            public void RemoveCurrent()
            {
                if (NodeData.TryGetValue(out SchemaNodeValue? node))
                {
                    SchemaNodeIndex key = new SchemaNodeIndex(node);
                    SchemaNodeOwnerIndex owner = new SchemaNodeOwnerIndex(node);

                    foreach (var item in OwnerData.Where(w => key.Equals(w) || owner.Equals(w)).ToList())
                    { OwnerData.Remove(item); }

                    NodeData.Remove(node);
                }
            }
        }
    }
}
