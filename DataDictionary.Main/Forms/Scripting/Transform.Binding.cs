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
        partial class FormBinding
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding() : base()
            { }

            public void Load(ITransformIndex transform)
            {
                TemplateIndex templateKey;
                TransformIndex transformKey = new TransformIndex(transform);

                TemplateBinding.RaiseListChangedEvents = false;
                TransformBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                transformValues.RaiseListChangedEvents = false;

                transformValues = new BindingView<TransformValue>(GetData().Transforms, w => transformKey.Equals(w));
                if (transformValues.FirstOrDefault() is TransformValue value)
                { templateKey = new TemplateIndex(value); }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(transform), transform);
                    throw ex;
                }

                templateValues = new BindingView<TemplateValue>(GetData(), w => templateKey.Equals(w));
                

                if (templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;
                    TransformBinding.DataSource = transformValues;

                    TemplateBinding.RaiseListChangedEvents = true;
                    TransformBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                    templateValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                TransformBinding.ResetBindings(false);
                TransformBinding.MoveFirst();
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TryGetValue(out TemplateValue? _);

                SecurableIndex? templateKey = null;
                if (TryGetValue(out TemplateValue? templateValue))
                { templateKey = new TemplateIndex(templateValue); }

                isGrant = BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner
                    || BusinessData.Authorization.IsGrant(templateKey);

                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return isGrant;
                    case Enumerations.ButtonType.Delete: return isGrant && isNode;
                    case Enumerations.ButtonType.OpenDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.SaveDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.DeleteDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.HistoryDatabase: return isGrant && isNode;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out TemplateValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }
        }
    }
}
