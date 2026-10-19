using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
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
            public DataBinding<TransformDocumentValue> DocumentData { get; }
            public IBindingList<SchemaDefinitionValue> SchemaData { get; private set; }

            //public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource transformBinding,
                BindingSource documentBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TransformDocumentValue>(documentBinding, () => GetData().TransformDocuments);
                SchemaData = new BindingView<SchemaDefinitionValue>(GetData().Schemata, w => false); ;
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(TransformIndex key)
            {
                TransformData.LoadBinding(w => key.Equals(w));
                if (TransformData.TryGetCurrent(out TransformValue? transformValue))
                {
                    TemplateIndex templateKey = new TemplateIndex(transformValue);
                    TemplateData.LoadBinding(w => templateKey.Equals(w));
                    DocumentData.LoadBinding(w => templateKey.Equals(w));

                    SchemaData = new BindingView<SchemaDefinitionValue>(GetData().Schemata, w => templateKey.Equals(w));
                }
            }

            public Boolean TryCreate(ITemplateIndex templateIndex, ref TransformIndex transform, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(templateIndex, out TransformValue? value, out exception))
                { transform = new TransformIndex(value); }

                return exception is null;
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
