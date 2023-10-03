using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager : ApplicationBase
    {

        LibraryManagerCollection bindingData = new LibraryManagerCollection();

        public LibraryManager()
        {
            InitializeComponent();
            newToolStripButton.Enabled = true;
            openToolStripButton.Enabled = true;
            saveToolStripButton.Enabled = true;
            deleteToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
            openToolStripButton.Click += OpenToolStripButton_Click;
            saveToolStripButton.Click += SaveToolStripButton_Click;
            deleteToolStripButton.Click += DeleteToolStripButton_Click;

            newToolStripButton.ToolTipText = "New Library from XML file";
            openToolStripButton.ToolTipText = "Open existing Library from database and add to Model";
            saveToolStripButton.ToolTipText = "Save the Library in the Model back to the Database";
            deleteToolStripButton.ToolTipText = "Remove the Library from the Model or Delete the Library from the Database (if not in any Model)";
            this.Icon = Resources.Icon_Library;
        }

        private void LibraryManager_Load(object sender, EventArgs e)
        {

            LibrarySourceCollection<LibrarySourceItem> data = new LibrarySourceCollection<LibrarySourceItem>();

            this.DoWork(data.LoadLibrary(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.Build(data);
                BindData();
            }
        }

        void BindData()
        {
            libraryBinding.DataSource = bindingData;

            libraryNavigation.AutoGenerateColumns = false;
            libraryNavigation.DataSource = libraryBinding;

            LibraryManagerItem nameOfValues = new LibraryManagerItem();
            libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), bindingData, nameof(nameOfValues.LibraryTitle)));
            libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), bindingData, nameof(nameOfValues.LibraryDescription)));
            asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), bindingData, nameof(nameOfValues.AssemblyName)));
            sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), bindingData, nameof(nameOfValues.SourceFile)));
            sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), bindingData, nameof(nameOfValues.SourceDate)));

            inModelData.DataBindings.Add(new Binding(nameof(inModelData.Checked), bindingData, nameof(nameOfValues.InModel), true, DataSourceUpdateMode.OnValidation, false));
            inDatabaseData.DataBindings.Add(new Binding(nameof(inDatabaseData.Checked), bindingData, nameof(nameOfValues.InDatabase), true, DataSourceUpdateMode.OnValidation, false));
        }

        void UnBindData()
        {
            libraryTitleData.DataBindings.Clear();
            libraryDescriptionData.DataBindings.Clear();
            asseblyNameData.DataBindings.Clear();
            sourceFileNameData.DataBindings.Clear();
            sourceFileDate.DataBindings.Clear();
            inModelData.DataBindings.Clear();
            inDatabaseData.DataBindings.Clear();

            libraryNavigation.DataSource = null;
            libraryBinding.DataSource = null;
        }

        private void NewToolStripButton_Click(object? sender, EventArgs e)
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
                List<WorkItem> workItems = new List<WorkItem>();

                foreach (String file in openFileDialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    workItems.AddRange(Program.Data.LoadLibrary(fileInfo));
                }

                // Do the work
                UnBindData();
                SendMessage(new Messages.DbDataBatchStarting());
                this.DoWork(workItems, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.RefreshFromModel();
                SendMessage(new Messages.DbDataBatchCompleted());
                BindData();
            }
        }

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
        {
            if (libraryBinding.Current is LibraryManagerItem sourceItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(sourceItem);
                List<WorkItem> work = new List<WorkItem>();

                if (Program.Data.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem existing)
                { work.AddRange(Program.Data.RemoveLibrary(key)); }

                work.AddRange(Program.Data.LoadLibrary(key));

                SendMessage(new Messages.DbDataBatchStarting());
                UnBindData();
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.RefreshFromModel();
                SendMessage(new Messages.DbDataBatchCompleted());
                BindData();
            }
        }

        private void SaveToolStripButton_Click(object? sender, EventArgs e)
        {
            if (libraryBinding.Current is LibraryManagerItem sourceItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(sourceItem);
                List<WorkItem> work = new List<WorkItem>();

                if (Program.Data.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem existing)
                { work.AddRange(Program.Data.SaveLibrary(key)); }

                work.AddRange(Program.Data.RemoveLibrary(key));
                work.AddRange(Program.Data.LoadLibrary(key));

                SendMessage(new Messages.DbDataBatchStarting());
                UnBindData();
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.RefreshFromModel();
                SendMessage(new Messages.DbDataBatchCompleted());
                BindData();

                if (libraryBinding.Current is LibraryManagerItem sourceItem)
                { sourceItem.InDatabase = true; }
            }
        }

        private void DeleteToolStripButton_Click(object? sender, EventArgs e)
        {
            if (libraryBinding.Current is LibraryManagerItem sourceItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(sourceItem);
                List<WorkItem> work = new List<WorkItem>();

                if (Program.Data.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem existing)
                { work.AddRange(Program.Data.RemoveLibrary(key)); }
                else { work.AddRange(Program.Data.DeleteLibrary(key)); }

                SendMessage(new Messages.DbDataBatchStarting());
                UnBindData();
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (libraryBinding.Current is LibraryManagerItem sourceItem && args.Error is null)
                {
                    if (sourceItem.InDatabase && !sourceItem.InModel) { libraryBinding.Remove(sourceItem); }
                    else if (sourceItem.InDatabase && sourceItem.InModel) { sourceItem.InModel = false; }
                    else if (!sourceItem.InDatabase && sourceItem.InModel) { libraryBinding.Remove(sourceItem); }
                }

                bindingData.RefreshFromModel();
                SendMessage(new Messages.DbDataBatchCompleted());
                BindData();
            }
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion


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


    }
}
