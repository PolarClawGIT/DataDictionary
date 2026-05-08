using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using System.Data;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        class FormBinding : DataModel<TemplateIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<TransformValue> TransformData { get; }
            public DataBinding<TemplateObjectValue> ObjectData { get; }
            //public DataBinding<DocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource transformBinding,
                BindingSource objectBinding,
                BindingSource documentBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                ObjectData = new DataBinding<TemplateObjectValue>(objectBinding, () => GetData().Objects);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void Load(TemplateIndex key)
            {
                TemplateData.LoadBinding(w => key.Equals(w));
                SchemaData.LoadBinding(w => key.Equals(w));
                TransformData.LoadBinding(w => key.Equals(w));
                ObjectData.LoadBinding(w => key.Equals(w));
            }
        }
    }
}
