using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        partial class FormBinding
        {
            public BindingView<SchemaNodeValue> GetSchemaNodes(SchemaDefinitionIndex key)
            { return new BindingView<SchemaNodeValue>(data.SchemataNodes, w => key.Equals(w)); }
        }
    }
}
