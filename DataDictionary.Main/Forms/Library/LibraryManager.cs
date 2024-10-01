using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager : ApplicationData
    {
        LibrarySynchronize libraries = new LibrarySynchronize(BusinessData.LibraryModel);

        public LibraryManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.Library);
            SetCommand(ScopeType.Library,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);
        }

        private void LibraryManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(libraries.GetLibraries(factory));
                DoWork(work, onCompleting);
            }
            else { BindData(); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                BindData();
                IsLocked(false);
            }

            void BindData()
            {
                LibrarySynchronizeValue libraryNames;
                libraryBinding.DataSource = libraries;
                Func<String, String> FormatName = (name) => { return String.Format("{0}.{1}", nameof(libraryNames.Source), name); };

                libraryNavigation.AutoGenerateColumns = false;
                libraryNavigation.DataSource = libraryBinding;

                libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), libraryBinding, FormatName(nameof(libraryNames.Source.LibraryTitle))));
                libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), libraryBinding, FormatName(nameof(libraryNames.Source.LibraryDescription)), false, DataSourceUpdateMode.OnPropertyChanged));
                asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), libraryBinding, FormatName(nameof(libraryNames.Source.AssemblyName))));
                sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), libraryBinding, FormatName(nameof(libraryNames.Source.SourceFile))));
                sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), libraryBinding, FormatName(nameof(libraryNames.Source.SourceDate))));
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

        private Boolean GetInModel()
        {
            if (libraryBinding.Current is LibrarySynchronizeValue item)
            { return item.InModel == true; }
            else { return false; }
        }

        private Boolean GetInDatabase()
        {
            if (libraryBinding.Current is LibrarySynchronizeValue item)
            { return item.InDatabase == true; }
            else { return false; }
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibrarySynchronizeValue value && value.Source is ILibrarySourceValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                LibrarySourceIndex key = new LibrarySourceIndex(item);

                work.Add(factory.OpenConnection());
                work.AddRange(libraries.DeleteFromDb(factory, key));
                work.AddRange(libraries.GetLibraries(factory));
                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                IsLocked(false);
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibrarySynchronizeValue value && value.Source is ILibrarySourceValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                LibrarySourceIndex key = new LibrarySourceIndex(item);
                work.Add(factory.OpenConnection());
                work.AddRange(libraries.OpenFromDb(factory, key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibrarySynchronizeValue value && value.Source is ILibrarySourceValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                LibrarySourceIndex key = new LibrarySourceIndex(item);

                work.Add(factory.OpenConnection());

                if (GetInModel())
                {
                    work.AddRange(libraries.SaveToDb(factory, key));
                    work.AddRange(libraries.GetLibraries(factory));
                }

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                IsLocked(false);
            }
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

                // Create the work items for each of the files selected
                List<WorkItem> work = new List<WorkItem>();

                foreach (String file in openFileDialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    work.AddRange(libraries.ImportFromFile(fileInfo));
                }

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            libraryNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            if (libraryBinding.Current is LibrarySynchronizeValue value && value.Source is ILibrarySourceValue item)
            {
                IsLocked(true);
                LibrarySourceIndex key = new LibrarySourceIndex(item);
                work.AddRange(BusinessData.LibraryModel.Delete(key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                libraries.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        private void LibraryBinding_CurrentChanged(object sender, EventArgs e)
        {
            CommandButtons[CommandImageType.Delete].IsEnabled = GetInModel();
            CommandButtons[CommandImageType.OpenDatabase].IsEnabled = GetInDatabase() && !GetInModel();
            CommandButtons[CommandImageType.SaveDatabase].IsEnabled = GetInModel();
            CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = GetInDatabase();
        }
    }
}
