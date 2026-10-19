using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformDocument
    {
        class FormBinding : PresenterData<DocumentIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<TransformValue> TransformData { get; }
            public DataBinding<TransformDocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource transformBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TransformDocumentValue>(documentBinding, () => GetData().TransformDocuments);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(DocumentIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();
                TransformIndex transformKey = new TransformIndex();

                DocumentData.LoadBinding(w => key.Equals(w));

                if (DocumentData.TryGetSingle(out TransformDocumentValue? documentValue))
                {
                    templateKey = new TemplateIndex(documentValue);
                    transformKey = new TransformIndex(documentValue);
                }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
                TransformData.LoadBinding(w => transformKey.Equals(w));
            }

            public Boolean TryCreate(TransformIndex transform, ref DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(transform, out TransformDocumentValue? value, out exception))
                { document = new DocumentIndex(value); }

                return exception is null;
            }
        }
    }
}
