using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        class FormBinding : PresenterDatabase<TemplateIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<TransformValue> TransformData { get; }
            public DataBinding<TemplateObjectValue> ObjectData { get; }
            //public DataBinding<DocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource transformBinding,
                BindingSource objectBinding,
                BindingSource documentBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, () => GetData());
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                ObjectData = new DataBinding<TemplateObjectValue>(objectBinding, () => GetData().Objects);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(TemplateIndex key)
            {
                TemplateData.LoadBinding(w => key.Equals(w));
                SchemaData.LoadBinding(w => key.Equals(w));
                TransformData.LoadBinding(w => key.Equals(w));
                ObjectData.LoadBinding(w => key.Equals(w));
            }

            public Boolean RemoveValue()
            {
                if(TemplateData.TryGetValue(out TemplateValue? value))
                {
                    TemplateIndex key = new TemplateIndex(value);
                    ITemplateData target = GetData();

                    target.Remove(key);
                    return true;
                }
                else { return false; }
            }

            protected override IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TemplateIndex key)
            {
                List<WorkItem> work = new List<WorkItem>();
                ITemplateData target = BusinessData.Templates;

                work.AddRange(target.Load(factory, key));
                work.Add(new WorkItem() { DoWork = () => { GetData = () => target; } });
                return work;
            }

            protected override IReadOnlyList<WorkItem> TemporalWork(IDatabaseWork factory, TemplateIndex key, TemporalIndex temporal)
            {
                List<WorkItem> work = new List<WorkItem>();
                ITemplateData target = ITemplateData.Create();

                work.AddRange(target.Load(factory, key, temporal));
                work.Add(new WorkItem() { DoWork = () => { GetData = () => target; } });
                return work;
            }

            protected override IReadOnlyList<WorkItem> DeleteWork(TemplateIndex key)
            { return GetData().Delete(key); }

            protected override IReadOnlyList<WorkItem> SaveWork(IDatabaseWork factory, TemplateIndex key)
            { return GetData().Save(factory, key); }
        }
    }
}
