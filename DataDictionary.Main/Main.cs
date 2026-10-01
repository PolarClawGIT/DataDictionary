using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Forms.ApplicationWide;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
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
            // TODO: The designer does not execute this stuff.
            // Image in design view may not match what is in the WinFormEnumeration.
            // Result, there are two places that the image needs to be maintained.
            // The Form (every form) and the WinFormEnumeration. 
            // The desire is to have only one places of "truth"

            InitializeComponent();
            Icon = ScopeType.Application.GetIcon();

            BindingPropertyChanged.ValidateInit();

            // Set the button images based on Scope.
            optionsToolStripMenuItem.Image = ScopeType.ApplicationOption.GetImage(ButtonType.Default);

            newAttributeCommand.Image = ScopeType.ModelAttribute.GetImage(ButtonType.Add);
            newAttributeCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newEntityCommand.Image = ScopeType.ModelEntity.GetImage(ButtonType.Add);
            newEntityCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newProcessCommand.Image = ScopeType.ModelProcess.GetImage(ButtonType.Add);
            newProcessCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newSubjectAreaCommand.Image = ScopeType.ModelSubjectArea.GetImage(ButtonType.Add);
            newSubjectAreaCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;

            manageModelCommand.Image = ScopeType.Model.GetImage(ButtonType.Default);
            manageModelCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            manageScriptingCommand.Image = ScopeType.Scripting.GetImage(ButtonType.Default);
            manageScriptingCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            securitySetAuthorization.Image = ScopeType.Security.GetImage(ButtonType.Default);
            browseHelpCommand.Image = ScopeType.ApplicationHelp.GetImage(ButtonType.Default);
            databaseMessagesCommand.Image = ScopeType.ApplicationLog.GetImage(ButtonType.Default);

            securityAuthorization.Image = ScopeType.SecuritySecurable.GetImage(ButtonType.Default);

            manageLibrariesCommand.Image = ScopeType.Library.GetImage(ButtonType.Default);
            manageLibrariesCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            viewLibraryMemberCommand.Image = ScopeType.LibraryType.GetImage(ButtonType.Default);
            viewLibrarySourceCommand.Image = ScopeType.Library.GetImage(ButtonType.Default);

            menuAttributes.Image = ScopeType.ModelAttribute.GetImage(ButtonType.Default);
            menuAttributeAlaises.Image = ScopeType.ModelAttributeAlias.GetImage(ButtonType.Default);
            menuAttributeProperties.Image = ScopeType.ModelAttributeProperty.GetImage(ButtonType.Default);
            menuAttributeDefinitions.Image = ScopeType.ModelAttributeDefinition.GetImage(ButtonType.Default);

            menuEntities.Image = ScopeType.ModelEntity.GetImage(ButtonType.Default);
            menuEntityAttributes.Image = ScopeType.ModelEntityAttribute.GetImage(ButtonType.Default);
            menuEntityAlias.Image = ScopeType.ModelEntityAlias.GetImage(ButtonType.Default);
            menuEntityProperties.Image = ScopeType.ModelEntityProperty.GetImage(ButtonType.Default);
            menuEntityDefinitions.Image = ScopeType.ModelEntityDefinition.GetImage(ButtonType.Default);

            menuProcess.Image = ScopeType.ModelProcess.GetImage(ButtonType.Default);
            menuProcessArgument.Image = ScopeType.ModelProcessArgument.GetImage(ButtonType.Default);
            menuProcessAlias.Image = ScopeType.ModelProcessAlias.GetImage(ButtonType.Default);
            menuProcessProperty.Image = ScopeType.ModelProcessProperty.GetImage(ButtonType.Default);
            menuProcessDefinition.Image = ScopeType.ModelProcessDefinition.GetImage(ButtonType.Default);

            menuSubjectArea.Image = ScopeType.ModelSubjectArea.GetImage(ButtonType.Default);
            menuModelProperty.Image = ScopeType.ModelProperty.GetImage(ButtonType.Default);
            menuModelDefinition.Image = ScopeType.ModelDefinition.GetImage(ButtonType.Default);

            manageDatabasesCommand.Image = ScopeType.Database.GetImage(ButtonType.Default);
            manageDatabasesCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;

            menuCatalogItem.Image = ScopeType.Database.GetImage(ButtonType.Default);
            menuSchemaItem.Image = ScopeType.DatabaseSchema.GetImage(ButtonType.Default);
            menuReferenceItem.Image = ScopeType.DatabaseReference.GetImage(ButtonType.Default);
            menuTableItem.Image = ScopeType.DatabaseTable.GetImage(ButtonType.Default);
            menuTableColumnItem.Image = ScopeType.DatabaseTableColumn.GetImage(ButtonType.Default);
            menuRoutineColumnItem.Image = ScopeType.DatabaseFunctionColumn.GetImage(ButtonType.Default);
            menuDomainItem.Image = ScopeType.DatabaseDomain.GetImage(ButtonType.Default);
            menuPropertyItem.Image = ScopeType.DatabaseProperty.GetImage(ButtonType.Default);
            menuConstraintItem.Image = ScopeType.DatabaseConstraint.GetImage(ButtonType.Default);
            menuConstraintColumnItem.Image = ScopeType.DatabaseConstraintCheck.GetImage(ButtonType.Default);
            menuRoutineItem.Image = ScopeType.DatabaseProcedure.GetImage(ButtonType.Default); ;
            menuRoutineParameterItem.Image = ScopeType.DatabaseProcedureParameter.GetImage(ButtonType.Default);

            manageTemplateCommand.Image = ScopeType.Scripting.GetImage(ButtonType.Default);
            manageTemplateCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            menuNewTemplate.Image = ScopeType.ScriptingTemplate.GetImage(ButtonType.Add);
            menuTemplate.Image = ScopeType.ScriptingTemplate.GetImage(ButtonType.Default);
            menuTemplateSchemaDocument.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Default);
            menuTemplateTransformDocument.Image = ScopeType.ApplicationDocument.GetImage(ButtonType.Default);
            menuTemplateNode.Image = ScopeType.ScriptingNode.GetImage(ButtonType.Default);
            menuTemplateSchemata.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Default);
            menuTemplateTransform.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Default);

            namedScopeData.DoWork = DoWork; // Pass the work method to the control

            IsLocked(true);

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
            System.Timers.Timer splashTimer = new System.Timers.Timer();
            splashTimer.Interval = 5000; // 5 seconds
            splashTimer.Elapsed += MinTime_Elapsed;
            splashTimer.AutoReset = false;
            splashTimer.Enabled = true; // Start
            splashScreen.Show();

            IsLocked(true);
            Program.SetupApplicationData(DataLoadComplete);

            void DataLoadComplete(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { // Could not load the data from the database for whatever reason.
                  // Switch to off-line mode and load from file if possible.
                    Settings.Default.IsOnLineMode = false;
                    Settings.Default.Save();
                }

                // Override default Directories
                if (!String.IsNullOrWhiteSpace(Settings.Default.UserProjects))
                { DirectoryType.Projects.GetEnumeration().RelativeFolder = Settings.Default.UserProjects; }

                if (!String.IsNullOrWhiteSpace(Settings.Default.UserData))
                { DirectoryType.Dictionary.GetEnumeration().RelativeFolder = Settings.Default.UserData; }

                if (args.Error is not null)
                { Program.ShowException(args.Error); }

                bindingModel.DataSource = BusinessData.Model.Models;
                SendMessage(new OnlineStatusChanged());

                DoWork(BusinessData.Create());

                IsLocked(false);
                dataLoaded = true;

                SetAuthorization();
                namedScopeData.ReloadCommand();

                if (splashDone)
                { this.Invoke(() => { splashScreen.Close(); }); }

            }

            // Handle Splash timer timed out.
            void MinTime_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
            {
                if (dataLoaded && !IsDisposed)
                { this.Invoke(() => { if (!splashScreen.IsDisposed) { splashScreen.Close(); } }); }

                splashTimer.Elapsed -= MinTime_Elapsed;
                splashDone = true;
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
        { Forms.General.HelpContent helpForm = Activate(() => new Forms.General.HelpContent()); }

        private void HelpIndexMenuItem_Click(object sender, EventArgs e)
        { throw new NotImplementedException(); } // Not Used.

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.AboutBox()); }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (ActiveMdiChild is Form currentForm)
            {
                Forms.General.HelpContent helpForm = Activate(() => new Forms.General.HelpContent());
                helpForm.OpenSubject(currentForm);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(BusinessData.Create());

            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { namedScopeData.ReloadCommand(); }
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ApplicationWide.ApplicationOptions()); }
        #endregion

        #region IColleague

        protected override void HandleMessage(FormAddMdiChild message)
        {
            if (!ReferenceEquals(this, message.ChildForm) && message.ChildForm.MdiParent is null)
            { message.ChildForm.MdiParent = this; }
        }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            toolStripStatusUser.Text = BusinessData.Authorization.PrincipalName;
            SetAuthorization();

            if (Settings.Default.IsOnLineMode)
            { toolStripOnlineStatus.Text = String.Format("On-Line: [{0}].[{1}]", BusinessData.Connection.ServerName, BusinessData.Connection.DatabaseName); }
            else { toolStripOnlineStatus.Text = "Off-Line"; }
        }
        #endregion

        private void GridViewToolStripMenuItem_Click(object sender, EventArgs e)
        { new Forms.UnitTestGridView().Show(); }

        private void PeekAtClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ClipboardView()); }

        private void TextEditorToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ProofOfConcept.TextEditor()); }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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

                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(BusinessData.ImportModel(openFile));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BusinessData.ModelFile is FileInfo file)
            {
                DoWork(BusinessData.ExportModel(file), onCompleting);
            }
            else
            { saveAsToolStripMenuItem_Click(sender, e); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
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

                DoWork(BusinessData.ExportModel(openFile), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        { Application.Exit(); }

        private void BindingModel_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (bindingModel.Current is IModelItem current)
            { namedScopeData.HeaderText = current.ModelTitle ?? "(no model title)"; }
            else { namedScopeData.HeaderText = "(no Model)"; }
        }

        private void SetAuthorization()
        {
            securitySetAuthorization.Enabled =
                BusinessData.Authorization.IsSecurityAdmin
                && Settings.Default.IsOnLineMode;

            securityAuthorization.Enabled = true;
        }


    }
}