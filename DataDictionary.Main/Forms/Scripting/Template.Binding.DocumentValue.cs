using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        partial class FormBinding
        {
            /// <summary>
            /// Reference to the BindingSource holding the Template Documents Values
            /// </summary>
            public required BindingSource DocumentBinding { private get; init; }


            /// <summary>
            /// Backing field for the Template Documents Values.
            /// </summary>
            BindingList<DocumentValue> documentValues = new BindingList<DocumentValue>();



            /// <summary>
            /// How to get the Template Schema Document Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<SchemaDocumentValue> GetSchemaDocuments(TemplateIndex key)
            { return new BindingView<SchemaDocumentValue>(data.SchemaDocuments, w => key.Equals(w)); }

            public BindingView<SchemaDocumentValue> GetSchemaDocuments(SchemaDefinitionIndex key)
            { return new BindingView<SchemaDocumentValue>(data.SchemaDocuments, w => key.Equals(w)); }

            /// <summary>
            /// How to get the Template Transform Document Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<TransformDocumentValue> GetTransformDocuments(TemplateIndex key)
            { return new BindingView<TransformDocumentValue>(data.TransformDocuments, w => key.Equals(w)); }

            public BindingView<TransformDocumentValue> GetTransformDocuments(TransformIndex key)
            { return new BindingView<TransformDocumentValue>(data.TransformDocuments, w => key.Equals(w)); }

            /// <summary>
            /// How to get the Template Documents Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingList<DocumentValue> GetDocuments(TemplateIndex key)
            {
                DocumentCompare compare = new DocumentCompare();
                BindingList<DocumentValue> values = new BindingList<DocumentValue>();
                values.AddRange(
                    data.SchemaDocuments.Select(s => new DocumentValue(s)).
                    Union(data.TransformDocuments.Select(s => new DocumentValue(s)), compare));

                return values;
            }
        }
    }
}
