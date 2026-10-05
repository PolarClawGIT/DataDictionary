using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

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
                TemplateIndex templateKey = new TemplateIndex();

                TransformData.LoadBinding(w => key.Equals(w));
                if (TransformData.TryGetCurrent(out TransformValue? transformValue))
                { templateKey = new TemplateIndex(transformValue); }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
            }

            public Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out TransformValue? transform)
            {
                TemplateIndex templateKey = new TemplateIndex(template);
                transform = null;

                if (TemplateData.TryGetSingle(w => templateKey.Equals(w), out TemplateValue? templateValue))
                { transform = new TransformValue(templateValue); return true; }
                else { return false; }
            }

            public Boolean TryGetFile([NotNullWhen(true)] out IDirectoryValue? directory, [NotNullWhen(true)] out IFileValue? file)
            {
                directory = null;
                file = null;

                if (TransformData.TryGetCurrent(out TransformValue? transform))
                { directory = transform; file = transform; return true; }
                else { return false; }
            }
        }
    }
}
