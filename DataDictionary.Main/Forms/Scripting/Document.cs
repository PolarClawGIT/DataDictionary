using DataDictionary.BusinessLayer.Scripting;
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
    partial class Document : ApplicationData
    {
        class FormData : ISelectionIndex, ISchemaIndex, ITransformIndex, INotifyPropertyChanged
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
            private XDocument inputData = new XDocument();

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
        }

        BindingList<FormData> data = new BindingList<FormData>() { new FormData() };

        public Document()
        {
            InitializeComponent();
            bindingDocument.DataSource = data;
            bindingDocument.Position = 0;
            this.Icon = Resources.Icon_XmlFile;
        }


        private void Document_Load(object sender, EventArgs e)
        {
            FormData? nameOfValue;

            TransformNameMember.Load(transformData, BusinessData.ScriptingEngine.Transforms);
            transformData.DataBindings.Add(new Binding(nameof(transformData.SelectedValue), bindingDocument, nameof(nameOfValue.TransformId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            SchemaNameMember.Load(schemaData, BusinessData.ScriptingEngine.Schemta);
            schemaData.DataBindings.Add(new Binding(nameof(schemaData.SelectedValue), bindingDocument, nameof(nameOfValue.SchemaId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            SelectionNameMember.Load(selectionData, BusinessData.ScriptingEngine.Selections);
            selectionData.DataBindings.Add(new Binding(nameof(selectionData.SelectedValue), bindingDocument, nameof(nameOfValue.SelectionId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            inputData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.InputData), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
            inputExceptionData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.InputException), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
            outputData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.OutputData), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
            outputExecptionData.DataBindings.Add(new Binding(nameof(inputData.Text), bindingDocument, nameof(nameOfValue.OutputException), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
        }

        private void BuildCommand_Click(object sender, EventArgs e)
        {

        }
    }
}
