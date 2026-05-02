using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Transform
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
            /// Get the Current TransformValue, if it exists.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetValue([NotNullWhen(true)] out TransformValue? result)
            {
                if (TransformBinding.Position >= 0
                    && TransformBinding.Current is TransformValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryAddValue(ITemplateIndex template, [NotNullWhen(true)] out TransformValue? result)
            {
                TransformValue value = new TransformValue(template);
                GetData().Transforms.Add(value);
                result = value; return true;
            }

        }
    }
}
