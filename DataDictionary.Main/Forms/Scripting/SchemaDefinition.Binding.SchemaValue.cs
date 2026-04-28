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
        partial class FormBinding
        {
            public required BindingSource SchemaBinding { private get; init; }
            BindingView<SchemaDefinitionValue> schemaValues =
                new BindingView<SchemaDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public Boolean TryGetValue([NotNullWhen(true)] out SchemaDefinitionValue? result)
            {
                if (SchemaBinding.Position >= 0
                    && SchemaBinding.Current is SchemaDefinitionValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryAddValue(ITemplateIndex template, [NotNullWhen(true)] out SchemaDefinitionValue? result)
            {
                SchemaDefinitionValue value = new SchemaDefinitionValue(template);
                GetData().Schemata.Add(value);
                result = value; return true;
            }
        }
    }
}
