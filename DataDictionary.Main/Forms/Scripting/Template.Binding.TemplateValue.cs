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
            /// How to get the Template Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<TemplateValue> GetTemplates(TemplateIndex key)
            { return new BindingView<TemplateValue>(data, w => key.Equals(w)); }

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

            /// <summary>
            /// Adds the TemplateValue, if possible
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryAddValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner)
                {
                    TemplateValue value = new TemplateValue();
                    data.Add(value);
                    result = value; return true;
                }
                else { result = null; return false; }
            }
        }
    }
}
