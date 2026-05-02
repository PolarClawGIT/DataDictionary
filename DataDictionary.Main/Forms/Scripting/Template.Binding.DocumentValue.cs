using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        partial class FormBinding
        {
            /// <summary>
            /// Reference to the BindingSource holding the Template Documents Values
            /// </summary>
            public required BindingSource DocumentBinding { private get; init; }

            /// <summary>
            /// Backing field for the Template Documents Values.
            /// </summary>
            BindingList<DocumentValue> documentValues = new BindingList<DocumentValue>();
        }
    }
}
