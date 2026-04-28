using DataDictionary.BusinessLayer.AppScripting;
using System.Diagnostics.CodeAnalysis;
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
        }
    }
}
