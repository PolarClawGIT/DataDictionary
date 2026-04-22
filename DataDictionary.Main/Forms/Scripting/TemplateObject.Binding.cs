using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateObject
    {
        class FormBinding
        {
            ITemplateData data = BusinessData.Templates; // Default data location

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            public Func<TemplateIndex, BindingView<TemplateValue>> GetTemplates { get; set; }
            BindingView<TemplateValue> templateValues =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource ObjectBinding { private get; init; }
            public Func<TemplateIndex, BindingView<TemplateObjectValue>> GetObjects { get; set; }
            BindingView<TemplateObjectValue> objectValues =
                new BindingView<TemplateObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding()
            {
                GetTemplates = (key) => { return new BindingView<TemplateValue>(data, w => new TemplateIndex(key).Equals(w)); };
                GetObjects = (key) => { return new BindingView<TemplateObjectValue>(data.Objects, w => new TemplateIndex(key).Equals(w)); };
            }

            public void Load(ITemplateIndex template)
            {
                TemplateIndex key = new TemplateIndex(template);
                TemplateBinding.RaiseListChangedEvents = false;
                ObjectBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                objectValues.RaiseListChangedEvents = false;

                templateValues = GetTemplates(key);
                objectValues = GetObjects(key);

                if (templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;
                    ObjectBinding.DataSource = objectValues;

                    TemplateBinding.RaiseListChangedEvents = true;
                    ObjectBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                    objectValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                ObjectBinding.ResetBindings(false);
                TemplateBinding.MoveFirst();
            }

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateObjectValue? result)
            {
                if (ObjectBinding.Position >= 0
                    && ObjectBinding.Current is TemplateObjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryAddValue([NotNullWhen(true)] out TemplateObjectValue? result)
            {
                if (TryGetValue(out TemplateValue? template))
                {
                    TemplateObjectValue value = new TemplateObjectValue(template);
                    data.Objects.Add(value);
                    result = value; return true;
                }
                else { result = null; return false; }
            }
        }
    }
}
