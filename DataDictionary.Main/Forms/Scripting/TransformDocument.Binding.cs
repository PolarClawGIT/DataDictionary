using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            public IBindingList<SchemaDocumentValue> SchemaDocumentData { get; private set; }


            public FormBinding(
                BindingSource templateBinding,
                BindingSource transformBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TransformDocumentValue>(documentBinding, () => GetData().TransformDocuments);
                SchemaDocumentData = new BindingView<SchemaDocumentValue>(GetData().SchemaDocuments, w => false);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(DocumentIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();
                TransformIndex transformKey = new TransformIndex();
                SchemaDefinitionIndex schemaKey = new SchemaDefinitionIndex();

                DocumentData.LoadBinding(w => key.Equals(w));

                if (DocumentData.TryGetSingle(out TransformDocumentValue? documentValue))
                {
                    templateKey = new TemplateIndex(documentValue);
                    transformKey = new TransformIndex(documentValue);

                    TemplateData.LoadBinding(w => templateKey.Equals(w));
                    TransformData.LoadBinding(w => transformKey.Equals(w));

                    if (TransformData.TryGetCurrent(out TransformValue? transform))
                    {
                        SchemaDefinitionIndex schema = new SchemaDefinitionIndex(transform);
                        SchemaDocumentData = new BindingView<SchemaDocumentValue>(GetData().SchemaDocuments, (w => schema.Equals(w)));

                        transform.PropertyChanged += Transform_PropertyChanged;

                        void Transform_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                        {
                            if (String.Equals(e.PropertyName, nameof(transform.SchemaId)))
                            {
                                schema = new SchemaDefinitionIndex(transform);
                                SchemaDocumentData.RaiseListChangedEvents = false;
                                SchemaDocumentData = new BindingView<SchemaDocumentValue>(GetData().SchemaDocuments, (w => schema.Equals(w)));
                                SchemaDocumentData.RaiseListChangedEvents = true;
                                SchemaDocumentData.ResetBindings();
                            }
                        }
                    }
                }
            }


            public Boolean TryCreate(TransformIndex transform, ref DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(transform, out TransformDocumentValue? value, out exception))
                { document = new DocumentIndex(value); }

                return exception is null;
            }

            public Boolean TryGetSchemaDirectory([NotNullWhen(true)] out IDirectoryValue? directory)
            {
                directory = null;

                if (TransformData.TryGetCurrent(out TransformValue? transform)
                    && GetData().Schemata.
                        TryGetSingle(w => new SchemaDefinitionIndex(transform).Equals(w),out SchemaDefinitionValue? value))
                { directory = value; }

                return directory is not null;
            }

            public Boolean TryGetSchemaDocument([NotNullWhen(true)] out SchemaDocumentValue? schemaDocument)
            {
                schemaDocument = null;

                if(DocumentData.TryGetCurrent(out TransformDocumentValue? transformDocument))
                {
                     return SchemaDocumentData.TryGetSingle(
                         w => String.Equals(transformDocument.DataFileName, w.DataFileName, StringComparison.CurrentCultureIgnoreCase),
                         out schemaDocument);
                }
                else { return false; }
            }
        }
    }
}
