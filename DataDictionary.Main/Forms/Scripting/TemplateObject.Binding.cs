using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateObject
    {
        class FormBinding
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templateValues =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource ObjectBinding { private get; init; }
            BindingView<TemplateObjectValue> objectValues =
                new BindingView<TemplateObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding() : base()
            { }

            public void Load(ITemplateIndex template)
            {
                TemplateIndex key = new TemplateIndex(template);
                TemplateBinding.RaiseListChangedEvents = false;
                ObjectBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                objectValues.RaiseListChangedEvents = false;

                templateValues = new BindingView<TemplateValue>(GetData(), w => key.Equals(w));
                objectValues = new BindingView<TemplateObjectValue>(GetData().Objects, w => key.Equals(w));

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
                    objectValues.Add(value);
                    result = value; return true;
                }
                else { result = null; return false; }
            }

            public Boolean TryAddValue(INamedScopeValue item, [NotNullWhen(true)] out TemplateObjectValue? result)
            {
                if (TryGetValue(out TemplateValue? template))
                {
                    TemplateIndex key = new TemplateIndex(template);

                    if (objectValues.Any(w => key.Equals(w)
                        && item.Scope.Equals(w.ObjectScope)
                        && item.Path.Equals(w.ObjectPath)))
                    { result = null; return false; }
                    else
                    {
                        TemplateObjectValue newValue = new TemplateObjectValue(key)
                        { ObjectPath = item.Path, ObjectScope = item.Scope };
                        objectValues.Add(newValue);
                        result = newValue; return true;
                    }
                }
                else { result = null; return false; }
            }

            public IEnumerable<PathIndex> GetObjectPaths()
            { return objectValues.Select(s => s.ObjectPath); }
        }
    }
}
