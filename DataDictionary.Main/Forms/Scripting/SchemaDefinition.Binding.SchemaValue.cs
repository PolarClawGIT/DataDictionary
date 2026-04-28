using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        public delegate BindingView<SchemaDefinitionValue> GetSchemaCallBack(SchemaDefinitionIndex schema);
        public delegate Boolean TryAddSchemaCallback(ITemplateIndex template, [NotNullWhen(true)] out SchemaDefinitionValue? result);

        partial class FormBinding
        {
            public required BindingSource SchemaBinding { private get; init; }
            public GetSchemaCallBack GetSchemata { get; set; }
            BindingView<SchemaDefinitionValue> schemaValues =
                new BindingView<SchemaDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };
            public TryAddSchemaCallback TryAddSchema { get; set; }
        }
    }
}
