using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformManager : ApplicationData
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTransform.Current is ISchemaValue current && ReferenceEquals(current, item); }

        public TransformManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(transformToolStrip, 0);
        }

        public TransformManager(ITransformItem? transformItem): this()
        {
            if (transformItem is null)
            {
                transformItem = new TransformItem();
                BusinessData.ScriptingEngine.Transforms.Add(transformItem);
                BusinessData.NamedScope.Add(new NamedScopeItem(BusinessData.Model, transformItem));
            }

            TransformKey key = new TransformKey(transformItem);

            bindingTransform.DataSource = new BindingView<TransformItem>(BusinessData.ScriptingEngine.Transforms, w => key.Equals(w));
            bindingTransform.Position = 0;

            Setup(bindingTransform);
        }

        private void TransformManager_Load(object sender, EventArgs e)
        {
            SendMessage(new RefreshNavigation());
            ITransformItem transformNames;
            this.DataBindings.Add(new Binding(nameof(this.Text), bindingTransform, nameof(transformNames.TransformTitle)));

            transformTitleData.DataBindings.Add(new Binding(nameof(transformTitleData.Text), bindingTransform, nameof(transformNames.TransformTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            transformDescriptionData.DataBindings.Add(new Binding(nameof(transformDescriptionData.Text), bindingTransform, nameof(transformNames.TransformDescription), false, DataSourceUpdateMode.OnPropertyChanged));
            outputAsTextData.DataBindings.Add(new Binding(nameof(outputAsTextData.Checked), bindingTransform, nameof(transformNames.AsText), false, DataSourceUpdateMode.OnPropertyChanged));
            outputAsXmlData.DataBindings.Add(new Binding(nameof(outputAsXmlData.Checked), bindingTransform, nameof(transformNames.AsXml), false, DataSourceUpdateMode.OnPropertyChanged));
            transformScriptData.DataBindings.Add(new Binding(nameof(transformScriptData.Text), bindingTransform, nameof(transformNames.TransformScript), false, DataSourceUpdateMode.OnPropertyChanged));
            transformExceptionData.DataBindings.Add(new Binding(nameof(transformExceptionData.Text), bindingTransform, String.Format("{0}.{1}",nameof(transformNames.TransformException),nameof(transformNames.TransformException.Message)), false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void addTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeTransformCommand_Click(object sender, EventArgs e)
        {
            if (bindingTransform.Current is TransformItem item)
            {
                TransformKey key = new TransformKey(item);

                BusinessData.ScriptingEngine.Transforms.Remove(item);

                SendMessage(new RefreshNavigation());
            }
        }
    }
}
