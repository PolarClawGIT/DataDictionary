using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
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
            (this as IApplicationDataBind).IsLocked = true;

            // Setup Images for Tree Control
            SetImages(dataSourceNavigation, dbDataImageItems.Values);
            SetImages(domainModelNavigation, domainModelImageItems.Values);

            // Setup Tabs
            navigationTabs.ImageList = new ImageList();
            foreach (navigationTabImageIndex item in Enum.GetValues(typeof(navigationTabImageIndex)))
            { navigationTabs.ImageList.Images.Add(item.ToString(), navigationTabImages[item]); }
            navigationDataSourceTab.ImageKey = navigationTabImageIndex.Database.ToString();
            navigationDomainTab.ImageKey = navigationTabImageIndex.Domain.ToString();

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

            System.Timers.Timer splashTimer = new System.Timers.Timer();
            splashTimer.Interval = 5000; // 5 seconds
            splashTimer.Elapsed += MinTime_Elapsed;
            splashTimer.AutoReset = false;
            splashTimer.Enabled = true; // Start
            splashScreen.Show();

            // Setup the Data
            SendMessage(new DoUnbindData());

            if (Settings.Default.IsOnLineMode)
            { this.DoWork(Program.Data.LoadApplicationData(), OnComplete); }
            else { this.DoWork(Program.Data.LoadApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete); }

            // Handle data load complete
            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (splashDone) { splashScreen.Close(); }

                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { // Could not load the data from the database for whatever reason.
                  // Switch to off-line mode and load from file if possible.
                    Settings.Default.IsOnLineMode = false;
                    Settings.Default.Save();
                    this.DoWork(Program.Data.LoadApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete);
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
            modelNameData.DataBindings.Add(new Binding(nameof(modelNameData.Text), Program.Data.Model, nameof(Program.Data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), Program.Data.Model, nameof(Program.Data.Model.ModelDescription)));

            BuildDataSourcesTree();
            BuildDomainModelTreeByAttribute();

            return true;
        }

        public void UnbindDataCore()
        {
            modelNameData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
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
        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Schema), Program.Data.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Table), Program.Data.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Column), Program.Data.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_ExtendedProperty), Program.Data.DbExtendedProperties); }

        [Obsolete()]
        private void menuImportDbSchema_Click(object sender, EventArgs e)
        {
            //Program.Data.ImportDbSchemaToDomain();
            //BuildDomainModelTree();
        }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), Program.Data.DomainAttributeProperties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), Program.Data.DomainAttributeAliases); }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_ClassPublic), Program.Data.DomainEntities); }

        private void entityPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), Program.Data.DomainEntityProperties); }

        private void entityAliasToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), Program.Data.DomainEntityAliases); }

        private void HelpContentsMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Dialogs.HelpSubject(currentForm)); }
        }

        private void HelpIndexMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.HelpSubject()); }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.AboutBox()); }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Dialogs.HelpSubject(currentForm)); }
        }

        private void openSaveModelDatabaseMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.OpenSaveModelDatabase()); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Key), Program.Data.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_KeyColumn), Program.Data.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_DomainType), Program.Data.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Procedure), Program.Data.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Parameter), Program.Data.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Dependancy), Program.Data.DbRoutineDependencies); }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Application.Property()); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (this as IApplicationDataBind).UnbindData();
            Program.Data.Clear();
            Program.Data.NewModel();
            (this as IApplicationDataBind).BindData();
        }

        private void cloneModelMenuItem_Click(object sender, EventArgs e)
        {
            (this as IApplicationDataBind).UnbindData();
            Program.Data.NewModel();
            (this as IApplicationDataBind).BindData();
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

        private void navigationDbSchemaTab_MouseDoubleClick(object sender, MouseEventArgs e)
        { //TODO: Not Working. Does not show when the context menu is assigned to the control or rigged to an event.
            dbSchemaContextMenu.Show();
        }

        private void navigationDomainTab_MouseDoubleClick(object sender, MouseEventArgs e)
        { //TODO: Not Working. Does not show when the context menu is assigned to the control or rigged to an event.
            domainModelMenu.Show();
        }

        private void extendedPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ViewTextTemplate()); }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ApplicationOptions()); }
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


        private void manageLibrariesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Database.CatalogManager()); }

        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Database), Program.Data.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Attribute), Program.Data.DomainAttributes); }
    }
}