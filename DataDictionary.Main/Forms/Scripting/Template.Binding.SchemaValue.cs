using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{

    partial class Template
    {
        partial class FormBinding
        {
            /// <summary>
            /// Reference to the BindingSource holding the Template Schemas Values
            /// </summary>
            public required BindingSource SchemaBinding { private get; init; }

            /// <summary>
            /// Backing field for the Template Schema Values.
            /// </summary>
            BindingView<SchemaDefinitionValue> schemaValues =
                new BindingView<SchemaDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            /// <summary>
            /// How to get the Template Schema Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<SchemaDefinitionValue> GetSchemata(TemplateIndex key)
            { return new BindingView<SchemaDefinitionValue>(data.Schemata, w => key.Equals(w)); }

            /// <summary>
            /// How to get the Template Schema Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<SchemaDefinitionValue> GetSchemata(SchemaDefinitionIndex key)
            { return new BindingView<SchemaDefinitionValue>(data.Schemata, w => key.Equals(w)); }

            /// <summary>
            /// Get the Current SchemaDefinitionValue, if it exists.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetValue([NotNullWhen(true)] out SchemaDefinitionValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && SchemaBinding.Current is SchemaDefinitionValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            /// <summary>
            /// Adds the TemplateValue, if possible
            /// </summary>
            /// <param name="template"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryAddValue(ITemplateIndex template, [NotNullWhen(true)] out SchemaDefinitionValue? result)
            {
                if (BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner)
                {
                    SchemaDefinitionValue value = new SchemaDefinitionValue(template);
                    data.Add(value);
                    result = value; return true;
                }
                else { result = null; return false; }
            }
        }
    }
}
