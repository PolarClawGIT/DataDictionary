using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main : ApplicationBase
    {
        public Boolean IsOpenItem(object? item)
        { return true; }

        public Main() : base()
        {
            InitializeComponent();
            Icon = Resources.Icon_Application;
            toolStrip.Hide(); // Hide base ToolStrip
            IsLocked(true);

            // Setup Images for Tree Control
            contextNameNavigation.ImageList = ModelScopeExtension.ToImageList();


            //Hook the WorkerQueue up to this forms UI thread for events.
            Worker.InvokeUsing = this.Invoke;
        }

        #region Form
        private void Main_Load(object sender, EventArgs e)
        {
            Worker.ProgressChanged += WorkerQueue_ProgressChanged;

            // Display the Splash screen (Show a minimum of 10 seconds or wait for data to Load)
            Dialogs.AboutBox splashScreen = new Dialogs.AboutBox();
            Boolean dataLoaded = false;
            Boolean splashDone = false;
            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
            FileInfo appInstallFile = new FileInfo(Settings.Default.AppDataFile);

            System.Timers.Timer splashTimer = new System.Timers.Timer();
            splashTimer.Interval = 5000; // 5 seconds
            splashTimer.Elapsed += MinTime_Elapsed;
            splashTimer.AutoReset = false;
            splashTimer.Enabled = true; // Start
            splashScreen.Show();

            // Setup the Data
            SendMessage(new DoUnbindData());

            List<WorkItem> work = new List<WorkItem>();
            List<NameScopeItem> names = new List<NameScopeItem>();
            work.AddRange(BusinessData.Export(names));
            work.AddRange(BusinessData.NameScope.Import(names));

            if (Settings.Default.IsOnLineMode)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.Load(factory));

                if (!appDataFile.Exists)
                { work.AddRange(BusinessData.ApplicationData.Export(appDataFile)); }

                this.DoWork(work, OnComplete);
            }
            else
            { FileLoad(); }

            // Handles the Application Data File
            void FileLoad()
            {
                if (appDataFile.Exists) // AppData already contains the Application Data File
                { work.AddRange(BusinessData.ApplicationData.Import(appDataFile)); }
                else if (appInstallFile.Exists)
                { // AppData does not contain file but the install folder does (Copy it)
                    work.AddRange(BusinessData.ApplicationData.Import(appInstallFile));
                    work.AddRange(BusinessData.ApplicationData.Export(appDataFile));
                }
                this.DoWork(work, OnComplete);
            }

            // Handle data load complete
            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (splashDone)
                { this.Invoke(() => splashScreen.Close()); }

                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { // Could not load the data from the database for whatever reason.
                  // Switch to off-line mode and load from file if possible.
                    Settings.Default.IsOnLineMode = false;
                    Settings.Default.Save();

                    FileLoad();
                }
                else
                {
                    BuildTree();
                    SendMessage(new OnlineStatusChanged());
                    dataLoaded = true;
                    IsLocked(false);
                }
            }

            // Handle Splash timer timed out.
            void MinTime_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
            {
                if (dataLoaded)
                {
                    this.Invoke(() =>
                    {
                        splashScreen.Close();
                        IsLocked(false);
                    });
                }

                splashDone = true;
                splashTimer.Elapsed -= MinTime_Elapsed;

            }
        }


        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        { Worker.ProgressChanged -= WorkerQueue_ProgressChanged; }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }
        #endregion

        #region Menu Events
        private void HelpContentsMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is Form currentForm && currentForm is not AboutBox)
            { Activate(() => new Forms.ApplicationWide.HelpSubject(currentForm)); }
            else { Activate(() => new Forms.ApplicationWide.HelpSubject(Settings.Default.DefaultSubject)); }
        }

        private void HelpIndexMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.ApplicationWide.HelpSubject(Settings.Default.DefaultSubject)); }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.AboutBox()); }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Forms.ApplicationWide.HelpSubject(currentForm)); }
        }

        private void browsePropertiesCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.ApplicationWide.Property()); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            List<NameScopeItem> names = new List<NameScopeItem>();
            work.AddRange(BusinessData.Remove());
            work.AddRange(BusinessData.NameScope.Remove());
            work.AddRange(BusinessData.Export(names));
            work.AddRange(BusinessData.NameScope.Import(names));

            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                ClearTree();
                BuildTree();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsCutCommand() { HandledBy = this.ActiveMdiChild }); }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsCopyCommand() { HandledBy = this.ActiveMdiChild }); }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsPasteCommand() { HandledBy = this.ActiveMdiChild }); }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsUndoCommand() { HandledBy = this.ActiveMdiChild }); }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsSelectAllCommand() { HandledBy = this.ActiveMdiChild }); }

        private void extendedPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ViewTextTemplate()); }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ApplicationWide.ApplicationOptions()); }
        #endregion

        #region IColleague

        protected override void HandleMessage(FormAddMdiChild message)
        {
            if (!ReferenceEquals(this, message.ChildForm) && message.ChildForm.MdiParent is null)
            { message.ChildForm.MdiParent = this; }
        }

        protected override void HandleMessage(DoUnbindData message)
        {
            //TODO: can a different event handle this?
            this.Controls[0].Focus();
            base.HandleMessage(message);
        }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            if (Settings.Default.IsOnLineMode)
            { toolStripOnlineStatus.Text = String.Format("On-Line: {0}.{0}", BusinessData.Connection.ServerName, BusinessData.Connection.DatabaseName); }
            else { toolStripOnlineStatus.Text = "Off-Line"; }
        }
        #endregion

        private void gridViewToolStripMenuItem_Click(object sender, EventArgs e)
        { new Forms.UnitTestGridView().Show(); }

        private void peekAtClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.ClipboardView()); }

        private void textEditorToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ProofOfConcept.TextEditor()); }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML Data Dictionary|*.XML";

            if (BusinessData.ModelFile is FileInfo file)
            {
                openFileDialog.InitialDirectory = file.DirectoryName;
                openFileDialog.FileName = file.Name;
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.FileName = BusinessData.Model.ModelTitle;
            }

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult is DialogResult.OK)
            {
                FileInfo openFile = new FileInfo(openFileDialog.FileName);

                SendMessage(new Messages.DoUnbindData());
                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(BusinessData.Import(openFile));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BusinessData.ModelFile is FileInfo file)
            {
                SendMessage(new Messages.DoUnbindData());
                DoWork(BusinessData.Export(file), onCompleting);
            }
            else
            { saveAsToolStripMenuItem_Click(sender, e); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML Data Dictionary|*.XML";

            if (BusinessData.ModelFile is FileInfo file)
            {
                saveFileDialog.InitialDirectory = file.DirectoryName;
                saveFileDialog.FileName = file.Name;
            }
            else
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.FileName = BusinessData.Model.ModelTitle;
            }

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult is DialogResult.OK)
            {
                FileInfo openFile = new FileInfo(saveFileDialog.FileName);
                BusinessData.ModelFile = openFile;
                saveToolStripMenuItem.Enabled = true;

                SendMessage(new Messages.DoUnbindData());
                DoWork(BusinessData.Export(openFile), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }

        }
    }
}