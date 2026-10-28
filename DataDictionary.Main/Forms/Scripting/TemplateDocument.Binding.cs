using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateDocument
    {
        class FormBinding : PresenterData<DocumentIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<TransformValue> TransformData { get; }
            public DataBinding<TemplateDocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource transformBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TemplateDocumentValue>(documentBinding, () => GetData().Documents);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }



            public override void LoadValue(DocumentIndex key)
            {
                DocumentData.LoadBinding(w => key.Equals(w));

                if(DocumentData.TryGetSingle(out TemplateDocumentValue? document))
                {
                    TemplateIndex templateKey = new TemplateIndex(document);

                    TemplateData.LoadBinding(w => templateKey.Equals(w));
                    SchemaData.LoadBinding(w => templateKey.Equals(w));
                    TransformData.LoadBinding(w => templateKey.Equals(w));
                }
            }

            public Boolean TryCreate(TemplateIndex template, ref DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(template, out TemplateDocumentValue? value, out exception))
                { document = new DocumentIndex(value); }

                return exception is null;
            }
        }
    }
}
