using DataDictionary.BusinessLayer.AppLibrary;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager : ApplicationData
    {
        FormBinding formBinding;

        public LibraryManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.Library);
            SetCommand(ScopeType.Library,
                ButtonType.Add,
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase);

            formBinding = new FormBinding()
            {
                ManagerBinding = libraryBinding,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void LibraryManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                libraryNavigation.AutoGenerateColumns = false;
                libraryNavigation.DataSource = libraryBinding;

                libraryTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), libraryBinding, nameof(BindingValue.LibraryTitle)));
                libraryDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), libraryBinding, nameof(BindingValue.LibraryDescription), false, DataSourceUpdateMode.OnPropertyChanged));
                asseblyNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), libraryBinding,nameof(BindingValue.AssemblyName)));
                sourceFileNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), libraryBinding, nameof(BindingValue.SourceFile)));
                sourceFileDate.DataBindings.Add(new Binding(nameof(TextBox.Text), libraryBinding, nameof(BindingValue.SourceDate)));

                // Security
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        private void LibraryTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(libraryTitleData.Text))
            { errorProvider.SetError(libraryTitleData.ErrorControl, "Library Title is required"); }
            else { errorProvider.SetError(libraryTitleData.ErrorControl, String.Empty); }
        }

        private void AsseblyNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(asseblyNameData.Text))
            { errorProvider.SetError(asseblyNameData.ErrorControl, "Assembly Name is required"); }
            else { errorProvider.SetError(asseblyNameData.ErrorControl, String.Empty); }
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();
            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Delete(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Load(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Save(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }


        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            openFileDialog.Filter = "XML VS Documentation|*.XML";
            openFileDialog.Multiselect = true;

            // Work out what directory to start in
            String initPath = Settings.Default.LastLibraryPath;

            if (String.IsNullOrWhiteSpace(initPath) || new DirectoryInfo(initPath).Exists == false)
            {
                initPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                if (new DirectoryInfo(initPath).GetDirectories("source").FirstOrDefault() is DirectoryInfo sourcePath)
                { initPath = sourcePath.FullName; }

                if (new DirectoryInfo(initPath).GetDirectories("repos").FirstOrDefault() is DirectoryInfo repoPath)
                { initPath = repoPath.FullName; }

                Settings.Default.LastLibraryPath = initPath;
                Settings.Default.Save();
            }

            // Open the Dialog
            openFileDialog.InitialDirectory = Settings.Default.LastLibraryPath;
            openFileDialog.FileName = String.Empty;
            DialogResult dialogResult = openFileDialog.ShowDialog();

            // Respond to Dialog options
            if (dialogResult is DialogResult.OK)
            {
                // Save the Directory to LastLibraryPath
                if (openFileDialog.FileNames.FirstOrDefault() is String firstFile)
                {
                    FileInfo file = new FileInfo(firstFile);
                    if (file.Directory is DirectoryInfo firstDirectory && firstDirectory.FullName != initPath)
                    {
                        Settings.Default.LastLibraryPath = firstDirectory.FullName;
                        Settings.Default.Save();
                    }
                }

                formBinding.Import(openFileDialog.FileNames.Select(s => new FileInfo(s)), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(false); }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            libraryNavigation.EndEdit();
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                formBinding.Remove(binding);
                SendMessage(new RefreshNavigation());
            }
        }

        private void LibraryBinding_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                CommandButtons[ButtonType.Delete].IsEnabled = binding.InModel;

                CommandButtons[ButtonType.OpenDatabase].IsEnabled = binding.InDatabase && !binding.InModel;
                CommandButtons[ButtonType.SaveDatabase].IsEnabled = binding.InModel;
                CommandButtons[ButtonType.DeleteDatabase].IsEnabled = binding.InDatabase;
            }
        }
    }
}
