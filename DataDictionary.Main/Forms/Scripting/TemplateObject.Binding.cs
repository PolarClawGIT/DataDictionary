using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System.Data;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateObject
    {
        class FormBinding : PresenterData<TemplateObjectIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<TemplateObjectValue> ObjectData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource objectBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                ObjectData = new DataBinding<TemplateObjectValue>(objectBinding, () => GetData().Objects);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public void LoadValue(TemplateIndex key)
            {
                TemplateData.LoadBinding(w => key.Equals(w));
                ObjectData.LoadBinding(w => key.Equals(w));
            }

            public override void LoadValue(TemplateObjectIndex key)
            { throw new NotSupportedException(); }

            public void AddValues(TemplateIndex key, IEnumerable<INamedScopeValue> values)
            {
                foreach (INamedScopeValue item in values)
                {
                    TemplateObjectValue newItem = new TemplateObjectValue(key);
                    newItem.ObjectScope = item.Scope;
                    newItem.ObjectPath = item.Path;
                    TemplateObjectNameIndex objectKey = new TemplateObjectNameIndex(newItem);

                    if (ObjectData.All(w => !objectKey.Equals(w)))
                    { ObjectData.Add(newItem); }
                }
            }

            public Boolean RemoveValue()
            { return ObjectData.Remove(); }

            public IEnumerable<PathIndex> GetObjectPaths()
            { return ObjectData.GetData().Select(s => s.ObjectPath); }
        }
    }
}
