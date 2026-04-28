using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        partial class FormBinding
        {
            public required BindingSource ObjectBinding { private get; init; }
            BindingView<TemplateObjectValue> objectValues =
                new BindingView<TemplateObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };
        }
    }
}
