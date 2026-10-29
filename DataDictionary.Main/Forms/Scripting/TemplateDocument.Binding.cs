using DataDictionary.BusinessLayer.AppScripting;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateDocument
    {
        class FormBinding : PresenterData<DocumentIndex>, INotifyPropertyChanged
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<TransformValue> TransformData { get; }
            public DataBinding<TemplateDocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource transformBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TemplateDocumentValue>(documentBinding, () => GetData().Documents);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }


            public String SchemaDirectory
            {
                get
                {
                    if (SchemaData.TryGetCurrent(out SchemaDefinitionValue? schema))
                    { return schema.InitialDirectory; }
                    else if (TemplateData.TryGetCurrent(out TemplateValue? template))
                    { return template.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public String TransformDirectory
            {
                get
                {
                    if (TemplateData.TryGetCurrent(out TemplateValue? template))
                    { return template.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public String ScriptedDirectory
            {
                get
                {
                    if (TransformData.TryGetCurrent(out TransformValue? transform))
                    { return transform.InitialDirectory; }
                    else if (TemplateData.TryGetCurrent(out TemplateValue? template))
                    { return template.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public override void LoadValue(DocumentIndex key)
            {
                DocumentData.LoadBinding(w => key.Equals(w));

                if (DocumentData.TryGetSingle(out TemplateDocumentValue? document))
                {
                    TemplateIndex templateKey = new TemplateIndex(document);

                    TemplateData.LoadBinding(w => templateKey.Equals(w));
                    SchemaData.LoadBinding(w => templateKey.Equals(w));
                    TransformData.LoadBinding(w => templateKey.Equals(w));
                }

                DocumentData.ListChanged += DocumentData_ListChanged;
                TemplateData.ListChanged += TemplateData_ListChanged;
                SchemaData.ListChanged += SchemaData_ListChanged;
                TransformData.ListChanged += TransformData_ListChanged;

                void DocumentData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property)
                    {
                        if (property.Name == nameof(TemplateDocumentValue.SchemaId))
                        { OnPropertyChanged(nameof(SchemaDirectory)); }

                        if (property.Name == nameof(TemplateDocumentValue.TransformId))
                        { OnPropertyChanged(nameof(ScriptedDirectory)); }
                    }
                }

                void TemplateData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(TemplateValue.InitialDirectory))
                    {
                        OnPropertyChanged(nameof(SchemaDirectory));
                        OnPropertyChanged(nameof(TransformDirectory));
                        OnPropertyChanged(nameof(ScriptedDirectory));
                    }
                }

                void SchemaData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(SchemaDefinitionValue.InitialDirectory))
                    { OnPropertyChanged(nameof(SchemaDirectory)); }
                }

                void TransformData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(TransformValue.InitialDirectory))
                    {
                        OnPropertyChanged(nameof(TransformDirectory));
                        OnPropertyChanged(nameof(ScriptedDirectory));
                    }
                }

            }



            public Boolean TryCreate(TemplateIndex template, ref DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(template, out TemplateDocumentValue? value, out exception))
                { document = new DocumentIndex(value); }

                return exception is null;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged(String propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }
    }
}
