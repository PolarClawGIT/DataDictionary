using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataDictionary.Main.Forms.Scripting
{
    [Obsolete("To be removed", true)]
    partial class Document : ApplicationData
    {
        class FormData : ISelectionIndex, IDefinitionIndex, ITransformIndex, INotifyPropertyChanged
        {
            public Guid? TransformId
            {
                get { return transformId; }
                set { transformId = value; OnPropertyChanged(nameof(TransformId)); }
            }
            private Guid? transformId;

            public Guid? SchemaId
            {
                get { return schemaId; }
                set { schemaId = value; OnPropertyChanged(nameof(SchemaId)); }
            }
            private Guid? schemaId;

            public Guid? SelectionId
            {
                get { return selectionId; }
                set { selectionId = value; OnPropertyChanged(nameof(SelectionId)); }
            }
            private Guid? selectionId;


            public event PropertyChangedEventHandler? PropertyChanged;
            void OnPropertyChanged(String propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }

            public XDocument InputData
            {
                get { return inputData; }
                set { inputData = value; OnPropertyChanged(nameof(InputData)); }
            }
            private XDocument inputData = new XDocument(new XElement(ScopeType.Model.ToName()));

            public String InputException
            {
                get { return inputException; }
                set { inputException = value; OnPropertyChanged(nameof(InputException)); }
            }
            private String inputException = String.Empty;

            public String OutputData
            {
                get { return outputData; }
                set { outputData = value; OnPropertyChanged(nameof(OutputData)); }
            }
            private String outputData = String.Empty;

            public String OutputException
            {
                get { return outputException; }
                set { outputException = value; OnPropertyChanged(nameof(OutputException)); }
            }
            private String outputException = String.Empty;

            public FormData() : base()
            { inputData.Changed += InputData_Changed; }

            private void InputData_Changed(Object? sender, XObjectChangeEventArgs e)
            { OnPropertyChanged(nameof(InputData)); }
        }

        BindingList<FormData> data = new BindingList<FormData>() { new FormData() };

        public Document()
        {
            InitializeComponent();
            bindingDocument.DataSource = data;
            bindingDocument.Position = 0;
            this.Icon = Resources.Icon_XmlFile;
            toolStrip.TransferItems(documentCommands, 0);
        }


        private void Document_Load(object sender, EventArgs e)
        {
            FormData? nameOfValue;

            TransformNameMember.Load(transformData, BusinessData.ScriptingEngine.Transforms);
            transformData.DataBindings.Add(new Binding(nameof(transformData.SelectedValue), bindingDocument, nameof(nameOfValue.TransformId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            DefinitionNameMember.Load(schemaData, BusinessData.ScriptingEngine.Schemta);
            schemaData.DataBindings.Add(new Binding(nameof(schemaData.SelectedValue), bindingDocument, nameof(nameOfValue.SchemaId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            SelectionNameMember.Load(selectionData, BusinessData.ScriptingEngine.Selections);
            selectionData.DataBindings.Add(new Binding(nameof(selectionData.SelectedValue), bindingDocument, nameof(nameOfValue.SelectionId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            inputData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.InputData), true, DataSourceUpdateMode.OnPropertyChanged));
            inputExceptionData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.InputException), true, DataSourceUpdateMode.OnPropertyChanged));
            outputData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.OutputData), true, DataSourceUpdateMode.OnPropertyChanged));
            outputExecptionData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.OutputException), true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void BuildCommand_Click(object sender, EventArgs e)
        {
            if (bindingDocument.Current is FormData data && data.InputData.Root is not null)
            {
                XElement root = data.InputData.Root;
                root.RemoveAll();

                SelectionIndex selectionIndex = new SelectionIndex(data);
                IEnumerable<SelectionPathValue> selections = BusinessData.ScriptingEngine.SelectionPaths.Where(w => selectionIndex.Equals(w));

                foreach (SelectionPathValue selected in selections)
                {
                    foreach (NamedScopeIndex index in BusinessData.NamedScope.PathKeys(selected.GetPath()))
                    {
                        INamedScopeSourceValue source = BusinessData.NamedScope.GetData(index);

                        if (source is IScripting scripting)
                        {
                            DefinitionIndex schemaIndex = new DefinitionIndex(data);
                            IEnumerable<DefinitionElementValue> schemaElement = BusinessData.ScriptingEngine.SchemeElements.Where(w => schemaIndex.Equals(w));
                            XElement item = scripting.GetXElement(BusinessData, schemaElement);
                            root.Add(item);
                        }
                    }
                }

                TransformIndex transformIndex = new TransformIndex(data);
                if (BusinessData.ScriptingEngine.Transforms.FirstOrDefault(w => transformIndex.Equals(w)) is TransformValue transform)
                {
                    if (transform.AsXml)
                    {
                        (XDocument result, Exception? exception) transformXml = transform.TransformToXml(data.InputData);
                        data.OutputData = transformXml.result.ToString();

                        if (transformXml.exception is not null)
                        { data.OutputException = transformXml.exception.Message; }
                        else { data.OutputException = String.Empty; }
                    }
                    else
                    {
                        (String result, Exception? exception) transformText = transform.TransformToText(data.InputData);

                        data.OutputData = transformText.result;

                        if (transformText.exception is not null)
                        { data.OutputException = transformText.exception.Message; }
                        else { data.OutputException = String.Empty; }
                    }
                }

            }
        }
    }
}
