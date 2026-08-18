using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDocument
    {
        class FormBinding : PresenterData<DocumentIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<SchemaDocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                DocumentData = new DataBinding<SchemaDocumentValue>(documentBinding, () => GetData().SchemaDocuments);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(DocumentIndex key)
            {
                DocumentData.LoadBinding(w => key.Equals(w));

                if (DocumentData.TryGetSingle(out SchemaDocumentValue? documentValue))
                {
                    TemplateIndex templateIndex = new TemplateIndex(documentValue);
                    SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex(documentValue);
                    TemplateData.LoadBinding(w => templateIndex.Equals(w));
                    SchemaData.LoadBinding(w => schemaIndex.Equals(w));
                }
                else
                {
                    Exception ex = new ArgumentException("Could not locate value");
                    ex.Data.Add(nameof(SchemaDocumentValue), key);
                    throw ex;
                }
            }

            /// <summary>
            /// Creates a SchemaDefinitionValue then loads it.
            /// </summary>
            /// <param name="schema"></param>
            /// <param name="key"></param>
            /// <exception cref="NotImplementedException"></exception>
            public void LoadValue(SchemaDefinitionIndex schema, out DocumentIndex key)
            {
                SchemaData.LoadBinding(w => schema.Equals(w));

                if(SchemaData.TryGetSingle(out SchemaDefinitionValue? definition))
                {
                    TemplateIndex templateIndex = new TemplateIndex(definition);
                    SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex(definition);
                    SchemaDocumentValue document = new SchemaDocumentValue(templateIndex, schemaIndex);
                    DocumentIndex documentIndex = new DocumentIndex(document);

                    GetData().SchemaDocuments.Add(document);
                    TemplateData.LoadBinding(w => templateIndex.Equals(w));
                    DocumentData.LoadBinding(w => documentIndex.Equals(w));

                    key = documentIndex;
                }
                else
                {
                    Exception ex = new ArgumentException("Could not locate value or duplicate exists");
                    ex.Data.Add(nameof(SchemaDefinitionValue), schema);
                    throw ex;
                }
            }

            public Boolean TryGetFile([NotNullWhen(true)] out IDirectoryValue? directory, [NotNullWhen(true)] out IFileValue? file)
            {
                directory = null;
                file = null;

                if(SchemaData.TryGetValue(out SchemaDefinitionValue? schema)
                    && DocumentData.TryGetValue(out SchemaDocumentValue? document)) 
                { directory = schema.SchemaDirectory; file = document.SchemaFile; return true; }
                else { return false; }
            }
        }
    }
}
