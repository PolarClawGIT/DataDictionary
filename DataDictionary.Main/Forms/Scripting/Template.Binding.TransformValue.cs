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
            /// <summary>
            /// Reference to the BindingSource holding the Template Transforms Values
            /// </summary>
            public required BindingSource TransformBinding { private get; init; }

            /// <summary>
            /// Backing field for the Template Transform Values.
            /// </summary>
            BindingView<TransformValue> transformValues =
                new BindingView<TransformValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

        }
    }
}
