using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.LibraryData.Source;
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

        LibrarySourceCollection dbData = new LibrarySourceCollection();
        LibraryManagerCollection bindingData = new LibraryManagerCollection();

        public LibraryManager()
        {
            InitializeComponent();
            openToolStripButton.Enabled = true;
            saveToolStripButton.Enabled = Settings.Default.IsOnLineMode;
            openToolStripButton.Click += OpenToolStripButton_Click;
            saveToolStripButton.Click += SaveToolStripButton_Click;

            openToolStripButton.ToolTipText = "Open and add from ASP.Net XML Documentation file";
            saveToolStripButton.ToolTipText = "Save the changes back to the database";
            this.Icon = Resources.Icon_Library;
        }

        private void LibraryManager_Load(object sender, EventArgs e)
        {
            this.DoWork(dbData.LoadLibrary(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.Build(Program.Data.LibrarySources, dbData);
                BindData();
            }
        }

        void BindData()
        {
            libraryBinding.DataSource = bindingData;

            libraryNavigation.AutoGenerateColumns = false;
            libraryNavigation.DataSource = libraryBinding;

            LibraryManagerItem nameOfValues = new LibraryManagerItem();
            libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), libraryBinding, nameof(nameOfValues.LibraryTitle)));
            libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), libraryBinding, nameof(nameOfValues.LibraryDescription)));
            asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), libraryBinding, nameof(nameOfValues.AssemblyName)));
            sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), libraryBinding, nameof(nameOfValues.SourceFile)));
            sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), libraryBinding, nameof(nameOfValues.SourceDate)));

            inModelData.DataBindings.Add(new Binding(nameof(inModelData.Checked), libraryBinding, nameof(nameOfValues.InModel), true, DataSourceUpdateMode.OnValidation, false));
            inDatabaseData.DataBindings.Add(new Binding(nameof(inDatabaseData.Checked), libraryBinding, nameof(nameOfValues.InDatabase), true, DataSourceUpdateMode.OnValidation, false));
            this.UnLockForm();
        }

        void UnBindData()
        {
            this.LockForm();
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

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
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
                SendMessage(new Messages.DbDataBatchCompleted());
                bindingData.Build(Program.Data.LibrarySources, dbData);
                BindData();
            }
        }

        private void SaveToolStripButton_Click(object? sender, EventArgs e)
        {
            libraryNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            foreach (LibraryManagerItem item in bindingData.ToList())
            {
                LibrarySourceKey key = new LibrarySourceKey(item);
                Boolean inDbList = (dbData.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem);
                Boolean inModelList = (Program.Data.LibrarySources.FirstOrDefault(w => key.Equals(w)) is LibrarySourceItem);

                if (item.InModel && !inDbList)
                { work.AddRange(Program.Data.SaveLibrary(item)); }
                else if (item.InModel && item.InDatabase && inModelList)
                { work.AddRange(Program.Data.SaveLibrary(item)); }
                else if (item.InModel && item.InDatabase && !inModelList)
                { work.AddRange(Program.Data.LoadLibrary(item)); }
                else if (!item.InModel && item.InDatabase && inModelList)
                {
                    work.AddRange(Program.Data.SaveLibrary(item));
                    work.AddRange(Program.Data.RemoveLibrary(item));
                }
                else if (!item.InModel && !item.InDatabase && inModelList && !inDbList)
                { work.AddRange(Program.Data.RemoveLibrary(item)); }
                else if (!item.InModel && !item.InDatabase && inModelList && inDbList)
                {
                    work.AddRange(Program.Data.RemoveLibrary(item));
                    work.AddRange(Program.Data.DeleteLibrary(item));
                    bindingData.Remove(item);
                }
                else if (!item.InModel && !item.InDatabase && !inModelList && inDbList)
                {
                    work.AddRange(Program.Data.DeleteLibrary(item));
                    bindingData.Remove(item);
                }
            }

            UnBindData();
            SendMessage(new Messages.DbDataBatchStarting());

            dbData.Clear();
            work.AddRange(dbData.LoadLibrary());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new Messages.DbDataBatchCompleted());
                bindingData.Build(Program.Data.LibrarySources, dbData);
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

        private void libraryBinding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.Exception is not null)
            { }// For Debugging
        }
    }
}
