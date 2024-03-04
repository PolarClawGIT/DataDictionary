using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager : ApplicationBase, IApplicationDataBind
    {
        public Boolean IsOpenItem(object? item)
        { return true; }

        LibrarySourceCollection dbData = new LibrarySourceCollection();
        LibraryManagerCollection bindingData = new LibraryManagerCollection();

        Boolean inModelList
        {
            get
            {
                if (libraryBinding.Current is LibraryManagerItem item)
                {
                    LibrarySourceKey key = new LibrarySourceKey(item);
                    return (BusinessData.LibraryModel.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem);
                }
                else { return false; }
            }
        }

        Boolean inDatabaseList
        {
            get
            {
                if (libraryBinding.Current is LibraryManagerItem item)
                {
                    LibrarySourceKey key = new LibrarySourceKey(item);
                    return (dbData.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem);
                }
                else { return false; }
            }
        }

        public LibraryManager()
        {
            InitializeComponent();

            newItemCommand.Enabled = true;
            newItemCommand.Click += NewItemCommand_Click;
            newItemCommand.Image = Resources.NewLibrary;
            newItemCommand.ToolTipText = "Import a Visual Studio XML Documentation file to the Model";

            deleteItemCommand.Enabled = true;
            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.Image = Resources.DeleteLibrary;
            deleteItemCommand.ToolTipText = "Removes the Library from the Model";

            openFromDatabaseCommand.Click += OpenFromDatabaseCommand_Click; ;
            deleteFromDatabaseCommand.Click += DeleteFromDatabaseCommand_Click;
            saveToDatabaseCommand.Click += SaveToDatabaseCommand_Click;

            this.Icon = Resources.Icon_Library;
        }

        private void LibraryManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(LoadLocalData(factory));
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { this.BindData(); }
        }

        public Boolean BindDataCore()
        {
            bindingData.Build(BusinessData.LibraryModel.LibrarySources, dbData);
            libraryBinding.DataSource = bindingData;

            libraryNavigation.AutoGenerateColumns = false;
            libraryNavigation.DataSource = libraryBinding;

            LibraryManagerItem? nameOfValues;
            libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), libraryBinding, nameof(nameOfValues.LibraryTitle)));
            libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), libraryBinding, nameof(nameOfValues.LibraryDescription)));
            asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), libraryBinding, nameof(nameOfValues.AssemblyName)));
            sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), libraryBinding, nameof(nameOfValues.SourceFile)));
            sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), libraryBinding, nameof(nameOfValues.SourceDate)));
            return true;
        }

        public void UnbindDataCore()
        {
            libraryTitleData.DataBindings.Clear();
            libraryDescriptionData.DataBindings.Clear();
            asseblyNameData.DataBindings.Clear();
            sourceFileNameData.DataBindings.Clear();
            sourceFileDate.DataBindings.Clear();

            libraryNavigation.DataSource = null;
            libraryBinding.DataSource = null;
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
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
                List<NameScopeItem> names = new List<NameScopeItem>();

                foreach (String file in openFileDialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    work.AddRange(BusinessData.LibraryModel.Import(fileInfo));
                    work.AddRange(BusinessData.LibraryModel.Export(names));
                    work.AddRange(BusinessData.NameScope.Import(names));
                }

                DoLocalWork(work);
            }
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibraryManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                LibrarySourceKey key = new LibrarySourceKey(item);
                NameScopeKey scopeKey = new NameScopeKey(item);

                work.AddRange(BusinessData.LibraryModel.Remove(key));
                work.Add(
                    new WorkItem()
                    {
                        WorkName = "Remove NameScope",
                        DoWork = () => { BusinessData.NameScope.Remove(scopeKey); }
                    });

                DoLocalWork(work);
            }
        }

        private void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibraryManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();

                ILibrarySourceKey key = new LibrarySourceKey(item);
                work.Add(factory.OpenConnection());
                Boolean inModelList = (BusinessData.LibraryModel.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem);

                if (inModelList)
                {
                    work.AddRange(BusinessData.LibraryModel.Remove(key));
                    work.AddRange(BusinessData.LibraryModel.Save(factory, key));
                }
                else
                {
                    work.Add(new WorkItem() { DoWork = () => { dbData.Remove(key); } });
                    work.Add(factory.CreateSave(dbData, key));
                }

                work.AddRange(LoadLocalData(factory));
                DoLocalWork(work);
            }
        }

        private void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibraryManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<NameScopeItem> names = new List<NameScopeItem>();
                work.Add(factory.OpenConnection());

                LibrarySourceKey key = new LibrarySourceKey(item);
                work.AddRange(BusinessData.LibraryModel.Load(factory, key));
                work.AddRange(LoadLocalData(factory));
                work.AddRange(BusinessData.LibraryModel.Export(names));
                work.AddRange(BusinessData.NameScope.Import(names));
                DoLocalWork(work);
            }


        }

        private void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            libraryNavigation.EndEdit();

            if (libraryBinding.Current is LibraryManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                ILibrarySourceKey key = new LibrarySourceKey(item);
                work.Add(factory.OpenConnection());

                if (inModelList)
                { work.AddRange(BusinessData.LibraryModel.Save(factory, key)); }
                else
                { work.Add(factory.CreateSave(dbData, key)); }

                work.AddRange(LoadLocalData(factory));

                DoLocalWork(work);
            }
        }

        private IReadOnlyList<WorkItem> LoadLocalData(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Clear local data", DoWork = dbData.Clear });
            work.Add(factory.CreateLoad(dbData));

            return work;
        }

        private void DoLocalWork(List<WorkItem> work)
        {
            SendMessage(new Messages.DoUnbindData());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void libraryTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(libraryTitleData.Text))
            { errorProvider.SetError(libraryTitleData.ErrorControl, "Library Title is required"); }
            else { errorProvider.SetError(libraryTitleData.ErrorControl, String.Empty); }
        }

        private void asseblyNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(asseblyNameData.Text))
            { errorProvider.SetError(asseblyNameData.ErrorControl, "Assembly Name is required"); }
            else { errorProvider.SetError(asseblyNameData.ErrorControl, String.Empty); }
        }

        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { if (sender is BindingSource binding) { binding.BindComplete(sender, e); } }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);

            openFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode;
            saveToDatabaseCommand.Enabled = Settings.Default.IsOnLineMode;
            deleteFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode;
        }

        private void libraryBinding_CurrentChanged(object sender, EventArgs e)
        {
            if (libraryBinding.Current is LibraryManagerItem item)
            {
                LibrarySourceKey key = new LibrarySourceKey(item);

                deleteItemCommand.Enabled = inModelList;
                openFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList;
                deleteFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList;
                saveToDatabaseCommand.Enabled = Settings.Default.IsOnLineMode;
            }
        }
    }
}
