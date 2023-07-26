using DataDictionary.DataLayer.ApplicationData;
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
using Toolbox.DbContext;

namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveDbDialog : Form
    {
        public enum OpenSaveOption
        {
            none,
            Open,
            Save
        }

        class FormData
        {
            public String? ModelTitle { get; set; }
            public String? ModelDescription { get; set; }
            public FormData()
            {
                ModelTitle = Program.Data.Model.ModelTitle;
                ModelDescription = Program.Data.Model.ModelDescription;
            }

        }

        FormData data = new FormData();

        Dictionary<OpenSaveOption, (String header, String buttonText, Icon icon)> OpenSaveConfigurations = new Dictionary<OpenSaveOption, (String header, String buttonText, Icon icon)>()
        {
            {OpenSaveOption.Open, ("Open from database", "Open", Resources.DbOpenSave) }, // TODO: Make different Open, Save, New icons
            {OpenSaveOption.Save, ("Save to database", "Save", Resources.DbOpenSave) },
        };

        public OpenSaveDbDialog() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbOpenSave;
        }

        public OpenSaveDbDialog(OpenSaveOption option) : this()
        {
            if (OpenSaveConfigurations.ContainsKey(option))
            {
                this.Text = OpenSaveConfigurations[option].header;
                this.Icon = OpenSaveConfigurations[option].icon;
                okCommand.Text = OpenSaveConfigurations[option].buttonText;
            }
        }

        private void OpenSaveDbDialog_Load(object sender, EventArgs e)
        {
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.ServerName)));

            modelNameData.DataBindings.Add(new Binding(nameof(modelNameData.Text), data, nameof(data.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data, nameof(data.ModelDescription)));
        }

        private void cancelCommand_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void okCommand_Click(object sender, EventArgs e)
        {
            Program.Data.Model.ModelTitle = data.ModelTitle;
            Program.Data.Model.ModelDescription = data.ModelDescription;
            DialogResult = DialogResult.OK;
        }
    }
}
