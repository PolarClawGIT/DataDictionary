using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class CatalogManager : ApplicationData
    {
        FormBinding formBinding;

        public CatalogManager() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.Database);
            SetCommand(
                ButtonType.Add,
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase);

            formBinding = new FormBinding()
            {
                ManagerBinding = catalogBinding,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void CatalogManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                catalogNavigation.AutoGenerateColumns = false;
                catalogNavigation.DataSource = catalogBinding;

                catalogTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), catalogBinding, nameof(BindingValue.CatalogTitle)));
                catalogDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), catalogBinding, nameof(BindingValue.CatalogDescription), false, DataSourceUpdateMode.OnPropertyChanged));
                sourceServerNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), catalogBinding, nameof(BindingValue.ServerName)));
                sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), catalogBinding, nameof(BindingValue.DatabaseName)));
                sourceDateData.DataBindings.Add(new Binding(nameof(TextBox.Text), catalogBinding, nameof(BindingValue.SourceDate)));

                // Security
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            using (Dialogs.ServerConnectionDialog dialog = new Dialogs.ServerConnectionDialog())
            {
                if (catalogBinding.Current is ICatalogValue catalogItem
                    && catalogItem.ServerName is String
                    && catalogItem.DatabaseName is String)
                {
                    dialog.ServerName = catalogItem.ServerName;
                    dialog.DatabaseName = catalogItem.DatabaseName;
                }

                foreach (String? item in Settings.Default.UserServers)
                {
                    if (String.IsNullOrEmpty(item)) { continue; }
                    else
                    {
                        string[] dbServer = item.Split('.');

                        if (dbServer.Length > 1 &&
                            dbServer[0] is String serverName &&
                            dbServer[1] is String databaseName)
                        { dialog.Servers.Add((serverName, databaseName)); }
                    }
                }

                dialog.OpenHelp = () =>
                {
                    General.HelpContent helpForm = Activate(() => new General.HelpContent());
                    helpForm.OpenSubject(dialog);
                };

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    String newValue = String.Format("{0}.{1}", dialog.ServerName, dialog.DatabaseName);

                    // Handle the User Settings
                    if (Settings.Default.UserServers.Contains(newValue))
                    { Settings.Default.UserServers.Remove(newValue); }

                    Settings.Default.UserServers.Insert(0, newValue);

                    while (Settings.Default.UserServers.Count > 10)
                    { Settings.Default.UserServers.RemoveAt(10); }

                    Settings.Default.Save();

                    IsLocked(true);
                    DbSchemaContext source = new BusinessLayer.DbSchemaContext()
                    {
                        ServerName = dialog.ServerName,
                        DatabaseName = dialog.DatabaseName
                    };
                    formBinding.Import(source, onCompleting);
                }
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(false); }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            catalogNavigation.EndEdit();
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                formBinding.Remove(binding);
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Delete(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Load(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Save(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        private void CatalogBinding_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                CommandButtons[ButtonType.Delete].Enabled = binding.InModel;

                CommandButtons[ButtonType.OpenDatabase].Enabled = binding.InDatabase && !binding.InModel;
                CommandButtons[ButtonType.SaveDatabase].Enabled = binding.InModel;
                CommandButtons[ButtonType.DeleteDatabase].Enabled = binding.InDatabase;
            }
        }
    }
}
