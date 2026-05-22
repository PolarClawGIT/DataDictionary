using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Transform
    {
        partial class FormBinding : PresenterData<TransformIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<TransformValue> TransformData { get; }

            //public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource transformBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(TransformIndex key)
            {
                TemplateIndex templateKey;

                TransformData.LoadBinding(w => key.Equals(w));
                if (TransformData.TryGetValue(out TransformValue? transformValue))
                { templateKey = new TemplateIndex(transformValue); }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(key), key);
                    throw ex;
                }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
            }


            public override void RemoveValue(TransformIndex key)
            {
                GetData().Transforms.Remove(key);
                GetData().TransformDocuments.Remove(key);
            }
        }
    }
}
