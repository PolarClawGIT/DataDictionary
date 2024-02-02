using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main : ApplicationBase, IApplicationDataBind
    {

        #region Static Data
        enum navigationTabImageIndex
        {
            Database,
            Domain
        }
        Dictionary<navigationTabImageIndex, Image> navigationTabImages = new Dictionary<navigationTabImageIndex, Image>()
        {
            {navigationTabImageIndex.Database, Resources.Database },
            {navigationTabImageIndex.Domain, Resources.Dictionary }
        };
        #endregion

        public bool IsOpenItem(object? item)
        { return true; }

        public Main() : base()
        {
            InitializeComponent();
            Icon = Resources.Icon_Application;
            toolStrip.Hide(); // Hide base ToolStrip
            this.IsLocked(true);

            // Setup Images for Tree Control
            nameSpaceNavigation.ImageList = ModelScopeExtension.ToImageList();


            //Hook the WorkerQueue up to this forms UI thread for events.
            Program.Worker.InvokeUsing = this.Invoke;
        }

        #region Form
        private void Main_Load(object sender, EventArgs e)
        {
            Program.Worker.ProgressChanged += WorkerQueue_ProgressChanged;

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
            work.AddRange(Program.Data.LoadNameSpace());


            if (Settings.Default.IsOnLineMode)
            {
                DatabaseWork factory = new DatabaseWork();
                work.Add(factory.OpenConnection());
                work.AddRange(Program.Data.LoadApplicationData(factory));

                if (!appDataFile.Exists)
                { work.AddRange(Program.Data.SaveApplicationData(appDataFile)); }

                this.DoWork(work, OnComplete);
            }
            else
            { FileLoad(); }

            // Handles the Application Data File
            void FileLoad()
            {
                if (appDataFile.Exists) // AppData already contains the Application Data File
                { work.AddRange(Program.Data.LoadApplicationData(appDataFile)); }
                else if (appInstallFile.Exists)
                { // AppData does not contain file but the install folder does (Copy it)
                    work.AddRange(Program.Data.LoadApplicationData(appInstallFile));
                    work.AddRange(Program.Data.SaveApplicationData(appDataFile));
                }
                this.DoWork(work, OnComplete);
            }

            // Handle data load complete
            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (splashDone) { splashScreen.Close(); }

                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { // Could not load the data from the database for whatever reason.
                  // Switch to off-line mode and load from file if possible.
                    Settings.Default.IsOnLineMode = false;
                    Settings.Default.Save();

                    FileLoad();
                }
                else
                {
                    SendMessage(new DoBindData());
                    SendMessage(new OnlineStatusChanged());
                    dataLoaded = true;
                }
            }

            // Handle Splash timer timed out.
            void MinTime_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
            {
                if (dataLoaded) { this.Invoke(() => splashScreen.Close()); }
                splashDone = true;
                splashTimer.Elapsed -= MinTime_Elapsed;
            }
        }


        public bool BindDataCore()
        {
            if (Program.Data.Model is ModelItem data)
            {
                BuildNameSpaceTree();

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            ClearNameSpaceTree();
        }

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        { Program.Worker.ProgressChanged -= WorkerQueue_ProgressChanged; }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }
        #endregion

        #region Menu Events
        private void HelpContentsMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Forms.App.HelpSubject(currentForm)); }
            else { Activate(() => new Forms.App.HelpSubject(Settings.Default.DefaultSubject)); }
        }

        private void HelpIndexMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.App.HelpSubject(Settings.Default.DefaultSubject)); }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.AboutBox()); }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Forms.App.HelpSubject(currentForm)); }
        }

        private void browsePropertiesCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.App.Property()); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (this as IApplicationDataBind).UnbindData();
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Program.Data.NewModel());

            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                (this as IApplicationDataBind).BindData();
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
        { Activate(() => new App.ApplicationOptions()); }
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
            { toolStripOnlineStatus.Text = String.Format("On-Line: {0}.{0}", Program.Data.ServerName, Program.Data.DatabaseName); }
            else { toolStripOnlineStatus.Text = "Off-Line"; }
        }
        #endregion

        private void gridViewToolStripMenuItem_Click(object sender, EventArgs e)
        { new Forms.UnitTestGridView().Show(); }

        [Obsolete("Proof of Concept Only")]
        private void testFormToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ProofOfConcept.CodeLibrary()); }

        private void peekAtClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.ClipboardView()); }

        private void textEditorToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ProofOfConcept.TextEditor()); }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML Data Dictionary|*.XML";

            if (Program.Data.ModelFile is FileInfo file)
            {
                openFileDialog.InitialDirectory = file.DirectoryName;
                openFileDialog.FileName = file.Name;
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.FileName = Program.Data.Model.ModelTitle;
            }

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult is DialogResult.OK)
            {
                FileInfo openFile = new FileInfo(openFileDialog.FileName);

                SendMessage(new Messages.DoUnbindData());
                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(Program.Data.LoadModelData(openFile));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Data.ModelFile is FileInfo file)
            {
                SendMessage(new Messages.DoUnbindData());
                DoWork(Program.Data.SaveModelData(Program.Data.ModelFile), onCompleting);
            }
            else
            { saveAsToolStripMenuItem_Click(sender, e); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML Data Dictionary|*.XML";

            if (Program.Data.ModelFile is FileInfo file)
            {
                saveFileDialog.InitialDirectory = file.DirectoryName;
                saveFileDialog.FileName = file.Name;
            }
            else
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.FileName = Program.Data.Model.ModelTitle;
            }

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult is DialogResult.OK)
            {
                FileInfo openFile = new FileInfo(saveFileDialog.FileName);
                Program.Data.ModelFile = openFile;
                saveToolStripMenuItem.Enabled = true;

                SendMessage(new Messages.DoUnbindData());
                DoWork(Program.Data.SaveModelData(openFile), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }

        }
    }
}