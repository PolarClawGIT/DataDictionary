using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateDocument
    {
        class FormBinding : PresenterData<DocumentIndex>
        {
            public override void LoadValue(DocumentIndex key)
            {
                throw new NotImplementedException();
            }

            public Boolean TryCreate(TemplateIndex templateIndex, ref DocumentIndex documentIndex, out Exception? exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
