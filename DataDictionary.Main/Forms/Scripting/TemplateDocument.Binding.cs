using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateDocument
    {
        // Allows the DataBinding.TryGetCurrent function to be passed in.
        delegate Boolean TryGetCurrent<TOut>([NotNullWhen(true)] out TOut? outputValue);

        /// <summary>
        /// Class contains supplemental binding values.
        /// </summary>
        class OtherBinding : IBindingPropertyChanged, IBindingRowState
        {   // This is a wrapper class to create additional properties that don't exist in the business layer.
            // They also need to be "simplified" so Data Binding would work.
            // 
            // To get this to work, OnPropertyChanged needs to be called from outside.
            // DataBinding then sees the change and updates the field.
            // 
            // Could not bind using IDirectoryValue.
            // FindGoodRow throws exceptions during OnPropertyChanged.
            // It is suspected the use of an interface.
            // Other possibility is that OnPropertyChanged passed with the complex types
            // while the actual binding is a child of the complex type.
            // 
            // TODO: Would prefer to implement this in the Business Layer but don't know how it would work.

            public required TryGetCurrent<TemplateValue> GetTemplate { private get; init; }
            public required TryGetCurrent<SchemaDefinitionValue> GetSchema { private get; init; }
            public required TryGetCurrent<TransformValue> GetTransform { private get; init; }
            public required TryGetCurrent<TemplateDocumentValue> GetDocument { private get; init; }

            public String SchemaDirectory
            {
                get
                {
                    if (TryGetSchemaDirectory(out IDirectoryValue? directory))
                    { return directory.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public Boolean TryGetSchemaDirectory([NotNullWhen(true)] out IDirectoryValue? directory)
            {
                directory = null;

                if (GetDocument(out TemplateDocumentValue? document))
                {
                    if (new SchemaDefinitionIndex(document).HasValue
                        && GetSchema(out SchemaDefinitionValue? schema))
                    { directory = schema; }
                    else if (GetTemplate(out TemplateValue? template))
                    { directory = template; }
                }

                return directory is not null;
            }

            public String TransformDirectory
            {
                get
                {
                    if (TryGetTransformDirectory(out IDirectoryValue? directory))
                    { return directory.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public Boolean TryGetTransformDirectory([NotNullWhen(true)] out IDirectoryValue? directory)
            {
                directory = null;

                if (GetDocument(out TemplateDocumentValue? document))
                {
                    if (new TemplateIndex(document).HasValue
                        && GetTemplate(out TemplateValue? template))
                    { directory = template; }
                }

                return directory is not null;
            }

            public String ScriptedDirectory
            {
                get
                {
                    if (TryGetScriptedDirectory(out IDirectoryValue? directory))
                    { return directory.InitialDirectory; }
                    else { return String.Empty; }
                }
            }

            public Boolean TryGetScriptedDirectory([NotNullWhen(true)] out IDirectoryValue? directory)
            {
                directory = null;

                if (GetDocument(out TemplateDocumentValue? document))
                {
                    if (new TransformIndex(document).HasValue
                        && GetTransform(out TransformValue? transform))
                    { directory = transform; }
                    else if (GetTemplate(out TemplateValue? template))
                    { directory = template; }
                }

                return directory is not null;
            }


            public Boolean IsObjectInModel
            {
                get
                {
                    if (GetDocument(out TemplateDocumentValue? document))
                    { return BusinessData.Model.IsInModel(document); }
                    else { return false; }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            /// <inheritdoc cref="BindingPropertyChanged.OnPropertyChanged"/>
            public virtual void OnPropertyChanged(String propertyName)
            { this.OnPropertyChanged(PropertyChanged, propertyName); }

            // Needed for DataBinding class, but not actually used.
            public event EventHandler<RowStateEventArgs>? RowStateChanged;
            public DataRowState RowState()
            { throw new InvalidOperationException(); }
        }

        class FormBinding : PresenterData<DocumentIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;
            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<TransformValue> TransformData { get; }

            public DataBinding<TemplateDocumentValue> DocumentData { get; }

            /// <summary>
            /// This is additional data elements for Data Binding.
            /// </summary>
            /// <remarks>
            /// It is setup to fake an actual data set, even with only one data item in it.
            /// This could be done other approaches but it is being done this way to leverage existing code.
            /// </remarks>
            public DataBinding<OtherBinding> OtherData { get; }
            OtherBinding otherValue; // Internal reference so I can deal with the values directly

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource transformBinding,
                BindingSource documentBinding,
                BindingSource otherBinding) : base()
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                TransformData = new DataBinding<TransformValue>(transformBinding, () => GetData().Transforms);
                DocumentData = new DataBinding<TemplateDocumentValue>(documentBinding, () => GetData().Documents);

                otherValue = new OtherBinding()
                {
                    GetTemplate = TemplateData.TryGetCurrent,
                    GetSchema = SchemaData.TryGetCurrent,
                    GetTransform = TransformData.TryGetCurrent,
                    GetDocument = DocumentData.TryGetCurrent
                };

                IBindingList<OtherBinding> otherValues = new BindingView<OtherBinding>(new BindingList<OtherBinding>()) { otherValue };
                OtherData = new DataBinding<OtherBinding>(otherBinding, () => { return otherValues; });

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
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
                    OtherData.LoadBinding(w => true);
                }

                // OtherData needs to be informed when it needs to recompute what the values are. 
                // Set that up here.
                DocumentData.ListChanged += DocumentData_ListChanged;
                TemplateData.ListChanged += TemplateData_ListChanged;
                SchemaData.ListChanged += SchemaData_ListChanged;
                TransformData.ListChanged += TransformData_ListChanged;

                void DocumentData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property)
                    {
                        if (property.Name == nameof(TemplateDocumentValue.SchemaId))
                        { otherValue.OnPropertyChanged(nameof(otherValue.SchemaDirectory)); }

                        if (property.Name == nameof(TemplateDocumentValue.TransformId))
                        { otherValue.OnPropertyChanged(nameof(otherValue.ScriptedDirectory)); }

                        if (property.Name == nameof(TemplateDocumentValue.ObjectPath))
                        { otherValue.OnPropertyChanged(nameof(otherValue.IsObjectInModel)); }
                    }
                }

                void TemplateData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(TemplateValue.InitialDirectory))
                    {
                        otherValue.OnPropertyChanged(nameof(otherValue.SchemaDirectory));
                        otherValue.OnPropertyChanged(nameof(otherValue.TransformDirectory));
                        otherValue.OnPropertyChanged(nameof(otherValue.ScriptedDirectory));
                    }
                }

                void SchemaData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(SchemaDefinitionValue.InitialDirectory))
                    {
                        otherValue.OnPropertyChanged(nameof(otherValue.SchemaDirectory));
                    }
                }

                void TransformData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.PropertyDescriptor is PropertyDescriptor property && property.Name == nameof(TransformValue.InitialDirectory))
                    {
                        otherValue.OnPropertyChanged(nameof(otherValue.TransformDirectory));
                        otherValue.OnPropertyChanged(nameof(otherValue.ScriptedDirectory));
                    }
                }
            }

            public Boolean TryCreate(TemplateIndex template, ref DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (GetData().TryCreate(template, out TemplateDocumentValue? value, out exception))
                { document = new DocumentIndex(value); }

                return exception is null;
            }

            // TODO: Add other functions that are dependent on if a value is selected.
            // DO NOT trust TryGetCurrent to return a correct value when nothing is selected.
            // Must check the current document value has a Key before executing function.
        }
    }
}
