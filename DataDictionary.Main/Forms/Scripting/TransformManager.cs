using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    [Obsolete("To be removed", true)]
    partial class TransformManager : ApplicationData
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTransform.Current is IDefinitionValue current && ReferenceEquals(current, item); }

        public TransformManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(transformToolStrip, 0);
        }

        public TransformManager(ITransformValue? transformItem) : this()
        {
            if (transformItem is null)
            {
                transformItem = new TransformValue();
                BusinessData.ScriptingEngine.Transforms.Add(transformItem);
            }

            TransformIndex key = new TransformIndex(transformItem);

            bindingTransform.DataSource = new BindingView<TransformValue>(BusinessData.ScriptingEngine.Transforms, w => key.Equals(w));
            bindingTransform.Position = 0;

            Setup(bindingTransform);
        }

        private void TransformManager_Load(object sender, EventArgs e)
        {
            SendMessage(new RefreshNavigation());
            ITransformValue transformNames;
            this.DataBindings.Add(new Binding(nameof(this.Text), bindingTransform, nameof(transformNames.TransformTitle)));

            transformTitleData.DataBindings.Add(new Binding(nameof(transformTitleData.Text), bindingTransform, nameof(transformNames.TransformTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            transformDescriptionData.DataBindings.Add(new Binding(nameof(transformDescriptionData.Text), bindingTransform, nameof(transformNames.TransformDescription), false, DataSourceUpdateMode.OnPropertyChanged));
            //outputAsTextData.DataBindings.Add(new Binding(nameof(outputAsTextData.Checked), bindingTransform, nameof(transformNames.AsText), false, DataSourceUpdateMode.OnPropertyChanged));
            //outputAsXmlData.DataBindings.Add(new Binding(nameof(outputAsXmlData.Checked), bindingTransform, nameof(transformNames.AsXml), false, DataSourceUpdateMode.OnPropertyChanged));
            transformScriptData.DataBindings.Add(new Binding(nameof(transformScriptData.Text), bindingTransform, nameof(transformNames.TransformScript), false, DataSourceUpdateMode.OnPropertyChanged));
            transformExceptionData.DataBindings.Add(new Binding(nameof(transformExceptionData.Text), bindingTransform, String.Format("{0}.{1}", nameof(transformNames.TransformException), nameof(transformNames.TransformException.Message)), false, DataSourceUpdateMode.OnPropertyChanged));

            transformScriptAsXml.DataBindings.Add(new Binding(nameof(transformScriptAsXml.Checked), bindingTransform, nameof(transformNames.AsXml), false, DataSourceUpdateMode.OnPropertyChanged));
            transformScriptAsText.DataBindings.Add(new Binding(nameof(transformScriptAsText.Checked), bindingTransform, nameof(transformNames.AsText), false, DataSourceUpdateMode.OnPropertyChanged));

            SetOutputButtonColor();
        }

        private void addTransformCommand_Click(object sender, EventArgs e)
        { }


        private void removeTransformCommand_Click(object sender, EventArgs e)
        {
            if (bindingTransform.Current is TransformValue item)
            {
                TransformIndex key = new TransformIndex(item);

                BusinessData.ScriptingEngine.Transforms.Remove(item);

                SendMessage(new RefreshNavigation());
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        { base.SaveToDatabaseCommand_Click(sender, e); }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        { base.OpenFromDatabaseCommand_Click(sender, e); }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        { base.DeleteFromDatabaseCommand_Click(sender, e); }


        private void FormatScriptCommand_Click(object sender, EventArgs e)
        {
            if (bindingTransform.Current is TransformValue item)
            {
                if (item.TransformDocument is XDocument value)
                { transformScriptData.Text = value.ToString(); }
            }
        }

        private void TransformScriptOutputAs_Click(object sender, EventArgs e)
        { SetOutputButtonColor(); }

        private void SetOutputButtonColor()
        {
            // Using the Back Color does not work. Back Color is an "ambient" which calculated.
            transformScriptAsXml.BackgroundImage = null;
            transformScriptAsText.BackgroundImage = null;

            if (transformScriptAsText.Checked && transformScriptAsText.Enabled)
            { transformScriptAsText.BackgroundImage = Resources.ButtonGreen; }

            if (transformScriptAsXml.Checked && transformScriptAsXml.Enabled)
            { transformScriptAsXml.BackgroundImage = Resources.ButtonGreen; }
        }
    }
}
