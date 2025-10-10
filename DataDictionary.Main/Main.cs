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

            // Set the button images based on Scope.
            newAttributeCommand.Image = ScopeType.ModelAttribute.GetImage(CommandType.Add);
            newEntityCommand.Image = ScopeType.ModelEntity.GetImage(CommandType.Add);
            newProcessCommand.Image = ScopeType.ModelProcess.GetImage(CommandType.Add);
            newSubjectAreaCommand.Image = ScopeType.ModelSubjectArea.GetImage(CommandType.Add);

            manageModelCommand.Image = ScopeType.Model.GetImage(CommandType.Default);
            manageScriptingCommand.Image = ScopeType.Scripting.GetImage(CommandType.Default);
            securityRole.Image = ScopeType.SecurityRole.GetImage(CommandType.Default);
            browseHelpCommand.Image = ScopeType.ApplicationHelp.GetImage(CommandType.Default);

            // TODO: Check if correct.
            securityPrincipal.Image = ScopeType.SecurityPrincipal.GetImage(CommandType.Default);
            securityAuthorization.Image = ScopeType.SecuritySecurable.GetImage(CommandType.Default);

            viewLibraryMemberCommand.Image = ScopeType.LibraryType.GetImage(CommandType.Default);
            viewLibrarySourceCommand.Image = ScopeType.Library.GetImage(CommandType.Default);

            manageDatabasesCommand.Image = ScopeType.Database.GetImage(CommandType.Default);
            manageLibrariesCommand.Image = ScopeType.Library.GetImage(CommandType.Default);

            menuAttributes.Image = ScopeType.ModelAttribute.GetImage(CommandType.Default);
            menuAttributeAlaises.Image = ScopeType.ModelAttributeAlias.GetImage(CommandType.Default);
            menuAttributeProperties.Image = ScopeType.ModelAttributeProperty.GetImage(CommandType.Default);
            menuAttributeDefinitions.Image = ScopeType.ModelAttributeDefinition.GetImage(CommandType.Default);

            menuEntities.Image = ScopeType.ModelEntity.GetImage(CommandType.Default);
            menuEntityAttributes.Image = ScopeType.ModelEntityAttribute.GetImage(CommandType.Default);
            menuEntityAlias.Image = ScopeType.ModelEntityAlias.GetImage(CommandType.Default);
            menuEntityProperties.Image = ScopeType.ModelEntityProperty.GetImage(CommandType.Default);
            menuEntityDefinitions.Image = ScopeType.ModelEntityDefinition.GetImage(CommandType.Default);

            menuProcess.Image = ScopeType.ModelProcess.GetImage(CommandType.Default);
            menuProcessArgument.Image = ScopeType.ModelProcessArgument.GetImage(CommandType.Default);
            menuProcessAlias.Image = ScopeType.ModelProcessAlias.GetImage(CommandType.Default);
            menuProcessProperty.Image = ScopeType.ModelProcessProperty.GetImage(CommandType.Default);
            menuProcessDefinition.Image = ScopeType.ModelProcessDefinition.GetImage(CommandType.Default);

            menuSubjectArea.Image = ScopeType.ModelSubjectArea.GetImage(CommandType.Default);
            menuModelProperty.Image = ScopeType.ModelProperty.GetImage(CommandType.Default);
            menuModelDefinition.Image = ScopeType.ModelDefinition.GetImage(CommandType.Default);

            menuCatalogItem.Image = ScopeType.Database.GetImage(CommandType.Default);
            menuSchemaItem.Image = ScopeType.DatabaseSchema.GetImage(CommandType.Default);
            menuReferenceItem.Image = ScopeType.DatabaseReference.GetImage(CommandType.Default);
            menuTableItem.Image = ScopeType.DatabaseTable.GetImage(CommandType.Default);
            menuTableColumnItem.Image = ScopeType.DatabaseTableColumn.GetImage(CommandType.Default);
            menuRoutineColumnItem.Image = ScopeType.DatabaseFunctionColumn.GetImage(CommandType.Default);
            menuDomainItem.Image = ScopeType.DatabaseDomain.GetImage(CommandType.Default);
            menuPropertyItem.Image = ScopeType.DatabaseProperty.GetImage(CommandType.Default);
            menuConstraintItem.Image = ScopeType.DatabaseConstraint.GetImage(CommandType.Default);
            menuConstraintColumnItem.Image = ScopeType.DatabaseConstraintCheck.GetImage(CommandType.Default);
            menuRoutineItem.Image = ScopeType.DatabaseProcedure.GetImage(CommandType.Default); ;
            menuRoutineParameterItem.Image = ScopeType.DatabaseProcedureParameter.GetImage(CommandType.Default);

            menuScriptingTemplate.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Default);
            menuScriptingNode.Image = ScopeType.ScriptingTemplateNode.GetImage(CommandType.Default);
            menuScriptingNodeOwner.Image = ScopeType.ScriptingTemplateNodeOwner.GetImage(CommandType.Default);
            menuScriptingDocument.Image = ScopeType.ScriptingTemplateDocument.GetImage(CommandType.Default);
            menuScriptingDataSource.Image = ScopeType.ScriptingData.GetImage(CommandType.Default);

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
                if (dataLoaded)
                { this.Invoke(() => { splashScreen.Close(); }); }

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

        private void gridViewToolStripMenuItem_Click(object sender, EventArgs e)
        { new Forms.UnitTestGridView().Show(); }

        private void peekAtClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new ClipboardView()); }

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

                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(BusinessData.ImportModel(openFile));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BusinessData.ModelFile is FileInfo file)
            {
                DoWork(BusinessData.ExportModel(file), onCompleting);
            }
            else
            { saveAsToolStripMenuItem_Click(sender, e); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { }
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
            securityPrincipal.Enabled = BusinessData.Authorization.IsSecurityAdmin;
            securityRole.Enabled = BusinessData.Authorization.IsSecurityAdmin;
            securityAuthorization.Enabled = true;

        }

    }
}