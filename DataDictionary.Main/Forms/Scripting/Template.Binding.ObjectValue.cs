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
            /// Reference to the BindingSource holding the Template Object Values
            /// </summary>
            public required BindingSource ObjectBinding { private get; init; }


            /// <summary>
            /// Backing field for the Template Object Values.
            /// </summary>
            BindingView<TemplateObjectValue> objectValues =
                new BindingView<TemplateObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            /// <summary>
            /// How to get the Template Objects Values from the source Data.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>This is to allow child forms to get the data from the base form.</remarks>
            public BindingView<TemplateObjectValue> GetObjects(TemplateIndex key)
            { return new BindingView<TemplateObjectValue>(data.Objects, w => key.Equals(w)); }
        }

    }
}
