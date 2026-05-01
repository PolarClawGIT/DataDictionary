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
            /// Reference to the BindingSource holding the Template Values
            /// </summary>
            public required BindingSource TemplateBinding { private get; init; }

            /// <summary>
            /// Backing field for the Template Values.
            /// </summary>
            BindingView<TemplateValue> templateValues =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            /// <summary>
            /// Get the Current TemplateValue, if it exists.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }
        }
    }
}
