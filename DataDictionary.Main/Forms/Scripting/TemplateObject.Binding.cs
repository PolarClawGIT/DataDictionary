using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using System.Data;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateObject
    {
        class FormBinding : DataModel<TemplateIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            //public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<TemplateObjectValue> ObjectData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource objectBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                ObjectData = new DataBinding<TemplateObjectValue>(objectBinding, () => GetData().Objects);
            }

            public override void Load(TemplateIndex key)
            {
                TemplateData.LoadBinding(w => key.Equals(w));
                ObjectData.LoadBinding(w => key.Equals(w));
            }

            public IEnumerable<PathIndex> GetObjectPaths()
            { return ObjectData.GetData().Select(s => s.ObjectPath); }

            public override Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TemplateData.TryGetValue(out TemplateValue? _);

                SecurableIndex? templateKey = null;
                if (TemplateData.TryGetValue(out TemplateValue? templateValue))
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

            public override Boolean GetLocked()
            {
                if (TemplateData.TryGetValue(out TemplateValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }
        }
    }
}
