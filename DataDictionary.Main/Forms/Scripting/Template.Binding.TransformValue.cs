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

            /// <summary>
            /// How to get the Template Transforms Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<TransformValue> GetTransforms(TemplateIndex key)
            { return new BindingView<TransformValue>(data.Transforms, w => key.Equals(w)); }

            public BindingView<TransformValue> GetTransforms(TransformIndex key)
            { return new BindingView<TransformValue>(data.Transforms, w => key.Equals(w)); }
        }
    }
}
